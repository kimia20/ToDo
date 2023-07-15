using ToDo.Domain.Common.ValueObjects;
using ToDo.Domain.CustomerAggregate.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CustomerItems.Commands.CreateCustomer
{
    public record CreateCustomerCommand : IRequest<CustomerItemDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string BankAccountNumber { get; set; }
    }
}
