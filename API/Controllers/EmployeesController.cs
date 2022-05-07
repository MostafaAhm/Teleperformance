using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")] //api/employees
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmployeesController : ControllerBase
    {
         private readonly ApiDbContext _context;
        public static IWebHostEnvironment _webHostEnvironment;
        public EmployeesController(ApiDbContext context,IWebHostEnvironment webHostEnvironment)
        {   
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [Route("GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _context.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        [Route("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee([FromForm] Employee data)
        {
            if(ModelState.IsValid)
            {
            
            await _context.Employees.AddAsync(data);
            await _context.SaveChangesAsync();
            string message = "";
            var files = data.Files;
            data.Files = null;
            if (data.Id > 0 && files != null && files.Length > 0)  
            {  
               string path = _webHostEnvironment.WebRootPath + "\\Images\\";

               if(!Directory.Exists(path))
                   Directory.CreateDirectory(path);

                string fileName = "Images_" + data.Id + ".png";
                if(System.IO.File.Exists(path + fileName))
                {
                    System.IO.File.Delete(path + fileName);
                }
                using (FileStream fileStream = System.IO.File.Create(path + fileName))
                {
                   data.Files.CopyTo(fileStream);
                   fileStream.Flush();
                   message = "Success";
                }
            }  
            else if (data.Id == 0)
            {
                message = "Failed";
            }
            else 
            {
                message = "Success";
            } 
            if (message == "Success") return Ok(data);

            return CreatedAtAction("GetEmployee", new {data.Id}, data);
            }
           

            return new JsonResult("Something went wrong") {StatusCode = 500};
        }

        [HttpGet("{id}")]
        [Route("GetEmployeeById")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);

            string fileName = "Images_" + employee.Id + ".png";
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images",fileName);
            employee.Image = await System.IO.File.ReadAllBytesAsync(path);

            if(employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPut("{id}")]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        {
            if(id != employee.Id)
                return BadRequest();

            var existEmployee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if(existEmployee == null)
                return NotFound();

            existEmployee.Name = employee.Name;
            existEmployee.Email = employee.Email;
            existEmployee.PhoneNumber = employee.PhoneNumber;
            existEmployee.EmployeeTypeId = employee.EmployeeTypeId;
            existEmployee.Image = employee.Image;
            
            // Implement the changes on the database level
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Route("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var existEmployee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if(existEmployee == null)
                return NotFound();

            _context.Employees.Remove(existEmployee);
            await _context.SaveChangesAsync();

            return Ok(existEmployee);
        }

    }
}