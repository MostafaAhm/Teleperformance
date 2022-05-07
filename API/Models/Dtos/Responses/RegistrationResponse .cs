using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Configuration;

namespace API.Models.Dtos.Responses
{
    public class RegistrationResponse : AuthResult
    {
        public string Id { get; set; }
        public string Username  { get; set; }
        public string Email { get; set; }
        
        public string Password { get; set; }
    }
}