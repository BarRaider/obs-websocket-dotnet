using System;

namespace OBSWebsocketDotNet
{
    /// <summary>
    /// Thrown if authentication fails
    /// </summary>
    public class AuthFailureException : Exception
    {
    }

    /// <summary>
    /// Thrown when the server responds with an error
    /// </summary>
    public class ErrorResponseException : Exception
    {
        public int ErrorCode { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        public ErrorResponseException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}