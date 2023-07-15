﻿using ToDo.Domain.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Domain.CustomerAggregate.Dto
{
    public class CustomerItemDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get;  set; }
        public string PhoneNumber { get;  set; }
        public DateTime DateOfBirth { get;  set; }
        public string BankAccountNumber { get; set; }
    }
}
