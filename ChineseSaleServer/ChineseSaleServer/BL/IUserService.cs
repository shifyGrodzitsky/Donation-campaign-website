using ChineseSaleServer.Models;

namespace ChineseSaleServer.BL
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task<bool> DeleteUserById(int userId);
        Task AddUser(User user);
        Task <bool> UpdateUser(User user);
        
    }
}




