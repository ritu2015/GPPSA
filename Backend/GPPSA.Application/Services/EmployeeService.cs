using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GPPSA.Application.Dtos.Employee;
using GPPSA.Application.Exceptions;
using GPPSA.Application.Interfaces;
using GPPSA.Domain;

namespace GPPSA.Application.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repo;
        private readonly IMapper _mapper;

    public EmployeeService(IEmployeeRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<Guid> CreateEmployeeAsync(CreateEmployeeDtos employeeDto)
    {
        // var employee = new Employee
        // {
        //     Id = Guid.NewGuid(),
        //     EmployeeCode = employeeDto.EmployeeCode,
        //     FullName = employeeDto.FullName,
        //     Email = employeeDto.Email,
        //     DateOfJoining = employeeDto.DateOfJoining,
        //     Salary = employeeDto.Salary,
        //     DepartmentId = employeeDto.DepartmentId,
        //     CreatedOn = DateTime.UtcNow
        // };
        var employee = _mapper.Map<Employee>(employeeDto);

        employee.Id = Guid.NewGuid();
        employee.CreatedOn = DateTime.UtcNow;
   
        return await _repo.AddEmployeeAsync(employee);
    }

    public async Task<EmployeeResponseDto> GetEmployeeAsync(Guid id)
    {
        var employee = await _repo.GetEmployeeByIdAsync(id);
        if (employee == null)            
        throw new NotFoundException($"Employee with ID {id} not found.");   
        return _mapper.Map<EmployeeResponseDto>(employee);
    }

    public async Task<List<EmployeeResponseDto>> GetAllAsync()
    {
        //var employees = await _repo.GetAllEmployeesAsync();
        // Simulate an async database delay (Optional)
            await Task.Delay(50);

            // Return a mock list of employees for your interview dashboard
            return new List<EmployeeResponseDto>
            {
                new EmployeeResponseDto 
                { 
                    Id = Guid.NewGuid(), 
                    EmployeeCode = "EMP001",
                    FullName = "John Doe", 
                    Email = "john.doe@gppsa.com", 
                    DepartmentName = "Engineering", 
                    Salary = 75000
                },
                new EmployeeResponseDto 
                { 
                    Id = Guid.NewGuid(), 
                    EmployeeCode = "EMP002",
                    FullName = "Jane Smith", 
                    Email = "jane.smith@gppsa.com", 
                    DepartmentName = "HR", 
                    Salary = 65000
                },
                new EmployeeResponseDto 
                { 
                    Id = Guid.NewGuid(), 
                    EmployeeCode = "EMP003",
                    FullName = "Alex Jones", 
                    Email = "alex.jones@gppsa.com", 
                    DepartmentName = "Finance", 
                    Salary = 70000
                }
            };
        //return employees.Select(_mapper.Map<EmployeeResponseDto>).ToList(); 
    }
    
     public async Task UpdateEmployeeAsync(Guid id, UpdateEmployeeDto employeeDto)
    {
        var existingEmployee = await _repo.GetEmployeeByIdAsync(id);
        if (existingEmployee == null)            
        throw new NotFoundException($"Employee with ID {id} not found.");   

        _mapper.Map(employeeDto, existingEmployee);

        await _repo.UpdateEmployeeAsync(existingEmployee);
    }
    public async Task DeleteEmployeeAsync(Guid id)
        {
            var employee= await _repo.GetEmployeeByIdAsync(id);
            if (employee == null)            
                throw new NotFoundException($"Employee with ID {id} not found.");   
                await _repo.DeleteEmployeeAsync(employee);
        }
}
}