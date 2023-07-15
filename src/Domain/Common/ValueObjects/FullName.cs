using RsjFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Domain.Common.ValueObjects
{
    public class FullName : BaseValueObject<FullName>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        private FullName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        public FullName() { }

        public static Result<FullName> Create(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                return Result.Fail<FullName>("FirstName is Required");
            if (string.IsNullOrWhiteSpace(lastName))
                return Result.Fail<FullName>("LastName is Required");
            if (firstName.Length > 100)
            {
                return Result.Fail<FullName>("Max length of FirstName must be 100 charecters.");

            }
            if (lastName.Length > 100)
            {
                return Result.Fail<FullName>("Max length of LastName must be 100 charecters.");
            }
            return Result.Ok(new FullName(firstName, lastName));
        }

        protected override bool IsEqual(FullName other)
        {
            return FirstName.Equals(other.FirstName, StringComparison.InvariantCultureIgnoreCase) && LastName.Equals(other.LastName, StringComparison.InvariantCultureIgnoreCase);
        }
        public override string ToString()
        {
            return $"{FirstName ?? ""} {LastName ?? ""}";
        }
    }
}
