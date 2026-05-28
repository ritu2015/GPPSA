using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPPSA.Domain.Entities;

namespace GPPSA.Application.Dtos.Employee
{
    public class CreateEmployeeDtos
    {
    public string EmployeeCode { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DateOfJoining { get; set; } = DateTime.MinValue;
    public decimal Salary { get; set; }
    public Guid DepartmentId { get; set; }
    }
}