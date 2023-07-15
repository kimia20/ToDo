using ToDo.Domain.CustomerAggregate.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CustomerItems.EventHandlers
{
    public class CustomerCreateEventHandler : INotificationHandler<CustomerCreateEvent>
    {
        private readonly ILogger<CustomerCreateEventHandler> _logger;

        public CustomerCreateEventHandler(ILogger<CustomerCreateEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(CustomerCreateEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("ToDo Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}

