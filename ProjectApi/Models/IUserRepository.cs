using ProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApi.Models
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> Search(string name);
        Task<User> GetUser(int UserId);
        Task<User> UpdateUser(User user);
        Task<User> DeleteUser(int UserId);


    }
}
