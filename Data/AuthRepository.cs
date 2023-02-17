using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Proj.Data
{
    public class AuthRepository : IAuthRepository
    {
        public DataContext Context { get; }
        public AuthRepository(DataContext context)
        {
            this.Context = context;

        }
        public Task<ServiceResponse<string>> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            ServiceResponse<int> response = new ServiceResponse<int>();
            if (await UserExist(user.UserName))
            {
                response.Success = false;
                response.Message = "User aleready exists.";
                return response;
            }

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            this.Context.Users.Add(user);
            await this.Context.SaveChangesAsync();

            response.Data = user.Id;
            return response;
        }

        private void CreatePasswordHash(string password, out byte[] passowrdHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passowrdHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExist(string username)
        {
            if (await this.Context.Users.AnyAsync(u => u.UserName.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }
    }
}