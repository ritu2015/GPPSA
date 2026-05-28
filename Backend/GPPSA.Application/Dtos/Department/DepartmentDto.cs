using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GPPSA.Application.Dtos.Department
{
    public class DepartmentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
    }
}