using ToDo.Domain.CustomerAggregate.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CustomerItems.Queries.GetCustomerById
{
    public record GetCustomerByIdQuery : IRequest<CustomerItemDto>
    {
        public int Id { get; set; }
    }
}
