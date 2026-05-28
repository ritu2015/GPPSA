using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPPSA.Domain;

namespace GPPSA.Application.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Guid>  AddEmployeeAsync(Employee employee);
        Task<Employee?> GetEmployeeByIdAsync(Guid id);
        Task<List<Employee>> GetAllEmployeesAsync();
        Task UpdateEmployeeAsync(Employee employee);
        bool DepartmentExists(Guid departmentId);
        Task DeleteEmployeeAsync(Employee employee);
    }
}