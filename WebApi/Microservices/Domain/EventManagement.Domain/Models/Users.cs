using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Domain.Models
{
    public class Users
    {
        public string Username { get; set; }

        /// <summary>
        /// Unsecure user password, in production we need to change it with hash and so on
        /// just sample.
        /// </summary>
        public string Password { get; set; }
    }
}
