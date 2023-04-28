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
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<UserDto>>> GetAllUsers()
        {
            var response = new ServiceResponse<List<UserDto>>();
            var dbUsers = await _context.Users.ToListAsync();
            response.Data = dbUsers.Select(u => _mapper.Map<UserDto>(u)).ToList();
            return response;
        }
    }
}