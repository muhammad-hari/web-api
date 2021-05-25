using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Api.RestModels.Request
{
    public class Login
    {
        public string Username { get; set; }

        /// <summary>
        /// Unsecure user password, in production we need to change it with hash and so on
        /// just sample.
        /// </summary>
        public string Password { get; set; }
    }
}
