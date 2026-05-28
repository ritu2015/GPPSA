using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPPSA.Domain.Entities;
using GPPSA.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GPPSA.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/departments")]
    public class DepartmentController : ControllerBase
    {
        private readonly AppDBContext _context;

        public DepartmentController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _context.Departments.ToListAsync();
            return Ok(departments);
        }
    }
}