using RsjFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ToDo.Domain.Common.ValueObjects
{
    public class PhoneNumber : BaseValueObject<PhoneNumber>
    {
        public string Value { get; private set; }
        private PhoneNumber(string value)
        {
            Value = value;
        }
        public PhoneNumber() { }

        public static Result<PhoneNumber> Create(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return Result.Fail<PhoneNumber>("PhoneNumber is required.");
            if (phone.Length > 11)
            {
                return Result.Fail<PhoneNumber>("Max Length of PhoneNumber is 11.");

            }
            return Result.Ok<PhoneNumber>(new PhoneNumber(phone));
        }

        protected override bool IsEqual(PhoneNumber other)
        {
            return Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);
        }
        public static implicit operator string(PhoneNumber value)
        {
            return value.Value;
        }
    }
}

