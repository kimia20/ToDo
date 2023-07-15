using ToDo.Domain.Common;
using ToDo.Domain.Common.ValueObjects;
using ToDo.Domain.CustomerAggregate.Dto;
using RsjFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Domain.CustomerAggregate
{
    public class CustomerItem : BaseAuditableEntity
    {
        public FullName FullName { get; private set; }
        public Email Email { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public AccountNumber BankAccountNumber { get; private set; }
        public static Result<CustomerItem> Create(CustomerItemDto customer)
        {
            var fullName = FullName.Create(customer.FirstName, customer.LastName);
            var email = Email.Create(customer.Email);
            var mobileNo = PhoneNumber.Create(customer.PhoneNumber);
            var accountNo = AccountNumber.Create(customer.BankAccountNumber);
            var result = ResultExtensions.CombineResults(fullName, email, mobileNo, accountNo);
            if (result.IsFailure)
                return Result.Fail<CustomerItem>(result.Error);

            return Result.Ok(new CustomerItem
            {
                DateOfBirth= DateTime.Now,
                FullName = fullName.Value,
                Email = email.Value,
                PhoneNumber = mobileNo.Value,
                BankAccountNumber= accountNo.Value
            });
        }
        public void UpdateDetail(CustomerModifyDto customerModify)
        {
            FullName = customerModify.FullName;
            PhoneNumber = customerModify.PhoneNumber;
            Email = customerModify.Email;
            BankAccountNumber = customerModify.BankAccountNumber;
            DateOfBirth = customerModify.DateOfBirth;
        }

    }
}
