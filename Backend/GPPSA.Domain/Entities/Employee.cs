using GPPSA.Domain.Entities;

namespace GPPSA.Domain;

public class Employee
{
    public Guid Id { get; set; }
    public string EmployeeCode { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DateOfJoining { get; set; } = DateTime.MinValue;
    public decimal Salary { get; set; }

    public Guid DepartmentId { get; set; }//FK
    public Department? Department { get; set; } //Navigation property
    public DateTime CreatedOn { get; set; }
}
