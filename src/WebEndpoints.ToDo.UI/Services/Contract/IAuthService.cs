using ToDo.Domain.UserAggregate.Dto;
using WebEndpoints.ToDo.UI.Models;

namespace WebEndpoints.ToDo.UI.Services.Contract
{
    public interface IAuthService
    {
        Task<CurrentUser?> CurrentUserInfo();
        Task<LoginResult> Login(LoginRequest loginRequest);
    }
}
