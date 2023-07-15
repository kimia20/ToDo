using ToDo.Domain.Common.ValueObjects;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnitTests.ValueObjects
{
    public class AccountNumberTest
    {
        [Test]
        public void ShouldReturnCorrectAccountNumber()
        {
            var accountNo = "305262329";

            var accountValueObject = AccountNumber.Create(accountNo)?.Value;

            accountValueObject.Value.Should().Be(accountNo);
        }
        [Test]
        public void ValidaationFailureLengthAccountNumber()
        {
            var accountNo = "30526232";
            var result =AccountNumber.Create(accountNo);
            Assert.IsFalse(result.IsSuccess);
        }
    }
}
