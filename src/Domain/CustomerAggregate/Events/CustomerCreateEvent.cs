using ToDo.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Domain.CustomerAggregate.Events
{
    public class CustomerCreateEvent : BaseEvent
    {
        public CustomerCreateEvent(CustomerItem item)
        {
            Item = item;
        }

        public CustomerItem Item { get; }
    }
}

