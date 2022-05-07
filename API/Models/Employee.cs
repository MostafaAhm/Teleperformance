using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace API.Models
{
    public class Employee
    {
        [Key]  
        public int Id { get; set; }  
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Name { get; set; }
        public IFormFile Files { get; set; }
        public byte[] Image { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public int EmployeeTypeId { get; set; }
    }
}