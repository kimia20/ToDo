using Application.Common.Exceptions;
using Application.Common.Interfaces;
using ToDo.Domain.CustomerAggregate;
using ToDo.Domain.CustomerAggregate.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CustomerItems.Commands.DeleteCustomer
{
    public record DeleteCustomerCommand(int Id) : IRequest;

    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly IToDoDbContext _context;

        public DeleteCustomerCommandHandler(IToDoDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Customers
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(CustomerItem), request.Id);
            }

            _context.Customers.Remove(entity);

            entity.AddDomainEvent(new CustomerDeleteEvent(entity));

            await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
