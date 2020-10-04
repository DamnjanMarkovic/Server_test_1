using System;


//https://www.youtube.com/watch?v=HFnJLv2Q1go
namespace Server_test_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting server on port 8080");
            HTTPServer server = new HTTPServer(8080);
            server.Start();
        }
    }
}
