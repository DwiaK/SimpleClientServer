using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer.Accounts
{
    class User
    {
        // to implement

        private int _userID;
        public int UserID
        {
            get => this._userID;
            set
            {
                this._userID = (value <= 0) ?
                    throw new ArgumentOutOfRangeException(nameof(UserID),
                    "User ID must be greater than 0") : value;
            }
        }

        private string _username;
        public string Username
        {
            get => this._username;
            set
            {
                this._username = (string.IsNullOrWhiteSpace(value)) ?
                    throw new ArgumentException(nameof(Username),
                    "Username is Invalid (null or empty)") : value;
            }
        }
    }
}
