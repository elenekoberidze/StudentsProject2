using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsProject2.Models
{
    public class User
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public User() { }

        public User(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be empty.");

            this.Username = username.Trim();
            this.Password = password;
        }

        public bool ValidatePassword(string password)
        {
            return Password == password; 
        }
    }
}
