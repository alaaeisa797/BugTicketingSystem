using BugTicketingSystem.BL.DTOs.User;

namespace BugTicketingSystem.BL.Managers.Users
{
    public interface IUserManger
    {
        Task<GeneralResult> AddAsync(UsersRegisterDto userRegisterDTO);
        Task<GeneralResult<TokenDTO>> LogInAsync(LoginCredentials loginUser);
    }
}