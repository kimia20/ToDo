using Application.Common.Interfaces;
using Application.CustomerItems.Commands.CreateCustomer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.CustomerAggregate.Dto;
using ToDo.Domain.CustomerAggregate.Events;
using ToDo.Domain.CustomerAggregate;
using ToDo.Domain.UserAggregate.Dto;

namespace Application.ApplicationUsers.Command.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IApplicationUserRepository _repository;

        public CreateUserCommandHandler(IApplicationUserRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new UserDto
            {
                PhoneNumber = request.PhoneNumber,
                UserName = request.UserName,
                Email=request.Email
            };
            var result = await _repository.Create(user, request.Password, "NormalUser");
            if (!result.Success) throw new Exception(string.Join(",", result.Errors.Select(o => o.Message)));
        }
    }
}
