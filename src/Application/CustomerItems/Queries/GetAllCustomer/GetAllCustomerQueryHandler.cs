using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.CustomerItems.Queries.GetCustomerById;
using ToDo.Domain.CustomerAggregate.Dto;
using ToDo.Domain.CustomerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.CustomerItems.Queries.GetAllCustomer
{
    public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, List<CustomerItemDto>>
    {
        private readonly IToDoDbContext _context;

        public GetAllCustomerQueryHandler(IToDoDbContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerItemDto>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Customers
                .Select(o => new CustomerItemDto
                {
                    Id = o.Id,
                    BankAccountNumber = o.BankAccountNumber.Value,
                    DateOfBirth = o.DateOfBirth,
                    Email = o.Email,
                    FirstName = o.FullName.FirstName,
                    LastName = o.FullName.LastName,
                    PhoneNumber = o.PhoneNumber.Value,
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            return result;
        }
    }
}

