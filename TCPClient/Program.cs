using System;
using TCPClient.Connection;

namespace TCPClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // User put an username 
            Console.WriteLine("Type an username: ");
            string username = Console.ReadLine();

            // console is cleared
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White; // set console main color

            // Initialize client connection
            TCPConnection clientConnection = new TCPConnection("127.0.0.1:9000", username); // localhost ip
        }
    }
}
