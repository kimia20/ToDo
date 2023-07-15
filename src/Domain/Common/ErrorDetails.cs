﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Domain.Common
{
    public class ErrorDetails
    {

        public string Title { get; private set; }
        public int? Status { get; private set; }
        public string Instance { get; private set; }
        public IEnumerable<Error> Errors { get; private set; }

        public ErrorDetails(int? status, string title, string instance, IEnumerable<Error> errors = null)
        {
            Title = title;
            Status = status;
            Instance = instance;
            Errors = errors;
        }
        public void SetErrors(IEnumerable<Error> errors) => Errors = errors;
        public void SetErrors(string error) => Errors = new Error[] { new Error(error) };
    }
    public class Error
    {
        public string Message { get; }
        public string Info { get; }
        public Error(string message, string info)
        {
            Message = message;
            Info = info;
        }
        public Error(string message)
        {
            Message = message;
        }
    }
}
