using Application.Common.Exceptions;
using NUnit.Framework;
using FluentAssertions;
using FluentValidation.Results;
namespace Application.UnitTests
{

    public class ValidationExceptionTests
    {

        [Test]
        public void SingleValidationFailureCreatesErrorDictionary()
        {
            var failures = new List<ValidationFailure>
            {
                new ValidationFailure("Email", "Email is Required"),
            };

            var actual = new ValidationException(failures).Errors;

            actual.Keys.Should().BeEquivalentTo(new string[] { "Email" });
            actual["Email"].Should().BeEquivalentTo(new string[] { "Email is Required" });
        }
    }
}