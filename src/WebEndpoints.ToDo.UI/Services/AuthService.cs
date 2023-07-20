using System.Net.Http.Json;
using ToDo.Domain.UserAggregate.Dto;
using WebEndpoints.ToDo.UI.Models;
using WebEndpoints.ToDo.UI.Services.Contract;
namespace WebEndpoints.ToDo.UI.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<CurrentUser> CurrentUserInfo()
        {
            var result = await _httpClient.GetFromJsonAsync<CurrentUser>("api/Account/CurrentUserInfo");
            return result;
        }
        public async Task<LoginResult> Login(LoginRequest loginRequest)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Account/Login", loginRequest);
            //if (result.IsSuccessStatusCode)
            //{
                var response = await result.Content.ReadFromJsonAsync<LoginResult>();
                return response;
                    //await jsr.InvokeVoidAsync("localStorage.setItem", "user", $"{result.email};{result.jwtBearer}").ConfigureAwait(false);
            //}

            //if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            //result.EnsureSuccessStatusCode();
        }
    }
}
