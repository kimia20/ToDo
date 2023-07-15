using Application.Common.Exceptions;
using Application.CustomerItems.Commands.CreateCustomer;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTest.Customers.Command;
    using static TestSetUp;
    public class CreateCustomerTest
    {


    [Test]
    public async Task ShouldCreateCustomer()
    {
        var command = new CreateCustomerCommand
        {
            BankAccountNumber = "123456789",
            DateOfBirth=DateTime.Now,
            Email="kimi@yahoo.com",
            FirstName="kimi",
            LastName="hd",
            PhoneNumber="9920220490"
        };

        var item= await SendAsync(command);
        item.Should().NotBeNull();
        item.PhoneNumber.Should().Be(command.PhoneNumber);
    }
}
