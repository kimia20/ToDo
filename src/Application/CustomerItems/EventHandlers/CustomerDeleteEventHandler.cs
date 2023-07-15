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
    public class CustomerDeleteEventHandler : INotificationHandler<CustomerDeleteEvent>
    {
        private readonly ILogger<CustomerDeleteEventHandler> _logger;

        public CustomerDeleteEventHandler(ILogger<CustomerDeleteEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(CustomerDeleteEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("ToDo Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}

