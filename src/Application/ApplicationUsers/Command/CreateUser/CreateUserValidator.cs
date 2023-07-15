using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Application.ApplicationUsers.Command.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IApplicationUserRepository _repository;

        public CreateUserValidator(IApplicationUserRepository repository)
        {
            _repository = repository;

            RuleFor(v => v.Email)
                .MustAsync(BeUniqueEmail)
                 .When(o=> !string.IsNullOrEmpty(o.Email))
                     .WithMessage("The specified email already exists.")
                     .EmailAddress().WithMessage("Emailis not valid.");


            RuleFor(v => v.UserName)
               .NotEmpty().WithMessage("UserName is required.")
               .MustAsync(CheckUserName).WithMessage("The specified userName already exists.");


            RuleFor(p => p.Password).NotEmpty().WithMessage("Your password cannot be empty")
                  .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                  .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                  .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
        }

        public async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            var user= await _repository.FindByEmail(email);
            return (user != null ? false : true);
        }

        public async Task<bool> CheckUserName(string userName, CancellationToken cancellationToken)
        {
            var user = await _repository.FindByUserName(userName);
            return (user != null ? false : true);
        }
    }
}
