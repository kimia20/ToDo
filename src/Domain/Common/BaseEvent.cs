﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ToDo.Domain.Common
{
    public abstract class BaseEvent : INotification
    {
    }
}
