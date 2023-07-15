using ToDo.Domain.CustomerAggregate.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CustomerItems.Queries.GetAllCustomer
{
    public record GetAllCustomerQuery : IRequest<List<CustomerItemDto>>
    {
    }
}
