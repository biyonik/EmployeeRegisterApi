using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeRegisterApi.Context;
using EmployeeRegisterApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeRegisterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController: ControllerBase
    {
        private readonly EmployeeAppContext _context;

        public EmployeesController(EmployeeAppContext context)
        {
            _context = context;
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
        public async Task<IActionResult> Create(EmployeeModel employeeModel)
        {
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
    }
}