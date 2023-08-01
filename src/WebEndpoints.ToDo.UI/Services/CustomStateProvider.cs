using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;
using ToDo.Domain.UserAggregate.Dto;
using WebEndpoints.ToDo.UI.Models;
using WebEndpoints.ToDo.UI.Services.Contract;

namespace WebEndpoints.ToDo.UI.Services
{
    public class CustomStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthService api;
        private readonly ILocalStorageService _localStorageService;
        private CurrentUser _currentUser;
        public CustomStateProvider(IAuthService api,
            ILocalStorageService localStorageService)
        {
            this.api = api;
            _localStorageService = localStorageService;
        }
        public async Task<LoginResult> Login(LoginRequest loginParameters)
        {
            var result=await api.Login(loginParameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return result;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var jwtToken = await _localStorageService.GetItemAsStringAsync("jwt-access-token");
            //if (string.IsNullOrEmpty(jwtToken))
            {
                return new AuthenticationState(
                    new ClaimsPrincipal(new ClaimsIdentity()));
            }
            return new AuthenticationState(
                    new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(jwtToken), "jwtAuth")));
        }
        public void NotifyAuthState()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];

            var jsonBytes = ParseBase64WithoutPadding(payload);

            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
            return claims;
        }
        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
        //public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        //{
        //    var identity = new ClaimsIdentity();
        //    try
        //    {
        //        var userInfo = await GetCurrentUser();
        //        if (userInfo.IsAuthenticated)
        //        {
        //            var claims = new[] { new Claim(ClaimTypes.Name, _currentUser.UserName) }.Concat(_currentUser.Claims.Select(c => new Claim(c.Key, c.Value)));
        //            identity = new ClaimsIdentity(claims, "Server authentication");
        //        }
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        Console.WriteLine("Request failed:" + ex.ToString());
        //    }
        //    return new AuthenticationState(new ClaimsPrincipal(identity));
        //}
        private async Task<CurrentUser> GetCurrentUser()
        {
            if (_currentUser != null && _currentUser.IsAuthenticated) return _currentUser;
           _currentUser = await api.CurrentUserInfo();
            return _currentUser;
        }
    }
}