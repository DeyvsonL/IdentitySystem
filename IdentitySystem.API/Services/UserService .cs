using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using IdentitySystem.API.DAL;
using IdentitySystem.API.Models;

namespace IdentitySystem.API.Services
{
    public class UserService
    {

        private readonly IdentitySystemDbContext _systemDbContext;

        public UserService(IdentitySystemDbContext dbContext)
        {
            _systemDbContext = dbContext;
        }

        public async Task<User> SignUp(User user)
        {
            ValidateUser(user);
            user.CreatedAt = DateTime.Now;
            user.LastLogin = user.CreatedAt;
            await _systemDbContext.Users.AddAsync(user);
            await _systemDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> SignIn(string email, string password)
        {
            var user = await _systemDbContext.Users.FirstOrDefaultAsync(user => user.Email.Equals(email) && user.Password.Equals(password));
            if (user == null)
                throw new Exception("Invalid e-mail or password");
            user.LastLogin = DateTime.Now;
            await _systemDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> Get(long id)
        {
            var user = await _systemDbContext.Users
                .Include(user => user.Phones)
                .FirstAsync(user => user.Id == id);
            return user;
        }

        private void ValidateUser(User user)
        {
            if (_systemDbContext.Users.Any(u => u.Email.Equals(user.Email, StringComparison.InvariantCultureIgnoreCase)))
                throw new Exception("E-mail already exists");
        }

    }
}
