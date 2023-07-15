using Application.Common.Exceptions;
using Application.Common.Interfaces;
using ToDo.Domain.CustomerAggregate;
using ToDo.Domain.CustomerAggregate.Dto;
using ToDo.Domain.CustomerAggregate.Events;
using FluentValidation.Results;
using MediatR;
using RsjFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CustomerItems.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerItemDto>
    {
        private readonly IToDoDbContext _context;

        public CreateCustomerCommandHandler(IToDoDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerItemDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new CustomerItemDto
            {
                DateOfBirth = request.DateOfBirth,
                BankAccountNumber = request.BankAccountNumber,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber
            };
            var entity = CustomerItem.Create(customer);
            if (entity.IsFailure) throw new ValidationException(entity.Error);
            entity.Value.AddDomainEvent(new CustomerCreateEvent(entity.Value));

            _context.Customers.Add(entity.Value);

            await _context.SaveChangesAsync(cancellationToken);
            customer.Id = entity.Value.Id;
            return customer;
        }
    }
}
