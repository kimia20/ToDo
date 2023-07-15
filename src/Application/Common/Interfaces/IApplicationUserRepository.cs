using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.UserAggregate.Dto;

namespace Application.Common.Interfaces
{
    public interface IApplicationUserRepository
    {
        Task<UserDto> FindByEmail(string email) ;
        Task<UserDto> FindByUserName(string userName);
        Task<UserResponseDto> Create(UserDto user, string password, string role);
        Task<IList<string>> GetUserRoles(Guid userId);
        Task<SignInResponseDto> PasswordSignIn(string userName, string password, bool rememberMe);
        Task SignOut();
     }
}
