using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using ToDo.Domain.UserAggregate.Dto;
using WebEndpoints.ToDo.UI.Models;
using WebEndpoints.ToDo.UI.Services.Contract;

namespace WebEndpoints.ToDo.UI.Services
{
    public class CustomStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthService api;
        private CurrentUser _currentUser;
        public CustomStateProvider(IAuthService api)
        {
            this.api = api;
        }
        public async Task<LoginResult> Login(LoginRequest loginParameters)
        {
            var result=await api.Login(loginParameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return result;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            try
            {
                var userInfo = await GetCurrentUser();
                if (userInfo.IsAuthenticated)
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, _currentUser.UserName) }.Concat(_currentUser.Claims.Select(c => new Claim(c.Key, c.Value)));
                    identity = new ClaimsIdentity(claims, "Server authentication");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Request failed:" + ex.ToString());
            }
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
        private async Task<CurrentUser> GetCurrentUser()
        {
            if (_currentUser != null && _currentUser.IsAuthenticated) return _currentUser;
           _currentUser = await api.CurrentUserInfo();
            return _currentUser;
        }
    }
}