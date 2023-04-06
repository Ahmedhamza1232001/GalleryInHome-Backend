using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Proj.Dtos.User;

namespace Proj.Data
{
    //test comment on test branch 
    public class AuthRepository : IAuthRepository
    {
        public DataContext Context { get; }
        public IConfiguration Configuration { get; }
        public AuthRepository(DataContext context, IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.Context = context;

        }
        public async Task<ServiceResponse<object>> Login(string email, string password)
        {
            var response = new ServiceResponse<object>();
            var user = await this.Context.Users
            .FirstOrDefaultAsync(u => u.Email.ToLower().Equals(email.ToLower()));
            if (user == null) //need to modfiy this to be an exception
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password.";
            }
            else
            {
                UserDto userDto = new();
                string tokenCreated = CreateToken(user);

                userDto.Email = user.Email;
                userDto.UserName = user.UserName;
                userDto.Token = tokenCreated;

                response.Data = userDto;

            }
            return response;

        }

        public async Task<ServiceResponse<List<object>>> Register(User user, string password)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            ServiceResponse<List<object>> response = new();
            string tokenCreated = CreateToken(user);
            if (await UserExist(user.Email)) //Email instade of username
            {
                response.Success = false;
                response.Message = "User aleready exists.";
                return response;
            }

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            this.Context.Users.Add(user);
            await this.Context.SaveChangesAsync();
            _ = new UserDto()
            {
                Email = user.Email,
                UserName = user.UserName,
                Password = string.Empty
            };



            response.Data = new List<object> { user, tokenCreated };
            return response;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);
            var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computeHash.SequenceEqual(passwordHash);
        }

        public async Task<bool> UserExist(string email)
        {
            if (await this.Context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower()))
            {
                return true;
            }
            return false;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var appSettingsToken = this.Configuration.GetSection("AppSettings:Token").Value;
            if (appSettingsToken is null)
                throw new Exception("AppSettings Token is null!");

            SymmetricSecurityKey key = new(System.Text.Encoding.UTF8.GetBytes(appSettingsToken));

            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);




            return tokenHandler.WriteToken(token);
        }

    }
}