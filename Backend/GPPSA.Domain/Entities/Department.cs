using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GPPSA.Domain.Entities
{
    public class Department
    {
        public Guid Id { get; set; }
        public string DepartmentName { get; set; } = string.Empty;  
        public ICollection<Employee> Employees { get; set; } = new List<Employee>(); //Navigation property
    }
}