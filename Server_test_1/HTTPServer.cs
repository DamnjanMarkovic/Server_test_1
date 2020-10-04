using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server_test_1
{
    public class HTTPServer
    {

        private bool running = false;

        private TcpListener listener;

        public HTTPServer(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
        }

        public void Start()
        {
            Thread serverThread = new Thread(new ThreadStart(Run));
            serverThread.Start();
        }

        private void Run()
        {
            running = true;
            listener.Start();
            while (running)
            {
                TcpClient client = listener.AcceptTcpClient();

                HandleClient(client);

                client.Close();
            }

            running = false;
            listener.Stop();
        }

        private void HandleClient(TcpClient client)
        {

        }

    }
}
