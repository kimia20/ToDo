using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Common;

namespace ToDo.Domain.UserAggregate.Dto
{
    public class SignInResponseDto:UserResponseDto
    {
        public SignInResponseStatus Status { get; }
        public SignInResponseDto(SignInResponseStatus status, bool success = false, IEnumerable<Error> errors = null)
            : base(success, errors)
        {
            Status = status;
        }
    }
    public enum SignInResponseStatus
    {
        Success,
        [Display(Description = "Your email has not been verified.")]
        EmailNotConfirmed,
        [Display(Description = "Your MobileNo has not been verified.")]
        PhoneNotConfirmed,
        [Display(Description = "Username or password is incorrect!")]
        WrongUserNameOrPassword
    }
}
