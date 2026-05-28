using GPPSA.Application.Dtos.Employee;
using GPPSA.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GPPSA.Api
{
    [Authorize]
    [ApiController]
    [Route("api/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _Service;

        public EmployeesController(EmployeeService service)
        {
            _Service = service;
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(CreateEmployeeDtos dto)
        {
            var id = await _Service.CreateEmployeeAsync(dto);
            return CreatedAtAction(nameof(GetEmployee), new { id }, new {id});
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(Guid id)
        {
            var employee = await _Service.GetEmployeeAsync(id);
            return Ok(employee);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _Service.GetAllAsync();
            return Ok(employees);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, UpdateEmployeeDto employeeDto)
        {
            await _Service.UpdateEmployeeAsync(id, employeeDto);
            return NoContent();  //Update successful, no content to return
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            await _Service.DeleteEmployeeAsync(id);
            return NoContent();  //Delete successful, no content to return
        }
    }
}