using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using GPPSA.Application.Dtos.Employee;

namespace GPPSA.Api.Validators.Employee
{
    public class UpdateEmployeeDtoValidator:AbstractValidator<UpdateEmployeeDto>
    {
        public UpdateEmployeeDtoValidator()
        {
            RuleFor(e => e.FullName)
                .NotEmpty().WithMessage("Full Name is required.")
                .MinimumLength(3).WithMessage("Full Name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Full Name cannot exceed 100 characters.");

            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(e => e.Salary)
                .GreaterThan(0).WithMessage("Salary must be greater than zero.");

            RuleFor(e => e.DepartmentId)
                .NotEmpty().WithMessage("Department ID is required.");
        }
    }
}