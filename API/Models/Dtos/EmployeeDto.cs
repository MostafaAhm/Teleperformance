using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace API.Models.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }  
        
        public string Email { get; set; }
        
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        
        public string Name { get; set; }
        public IFormFile Files { get; set; }
        public byte[] Image { get; set; }
        public string EmployeeType { get; set; }
        
    }
}