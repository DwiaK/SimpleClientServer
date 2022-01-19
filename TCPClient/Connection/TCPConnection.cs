using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTcp;

namespace TCPClient.Connection
{
    class TCPConnection
    {
        SimpleTcpClient client; // client instance

        private string _ip; // ip private var
        public string IP // ip property
        {
            get => this._ip;
            private set // setting validation
            {
                this._ip = (string.IsNullOrWhiteSpace(value) || int.TryParse(value, out int n)) ?
                    throw new ArgumentException(nameof(IP),
                    "Ip must be a number and can't be null or empty") : value;
            }
        }

        private string _username; // username private var
        public string Username // username property
        {
            get => this._username;
            private set // setting validation
            {
                this._username = (string.IsNullOrWhiteSpace(value)) ?
                    throw new ArgumentException(nameof(Username),
                    "Username cannot be empty or null.") : value;
            }
        }

        // constructor
        public TCPConnection(string ip, string username)
        {
            this.IP = ip;
            this.Username = username;

            client = new(ip); // set client ip
            
            // client events
            client.Events.Connected += Events_Connected;
            client.Events.Disconnected += Events_Disconnected;
            client.Events.DataReceived += Events_DataReceived;

            // Connect client
            try
            {
                client.Connect(); // Connect to server
            }
            catch (Exception e)
            {
                Console.WriteLine(e); // show error message
            }

            // if connected go to method send message
            if (client.IsConnected)
                SendMessage();
        }

        private void SendMessage()
        {
            string clientMessage = string.Empty;

            // when client is connected, send his messages
            while (client.IsConnected)
            {
                clientMessage = Console.ReadLine();
                if (!string.IsNullOrEmpty(clientMessage))
                    client.Send($"{this.Username}: {clientMessage}");
                // ClearLastLine();
            }
        }

        private void Events_Connected(object sender, ConnectionEventArgs e)
        {
            Console.WriteLine($"Connected as {this.Username}"); // Connected message
        }

        private void Events_Disconnected(object sender, ConnectionEventArgs e)
        {
            Console.WriteLine($"Disconnected."); // message when client is disconnected from server
        }

        private void Events_DataReceived(object sender, DataReceivedEventArgs e)
        {
            // receive from server
            Console.ForegroundColor = ConsoleColor.Green; // set green color to received messages
            Console.WriteLine($"{Encoding.UTF8.GetString(e.Data)}");
            Console.ForegroundColor = ConsoleColor.White; // set to main color
        }

        // Clear last line
        public static void ClearLastLine()
        {
            Console.Write(new string('\0', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
    }
}
