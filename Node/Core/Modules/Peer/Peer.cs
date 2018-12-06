using System;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;

namespace ADL.Node.Core.Modules.Peer
{    
    /// <summary>
    /// 
    /// </summary>
    public class Peer : IDisposable
    {
        public int nonce = 0;
        public string ipPort;
        public int Port;
        public string Ip;
        internal TcpClient TcpClient;
        internal SslStream SslStream;
        private bool Disposed = false;
        internal NetworkStream NetworkStream;
        private static readonly object Mutex = new object();
        private bool _Connected;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tcp"></param>
        public Peer(TcpClient tcp)
        {
            TcpClient = tcp ?? throw new ArgumentNullException(nameof(tcp));
            NetworkStream = tcp.GetStream();
            Port = ((IPEndPoint)tcp.Client.RemoteEndPoint).Port;
            Ip = ((IPEndPoint)tcp.Client.RemoteEndPoint).Address.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (Disposed)
            {
                return;
            }

            if (disposing)
            {
                if (SslStream != null)
                {
                    SslStream.Close();
                }

                if (NetworkStream != null)
                {
                    NetworkStream.Close();
                }

                if (TcpClient != null)
                {
                    TcpClient.Close();
                }
            }

            Disposed = true;
        }
    }
}