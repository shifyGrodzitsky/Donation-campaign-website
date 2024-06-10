using ChineseSaleServer.Models;

namespace ChineseSaleServer.DAL
{
    public interface IUserDal
    {
        Task<List<User>> GetAsync();
        Task<User> GetByIdAsync(int id);
        Task<bool> DeleteByIdAsync(int id);
        Task AddUserAsync(User user);
        Task<bool> UpdateUserAsync(User updatedUser);
        Task<User> LoginAsync(string email, string password);


    }
}
