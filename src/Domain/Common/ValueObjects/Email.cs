using RsjFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ToDo.Domain.Common.ValueObjects
{
    public class Email : BaseValueObject<Email>
    {
        public string Value { get; private set; }
        private Email(string value)
        {
            Value = value;
        }
        public Email() { }
        public static Result<Email> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Result.Fail<Email>("Email is Required");
            if (!Regex.IsMatch(email, "^(.+)@(.+)$"))
                return Result.Fail<Email>($"{email} is not valid Email");

            return Result.Ok<Email>(new Email(email));
        }

        protected override bool IsEqual(Email other)
        {
            return Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);
        }
        public static implicit operator string(Email value)
        {
            return value.Value;
        }
    }
}
