using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestClient
{
    /// <summary>
    /// Result of the function given to <see cref="AsyncEventListener{TResult, TEventArgs}"/>.
    /// </summary>
    /// <typeparam name="TResult">Result type returned by <see cref="AsyncEventListener{TResult, TEventArgs}.Task"/>.</typeparam>
#pragma warning disable CA1815 // Override equals and operator equals on value types
    public struct EventListenerResult<TResult>
#pragma warning restore CA1815 // Override equals and operator equals on value types
    {
        /// <summary>
        /// Result from the execution of the function.
        /// </summary>
        public readonly TResult Result;
        /// <summary>
        /// Set to true if the result should be returned in <see cref="AsyncEventListener{TResult, TEventArgs}.Task"/>
        /// </summary>
        public readonly bool IsFinished;

        public EventListenerResult(TResult result, bool isFinished)
        {
            Result = result;
            IsFinished = isFinished;
        }
    }

    /// <summary>
    /// <see cref="AsyncEventListener{TResult, TEventArgs}"/> provides a way to listen to an event in the form of a <see cref="Task{TResult}"/>
    /// </summary>
    /// <typeparam name="TResult">Type of result returned by the <see cref="Task"/>.</typeparam>
    /// <typeparam name="TEventArgs">Type of the event arguments in the <see cref="EventHandler"/></typeparam>
    public class AsyncEventListener<TResult, TEventArgs>
    {
        private readonly Func<object, TEventArgs, EventListenerResult<TResult>> _function;
        private CancellationToken _cancellationToken;
        private CancellationTokenRegistration _tokenRegistration;

        private readonly int _timeout;
        private CancellationTokenSource TimeoutSource;
        private CancellationToken _timeoutToken;
        private CancellationTokenRegistration _timeoutRegistration;

        private object startLock = new object();
        private TaskCompletionSource<TResult> _taskCompletion = null;

        private bool _listening;
        /// <summary>
        /// Returns true if the <see cref="AsyncEventListener{TResult, TEventArgs}"/> is listening to any events its subscribed to.
        /// </summary>
        public bool Listening
        {
            get => _listening && !_taskCompletion.Task.IsCompleted;
            set => _listening = value;
        }

        /// <summary>
        /// This task will remain incomplete until an event <see cref="OnEvent(object, TEventArgs)"/>
        /// is subscribed to is raised and the function returns a <see cref="EventListenerResult{TResult}"/> 
        /// where <see cref="EventListenerResult{TResult}.IsFinished"/> is true, the <see cref="CancellationToken"/> is cancelled, or the event listener times out.
        /// </summary>
        public Task<TResult> Task
        {
            get
            {
                if (_listening == false)
                    StartListening();
                return _taskCompletion.Task;
            }
        }

        /// <summary>
        /// This must be called for the <see cref="AsyncEventListener{TResult, TEventArgs}"/> to start listening to events.
        /// The timeout clock starts when this is called if a timeout was provided in the constructor.
        /// </summary>
        public void StartListening()
        {
            bool wasListening = false;
            lock (startLock)
            {
                wasListening = _listening;
                Listening = true;
            }
            if (wasListening) return; // Don't StartListening twice
            if (_timeout > 0)
            {
                TimeoutSource = new CancellationTokenSource(_timeout);
                _timeoutToken = TimeoutSource.Token;
                _timeoutRegistration = _timeoutToken.Register(Timeout);
            }
        }

        /// <summary>
        /// Resets the <see cref="AsyncEventListener{TResult, TEventArgs}"/> so it can be reused with a new <see cref="CancellationToken"/>.
        /// <see cref="StartListening"/> must be called again, the existing timeout value, if any, is used.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public void Reset(CancellationToken cancellationToken = default)
        {
            _listening = false;
            TaskCompletionSource<TResult> previous = _taskCompletion;
            if (previous != null)
                previous.TrySetException(new OperationCanceledException("Operation cancelled: AsyncEventListener was reset."));
            Cleanup();
            _taskCompletion = new TaskCompletionSource<TResult>();
            _cancellationToken = cancellationToken;
            if (_cancellationToken.CanBeCanceled)
                _tokenRegistration = _cancellationToken.Register(CancelInternal);
        }

        /// <summary>
        /// Subscribe this method to the event it should listen to.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void OnEvent(object sender, TEventArgs eventArgs)
        {
            bool finished = true;
            if (!Listening) return;
            try
            {
                EventListenerResult<TResult> result = _function(sender, eventArgs);
                if (result.IsFinished)
                    _taskCompletion.TrySetResult(result.Result);
                else
                    finished = false;
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (OperationCanceledException)
            {
                _taskCompletion.TrySetCanceled(_cancellationToken);
            }
            catch (Exception ex)
            {
                _taskCompletion.TrySetException(ex);
            }
#pragma warning restore CA1031 // Do not catch general exception types
            finally
            {
                if (finished)
                {
                    _tokenRegistration.Dispose();
                    Listening = false;
                    Cleanup();
                }
            }
        }

        /// <summary>
        /// Creates a new <see cref="AsyncEventListener{TResult, TEventArgs}"/> with an optional timeout (in milliseconds) and <see cref="CancellationToken"/>.
        /// </summary>
        /// <param name="function">Function that will operate on the parameters given by the raised event.</param>
        /// <param name="timeout">Timeout in milliseconds (0 for none). The clock starts when <see cref="StartListening"/> is called.</param>
        /// <param name="cancellationToken"></param>
        public AsyncEventListener(Func<object, TEventArgs, EventListenerResult<TResult>> function, int timeout, CancellationToken cancellationToken = default)
        {
            _timeout = timeout;
            Reset(cancellationToken);
            _function = function;
        }

        /// <summary>
        /// Creates a new <see cref="AsyncEventListener{TResult, TEventArgs}"/> with an optional <see cref="CancellationToken"/>.
        /// </summary>
        /// <param name="function">Function that will operate on the parameters given by the raised event.</param>
        /// <param name="cancellationToken"></param>
        public AsyncEventListener(Func<object, TEventArgs, EventListenerResult<TResult>> function, CancellationToken cancellationToken = default)
            : this(function, 0, cancellationToken)
        { }
        public void SetResult(TResult result)
        {
            _taskCompletion.TrySetResult(result);
            Cleanup();
        }
        public void SetException(Exception exception)
        {
            Cleanup();
            _taskCompletion.TrySetException(exception);
        }

        private void Timeout()
        {
            _taskCompletion.TrySetException(new TimeoutException("EventListener timed out."));
            Cleanup();
        }

        private void CancelInternal()
        {
            _taskCompletion.TrySetCanceled(_cancellationToken);
            Cleanup();
        }

        public void Cancel()
        {
            _taskCompletion.TrySetCanceled();
            Cleanup();
        }
        public void Cancel(CancellationToken cancelSource)
        {
            _taskCompletion.TrySetCanceled(cancelSource);
            Cleanup();
        }

        private void Cleanup()
        {
            Listening = false;
            _tokenRegistration.Dispose();
            _timeoutRegistration.Dispose();
            TimeoutSource?.Dispose();
            TimeoutSource = null;
        }
    }
}
