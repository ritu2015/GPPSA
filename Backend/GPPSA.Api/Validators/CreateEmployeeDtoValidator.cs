using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using GPPSA.Application.Dtos.Employee;
using GPPSA.Application.Interfaces;

namespace GPPSA.Api.Validators
{
    public class CreateEmployeeDtoValidator:AbstractValidator<CreateEmployeeDtos>
    {
        public CreateEmployeeDtoValidator(IEmployeeRepository employeeRepository)
        {
            RuleFor(x => x.EmployeeCode).NotEmpty().MaximumLength(20).WithMessage("Employee code is required.");
            RuleFor(x => x.FullName).NotEmpty().MinimumLength(3).MaximumLength(100).WithMessage("Full name is required and must be between 3 and 100 characters.");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Valid email is required.");
            RuleFor(x => x.DateOfJoining).NotEmpty().LessThanOrEqualTo(DateTime.Now).WithMessage("Date of joining must be in the past.");
            RuleFor(x => x.Salary).GreaterThan(0).WithMessage("Salary must be greater than zero.");
            RuleFor(x => x.DepartmentId).Must(departmentId => employeeRepository.DepartmentExists(departmentId))
                .WithMessage("Department does not exist."); 
        }
    }
}