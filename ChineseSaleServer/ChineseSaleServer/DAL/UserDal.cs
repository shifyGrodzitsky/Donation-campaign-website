using ChineseSaleServer.Dal;
using ChineseSaleServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ChineseSaleServer.DAL
{
    public class UserDal : IUserDal
    {
        private readonly ChineseSaleContext _chineseSaleContext;
        public UserDal(ChineseSaleContext chineseSaleContext)
        {
                this._chineseSaleContext = chineseSaleContext ?? throw new ArgumentNullException(nameof(chineseSaleContext));
        }


        //get
        public async Task<List<User>> GetAsync()
        {
            return await _chineseSaleContext.Users.ToListAsync();
        }
        //getById
        public async Task<User> GetByIdAsync(int id)
        {
            return await _chineseSaleContext.Users.FindAsync(id);
        }

        //delete
        public async Task<bool> DeleteByIdAsync(int id)
        {
            var user = await _chineseSaleContext.Users.FindAsync(id);
            if (user == null)
            {
                return false; // אם המתנה לא נמצאה במסד הנתונים
            }

            _chineseSaleContext.Users.Remove(user);
            await _chineseSaleContext.SaveChangesAsync();
            return true; // המתנה נמחקה בהצלחה
        }

        //add
        public async Task AddUserAsync(User newUser)
        {
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password); // Hash the user's password
            _chineseSaleContext.Users.Add(newUser);
            await _chineseSaleContext.SaveChangesAsync();

        }

        //login
        public async Task<User> LoginAsync(string email, string password)
        {
            var user = await _chineseSaleContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null; // Return null if user not found or password is incorrect
            }
            return user;
        }





            //update
            public async Task<bool> UpdateUserAsync(User updatedUser)
        {
            var existingUser = await _chineseSaleContext.Users.FindAsync(updatedUser.Id);
            if (existingUser == null)
            {
                return false; // אם המתנה לא נמצאה במסד הנתונים
            }

            existingUser.FirstName = updatedUser.FirstName;
            existingUser.LastName = updatedUser.LastName;
            existingUser.Password = updatedUser.Password;
            existingUser.Email = updatedUser.Email;
            existingUser.Address= updatedUser.Address;
            existingUser.Phone = updatedUser.Phone;

            await _chineseSaleContext.SaveChangesAsync();
            return true; // המתנה עודכנה בהצלחה
        }

    }



}
