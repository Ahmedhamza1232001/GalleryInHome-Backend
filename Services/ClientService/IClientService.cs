using System.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proj.Dtos.User;

namespace Proj.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<List<UserDto>>> GetAllUsers();
    }
}