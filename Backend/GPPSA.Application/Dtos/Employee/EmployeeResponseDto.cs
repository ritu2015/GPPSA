using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GPPSA.Application.Dtos.Employee
{
    public class EmployeeResponseDto
    {
        public Guid Id { get; set; }
        public string EmployeeCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public DateTime DateOfJoining { get; set; }
    }
}