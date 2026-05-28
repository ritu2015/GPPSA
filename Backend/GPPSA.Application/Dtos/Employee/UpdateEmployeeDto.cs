using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GPPSA.Application.Dtos.Employee
{
    public class UpdateEmployeeDto
    {

        // we are not exposing Id or EmployeeCode for update as they are not updatable fields
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
   
    public decimal Salary { get; set; }
    public Guid DepartmentId { get; set; }
    }
}