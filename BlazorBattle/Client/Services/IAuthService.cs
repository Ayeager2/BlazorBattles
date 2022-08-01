using BlazorBattle.Shared;

namespace BlazorBattle.Client.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserRegister request);
    }
}
