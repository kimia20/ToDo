using Application.Common.Exceptions;
using Application.Common.Interfaces;
using ToDo.Domain.CustomerAggregate;
using ToDo.Domain.CustomerAggregate.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RsjFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CustomerItems.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerItemDto>
    {
        private readonly IToDoDbContext _context;

        public GetCustomerByIdQueryHandler(IToDoDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerItemDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var result= await _context.Customers
                .Where(x => x.Id == request.Id)
                .Select(o=>new CustomerItemDto
                {
                    Id=o.Id,
                    BankAccountNumber=o.BankAccountNumber.Value,
                    DateOfBirth=o.DateOfBirth,
                    Email=o.Email,
                    FirstName=o.FullName.FirstName,
                    LastName=o.FullName.LastName,
                    PhoneNumber=o.PhoneNumber.Value,
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
            if (result == null)
            {
                throw new NotFoundException(nameof(CustomerItem), request.Id);
            }
            return result;
        }
    }
}

