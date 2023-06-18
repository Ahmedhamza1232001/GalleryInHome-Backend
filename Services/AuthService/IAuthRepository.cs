using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proj.Dtos.User;

namespace Proj.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<GetUserDto>> Register(User user, string password);
        Task<ServiceResponse<GetUserDto>> Login(string username, string password);
        Task<bool> UserExist(string username);
    }
}