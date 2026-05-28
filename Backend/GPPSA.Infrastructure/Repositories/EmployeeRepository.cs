using Microsoft.EntityFrameworkCore;
using GPPSA.Application.Interfaces;
using GPPSA.Domain;
using GPPSA.Infrastructure.Data;

namespace GPPSA.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDBContext _context;

        public EmployeeRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee.Id;
        }

        public bool DepartmentExists(Guid departmentId)
        {
            return _context.Departments.Any(d => d.Id == departmentId);
        }

        public  Task<List<Employee>> GetAllEmployeesAsync()
        {
            return  _context.Employees.Include(e => e.Department).AsNoTracking().ToListAsync();
        }

        public Task<Employee?> GetEmployeeByIdAsync(Guid id)
        {
           return _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            
                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();
          
        }
       
        public Task DeleteEmployeeAsync(Employee employee)
        {
            _context.Employees.Remove(employee);
            return _context.SaveChangesAsync(); 
        }
    }
}