using Application.Common.Exceptions;
using Application.CustomerItems.Commands.CreateCustomer;
using Application.CustomerItems.Commands.DeleteCustomer;
using Application.CustomerItems.Commands.UpdateCustomer;
using Application.CustomerItems.Queries.GetAllCustomer;
using Application.CustomerItems.Queries.GetCustomerById;
using ToDo.Domain.Common;
using ToDo.Domain.CustomerAggregate.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.Net;
using WebEndpoints.ToDo.Api.Controllers;

namespace Endpoints.ToDo.Api.Controllers
{
    [ApiController]
    [Route("api/Customer/")]
    [SwaggerTag("CustomerService")]
    public class CustomerController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CustomerItemDto), (int)HttpStatusCode.OK)]
        [SwaggerOperation("Create Customer")]
        public async Task<ActionResult<CustomerItemDto>> Create(CreateCustomerCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ErrorDetails),(int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesDefaultResponseType]
        [SwaggerOperation("Update Customer")]
        public async Task<IActionResult> Update(int id, UpdateCustomerCommand command)
        {
            command.Id = id;
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [SwaggerOperation("Delete Customer")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCustomerCommand(id));

            return NoContent();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation("Get Customer With Id")]
        public async Task<ActionResult<CustomerItemDto>> GetCustomerById([FromQuery] GetCustomerByIdQuery query)
        {
            return await Mediator.Send(query);
        }
        //WithoutPagination
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation("Get All Customer ")]
        public async Task<ActionResult<List<CustomerItemDto>>> GetAllCustomer()
        {
            return await Mediator.Send(new GetAllCustomerQuery());
        }
    }
}