using System;
using System.Collections.Generic;
using System.Text;
using SimpleTcp;

namespace TCPServer.Connection
{
    class TCPConnection
    {
        SimpleTcpServer server; // server instance

        private string _ip; // ip private var
        public string IP // ip property
        {
            get => this._ip;
            private set // setting validation
            {
                this._ip = (string.IsNullOrWhiteSpace(value)) ?
                    throw new ArgumentException(nameof(IP),
                    "Ip must be a number and can't be null or empty") : value;
            }
        }

        public List<string> UsersOnline = new List<string>(); // list of users online

        // initialize connection in the constructor
        public TCPConnection(string ip)
        {
            // assigning constructor data
            this.IP = ip;

            // starting server
            server = new SimpleTcpServer($"{this.IP}");

            try
            {
                server.Start();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("Initializing server...");
            //while (!server.IsConnected($"{this.IP}:{this.IpPort}")) // check if server isn't connected

            if (server.IsConnected($"{this.IP}")) // check if is connected
                Console.WriteLine($"Server initialized");

            // Events (Connected, Disconnected and DataReceived from client)
            server.Events.ClientConnected += Events_ClientConnected;
            server.Events.ClientDisconnected += Events_ClientDisconnected;
            server.Events.DataReceived += Events_DataReceived;

            // Server commands
            SendMessage();
        }

        private void SendMessage()
        {
            while (true) // server.IsConnected($"{this.IP}:{this.IpPort}")
            {
                string serverCommand = Console.ReadLine();

                switch (serverCommand)
                {
                    case "/userlist": // check users
                        foreach (var user in this.UsersOnline)
                        {
                            Console.WriteLine($"Users online: {user}\n");
                        }
                        break;

                    default:
                        Console.WriteLine("Must use / to send a command.");
                        break;
                }
            }
        }

        // Data received when a client is connected
        private void Events_ClientConnected(object sender, ConnectionEventArgs e)
        {
            Console.WriteLine($"{e.IpPort} connected.");
            UsersOnline.Add(e.IpPort);
        }

        // Data received when a client is disconnected
        private void Events_ClientDisconnected(object sender, ConnectionEventArgs e)
        {
            Console.WriteLine($"{e.IpPort} disconnected from the server.");
        }

        // Data received from Client
        private void Events_DataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine($"{e.IpPort} - {Encoding.UTF8.GetString(e.Data)}");

            // send message to everyone
            string message = Encoding.UTF8.GetString(e.Data);
            UsersOnline.ForEach(delegate (string userip)
            {
                server.Send(userip, message);
            });
        }
    }
}
