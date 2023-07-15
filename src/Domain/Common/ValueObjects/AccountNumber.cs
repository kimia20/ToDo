using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using RsjFramework.Entities;

namespace ToDo.Domain.Common.ValueObjects
{
    public class AccountNumber : BaseValueObject<AccountNumber>
    {
        public string Value { get; private set; }
        private AccountNumber(string value)
        {
            Value = value;
        }
        public AccountNumber() { }

        public static Result<AccountNumber> Create(string accountNo)
        {
            if (!Regex.IsMatch(accountNo, @"^\d{9,14}$"))
                return Result.Fail<AccountNumber>("Invalid AccountNumber.");
            if (string.IsNullOrWhiteSpace(accountNo))
                return Result.Fail<AccountNumber>("AccountNumber is Required");
            return Result.Ok<AccountNumber>(new AccountNumber(accountNo));
        }

        protected override bool IsEqual(AccountNumber other)
        {
            return Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);
        }
        public static implicit operator string(AccountNumber value)
        {
            return value.Value;
        }
    }
}

