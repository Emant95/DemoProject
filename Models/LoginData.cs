using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoProject.Models
{
    public class LoginData
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Usertype { get; set; }
    }
    public class SignUPData
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string rePassword { get; set; }
        public string Usertype { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string gender { get; set; }
        public string PhoneNumber { get; set; }
    }
}