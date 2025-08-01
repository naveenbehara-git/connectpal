using Microsoft.EntityFrameworkCore;
using ProjectModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectApi.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<User> DeleteUser(int UserId)
        {
            var result = await appDbContext.Users
               .FirstOrDefaultAsync(e => e.UserId == UserId);
            if (result != null)
            {
                appDbContext.Users.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<User> GetUser(int UserId)
        {
            return await appDbContext.Users
                .Include(c=>c.Country)
                .Include(ci => ci.City)
                .FirstOrDefaultAsync(e => e.UserId == UserId);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await appDbContext.Users
                .Include(c => c.Country)
                .Include(ci => ci.City)
                .ToListAsync();
        }

        public async Task<User> Search(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var result=await appDbContext.Users
                .Include(c => c.Country)
                .Include(ci => ci.City)
                .FirstOrDefaultAsync(e => e.UserName == name);
                return result;
            }
            return null;
        }

        public async Task<User> UpdateUser(User user)
        {
            var result = await appDbContext.Users
               .FirstOrDefaultAsync(e => e.UserId == user.UserId);
            if (result != null)
            {

                result.UserEmail = user.UserEmail;
                result.UserName = user.UserName;
                result.MobileNumber = user.MobileNumber;
                result.DateOfBirth = user.DateOfBirth;
                result.CountryId = user.CountryId;
                result.CityID = user.CityID;
                result.Gender = user.Gender;
                result.Friends = user.Friends;
                result.Requests = user.Requests;
                result.ProfilePic = user.ProfilePic;


                await appDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
