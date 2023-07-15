using Application.Common.Interfaces;
using AutoMapper;
using Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using RsjFramework.Entities.GatewayResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Common;
using ToDo.Domain.UserAggregate.Dto;

namespace Infrastructure.Identity.Repository
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        public ApplicationUserRepository(UserManager<ApplicationUser> userManager, IMapper mapper, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        public async Task<UserDto> FindByEmail(string email) =>
           _mapper.Map<UserDto>(await _userManager.FindByEmailAsync(email));

        public async Task<UserDto> FindByUserName(string userName) =>
          _mapper.Map<UserDto>(await _userManager.FindByNameAsync(userName));

        public async Task<UserResponseDto> Create(UserDto user, string password, string role)
        {
            var appUser = _mapper.Map<ApplicationUser>(user);
            var identityResult = await _userManager.CreateAsync(appUser, password);
            var r = identityResult.Succeeded
                        ? null
                        : identityResult.Errors.Select(e => new Error(e.Code, e.Description));

            if (!identityResult.Succeeded)
                return new UserResponseDto(identityResult.Succeeded,
                    identityResult.Succeeded
                        ? null
                        : identityResult.Errors.Select(e => new Error(e.Code, e.Description)));

            //await _userManager.AddClaimsAsync(appUser, user.UserClaims.Select(o => new Claim(o.Type, o.Value)));

            await _userManager.AddToRoleAsync(appUser, role);
            return new UserResponseDto(identityResult.Succeeded, identityResult.Succeeded ? null :
                identityResult.Errors.Select(e => new Error(e.Code, e.Description)));
        }

        public async Task<IList<string>> GetUserRoles(Guid userId) =>
            await _userManager.GetRolesAsync(new ApplicationUser { Id = userId });


        public async Task<SignInResponseDto> PasswordSignIn(string userName, string password, bool rememberMe)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, rememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
                return new SignInResponseDto(SignInResponseStatus.Success, result.Succeeded);

            return new SignInResponseDto(SignInResponseStatus.WrongUserNameOrPassword, result.Succeeded);

        }
        public async Task SignOut() =>
           await _signInManager.SignOutAsync();
    }
}
