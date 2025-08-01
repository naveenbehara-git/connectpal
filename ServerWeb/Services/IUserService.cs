using ProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerWeb.Services
{
    public interface IUserService
    {
        Task<User> Search(string name);
        Task<User> GetUser(int UserId);
        Task<User> UpdateUser(User user);
        Task DeleteUser(int UserId);
    }
}
