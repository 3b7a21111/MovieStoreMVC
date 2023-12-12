using MovieStoreMVC.Models.DTO;

namespace MovieStoreMVC.Repository.Abstract
{
    public interface IUserAuthenticationService
    {
        Task<Status> RegisterAsync(RegisterModel model);
        Task <Status> LoginAsync (LoginModel model);
        Task LogoutAsync();
    }
}
