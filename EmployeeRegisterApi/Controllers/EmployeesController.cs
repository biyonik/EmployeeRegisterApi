using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeeRegisterApi.Context;
using EmployeeRegisterApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeRegisterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController: ControllerBase
    {
        private readonly EmployeeAppContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public EmployeesController(EmployeeAppContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _context.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]EmployeeModel employeeModel)
        {
            employeeModel.ImageName = await SaveImage(employeeModel.ImageFile);
            await _context.Employees.AddAsync(employeeModel);
            await _context.SaveChangesAsync();
            return Created("", employeeModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, EmployeeModel employeeModel)
        {
            if (id != employeeModel.Id)
            {
                return BadRequest("Geçersiz parametre");
            }

            _context.Entry<EmployeeModel>(employeeModel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return BadRequest("Geçersiz istek");
            }

            _context.Entry<EmployeeModel>(employee).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile file)
        {
            var fileName =
                new String(Path.GetFileNameWithoutExtension(file.FileName).Take(10).ToArray()).Replace(' ', '-');
            fileName += DateTime.Now.ToString("yymmssfff") + Path.GetExtension(file.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images/", fileName);
            await using var fileStream = new FileStream(imagePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return fileName;
        }
    }
}