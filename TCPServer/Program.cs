using System;
using TCPServer.Connection;

namespace TCPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // initialize server
            TCPConnection serverConnection = new TCPConnection("127.0.0.1:9000");
        }
    }
}
