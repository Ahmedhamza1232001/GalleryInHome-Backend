using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<List<object>>> Register(User user, string password);
        Task<ServiceResponse<object>> Login(string username, string password);
        Task<bool> UserExist(string username);
    }
}