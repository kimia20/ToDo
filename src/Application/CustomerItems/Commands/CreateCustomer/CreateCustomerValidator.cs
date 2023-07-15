using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CustomerItems.Commands.CreateCustomer
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
    {
        private readonly IToDoDbContext _context;

        public CreateCustomerValidator(IToDoDbContext context)
        {
            _context = context;

            RuleFor(v => v.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MustAsync(BeUniqueEmail).WithMessage("The specified email already exists.");
            RuleFor(v => v.PhoneNumber)
               .NotEmpty().WithMessage("Phone is required.")
               .MustAsync(BeValidPhoneNumber).WithMessage("Phone validatin has error.");

            RuleFor(m => new { m.FirstName, m.LastName, m.DateOfBirth })
                .Must(x => BeUniqueCustomer(x.FirstName, x.LastName, x.DateOfBirth).Result)
                                      .WithMessage("Customer already exist");
        }

        public async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return await _context.Customers
                .AllAsync(l => l.Email != email, cancellationToken);
        }

        public async Task<bool> BeValidPhoneNumber(string phone, CancellationToken cancellationToken)
        {
            var util = PhoneNumberUtil.GetInstance();
            try
            {
                var number = util.Parse(phone, "IR");
                return util.IsValidNumber(number);
            }
            catch (NumberParseException)
            {
                return false;
            }

        }
        public async Task<bool> BeUniqueCustomer(string firstName, string lastName, DateTime birthDate)
        {
            return await _context.Customers
                .AllAsync(l => l.FullName.FirstName != firstName && l.FullName.LastName!=lastName
                && l.DateOfBirth!=birthDate);
        }
    }
}
