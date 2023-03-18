using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Proj.Data;
using Proj.Dtos.User;

namespace Proj.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IMapper Mapper;
        private readonly DataContext Context;
        private readonly IHttpContextAccessor HttpContextAccessor;
        public UserService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.HttpContextAccessor = httpContextAccessor;
            this.Context = context;
            this.Mapper = mapper;
        }
        public async Task<ServiceResponse<List<UserDto>>> GetAllUsers()
        {
            var response = new ServiceResponse<List<UserDto>>();
            var dbUsers = await this.Context.Users.ToListAsync();
            response.Data = dbUsers.Select(u => this.Mapper.Map<UserDto>(u)).ToList();
            return response;
        }
    }
}