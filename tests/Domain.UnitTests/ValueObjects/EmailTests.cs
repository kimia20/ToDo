using ToDo.Domain.Common.ValueObjects;
using FluentAssertions;
using NUnit.Framework;
using RsjFramework.Entities;

namespace ToDo.Domain.UnitTests.ValueObjects
{

    public class EmailTests
    {
        [Test]
        public void ShouldReturnCorrectEmail()
        {
            var emailValue = "kimia_heidari1998@outlook.com";

                var email = Email.Create(emailValue)?.Value;

            email.Value.Should().Be(emailValue);
        }

        [Test]
        public void ValidationFailureCreateInvalidEmail()
        {
            var emailValue = "kimia.com";
            var result = Email.Create(emailValue);
            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void ValidationFailureCreateEmptyEmail()
        {
            var emailValue = "";
            var result = Email.Create(emailValue);
            Assert.IsFalse(result.IsSuccess);
        }
    }
}
