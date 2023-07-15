using ToDo.Domain.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Domain.CustomerAggregate.Dto
{
    public class CustomerModifyDto
    {
        public FullName FullName { get;  set; }
        public Email Email { get;  set; }
        public PhoneNumber PhoneNumber { get;  set; }
        public DateTime DateOfBirth { get;  set; }
        public AccountNumber BankAccountNumber { get;  set; }
    }
}
