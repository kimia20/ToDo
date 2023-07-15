using Application.Common.Interfaces;
using Application.CustomerItems.Commands.CreateCustomer;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RsjFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CustomerItems.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        private readonly IToDoDbContext _context;

        public UpdateCustomerCommandValidator(IToDoDbContext context)
        {
            _context = context;

            RuleFor(v => v.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MustAsync(BeUniqueEmail).WithMessage("The specified email already exists.");

            RuleFor(m => new { m.FirstName, m.LastName, m.DateOfBirth })
                .Must(x => BeUniqueCustomer(x.FirstName, x.LastName, x.DateOfBirth).Result)
                                      .WithMessage("Customer already exist");
        }

        public async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return await _context.Customers
                .AllAsync(l => l.Email != email, cancellationToken);
        }
        public async Task<bool> BeUniqueCustomer(string firstName, string lastName, DateTime birthDate)
        {
            var result= await _context.Customers
                .Where(l => l.FullName.FirstName != firstName && l.FullName.LastName != lastName
                && l.DateOfBirth != birthDate).AsNoTracking().FirstOrDefaultAsync();
            return result == null;
        }
    }
}
