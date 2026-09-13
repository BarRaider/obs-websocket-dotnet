using System;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OBSWebsocketDotNet.Tests
{
    /// <summary>
    /// Minimal loopback WebSocket server used by tests to exercise OBSWebsocket against
    /// real socket I/O instead of mocking the transport.
    /// </summary>
    internal sealed class TestWebSocketServer : IDisposable
    {
        private readonly HttpListener listener;
        private WebSocket serverSocket;

        public int Port { get; }

        private TestWebSocketServer(int port)
        {
            Port = port;
            listener = new HttpListener();
            listener.Prefixes.Add($"http://127.0.0.1:{port}/");
        }

        public static TestWebSocketServer Start(Func<WebSocket, CancellationToken, Task> onConnected)
        {
            var server = new TestWebSocketServer(GetFreeTcpPort());
            server.listener.Start();

            _ = Task.Run(async () =>
            {
                try
                {
                    var context = await server.listener.GetContextAsync();
                    var wsContext = await context.AcceptWebSocketAsync(null);
                    server.serverSocket = wsContext.WebSocket;
                    await onConnected(wsContext.WebSocket, CancellationToken.None);
                }
                catch
                {
                    // Listener stopped/disposed during test teardown - ignore.
                }
            });

            return server;
        }

        public static async Task<string> ReceiveTextAsync(WebSocket socket, CancellationToken ct)
        {
            var buffer = new byte[8192];
            using var ms = new System.IO.MemoryStream();
            WebSocketReceiveResult result;
            do
            {
                result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), ct);
                ms.Write(buffer, 0, result.Count);
            } while (!result.EndOfMessage);
            return Encoding.UTF8.GetString(ms.ToArray());
        }

        public static Task SendTextAsync(WebSocket socket, string text, CancellationToken ct)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            return socket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, ct);
        }

        private static int GetFreeTcpPort()
        {
            var tcpListener = new TcpListener(IPAddress.Loopback, 0);
            tcpListener.Start();
            int port = ((IPEndPoint)tcpListener.LocalEndpoint).Port;
            tcpListener.Stop();
            return port;
        }

        public void Dispose()
        {
            serverSocket?.Dispose();
            if (listener.IsListening)
            {
                listener.Stop();
            }
            ((IDisposable)listener).Dispose();
        }
    }
}
