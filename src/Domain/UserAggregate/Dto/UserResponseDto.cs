using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Common;

namespace ToDo.Domain.UserAggregate.Dto
{
    public class UserResponseDto
    {
        public UserResponseDto( bool success = false, IEnumerable<Error> errors = null)
        {
            Success=success;
            Errors=errors;
        }
        public bool Success { get; }

        public IEnumerable<Error> Errors { get; }
    }
}
