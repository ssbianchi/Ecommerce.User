using Ecommerce.User.Application.User.Dto;

namespace Ecommerce.User.Application.User
{
    public interface IUserService
    {
        Task<UserDto> GetUser(int userId);
        Task<List<UserDto>> GetAllUsers();
        Task<UserDto> SaveUser(UserDto userDto);
        Task<bool> DeleteUser(int userdId);
    }
}
