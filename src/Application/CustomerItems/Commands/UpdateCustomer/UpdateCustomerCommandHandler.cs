using Application.Common.Interfaces;
using Application.CustomerItems.Commands.CreateCustomer;
using ToDo.Domain.CustomerAggregate.Dto;
using ToDo.Domain.CustomerAggregate.Events;
using ToDo.Domain.CustomerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Common.ValueObjects;
using RsjFramework.Entities;
using Application.Common.Exceptions;
using ToDo.Domain.Common;

namespace Application.CustomerItems.Commands.UpdateCustomer
{
    internal class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
    {
        private readonly IToDoDbContext _context;

        public UpdateCustomerCommandHandler(IToDoDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {

            var fullName = FullName.Create(request.FirstName, request.LastName);
            var email = Email.Create(request.Email);
            var mobileNo = PhoneNumber.Create(request.PhoneNumber);
            var accountNo = AccountNumber.Create(request.BankAccountNumber);
            var result = ResultExtensions.CombineResults(fullName, email, mobileNo, accountNo);
            if (result.IsFailure)
                throw new ValidationException(result.Error);

            var entity = await _context.Customers
            .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(CustomerItem), request.Id);
            }

            entity.UpdateDetail(new CustomerModifyDto
            {
                FullName=fullName.Value,
                BankAccountNumber=accountNo.Value,
                Email=email.Value,
                PhoneNumber=mobileNo.Value,
                DateOfBirth = request.DateOfBirth,
            });
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
