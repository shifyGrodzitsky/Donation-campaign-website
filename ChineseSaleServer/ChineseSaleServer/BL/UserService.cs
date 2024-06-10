using ChineseSaleServer.DAL;
using ChineseSaleServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChineseSaleServer.BL
{
    public class UserService : IUserService
    {
        private readonly IUserDal _userDal;

        public UserService(IUserDal userDal)
        {
            this._userDal = userDal?? throw new ArgumentNullException(nameof(DonorDal));
        }

        public async Task AddUser(User user)
        {
           await _userDal.AddUserAsync(user);
        }

        public async Task<bool> DeleteUserById(int userId)
        {
            return await _userDal.DeleteByIdAsync(userId);
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userDal.GetByIdAsync(id);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _userDal.GetAsync();
        }

        public async Task<bool> UpdateUser(User user)
        {
            return await _userDal.UpdateUserAsync(user);
        }
    }
}
