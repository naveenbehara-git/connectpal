using Microsoft.AspNetCore.Components;
using ProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServerWeb.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;

        public UserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task DeleteUser(int UserId)
        {
            await httpClient.DeleteAsync($"api/User/{UserId}");
        }

        public async Task<User> GetUser(int UserId)
        {
            return await httpClient.GetJsonAsync<User>($"api/User/{UserId}");
        }

        public async Task<User> Search(string name)
        {
            try
            {
                var result = await httpClient.GetJsonAsync<User>($"api/User/Search/{name}");
                if (result.UserId != 0)
                {
                    return result;
                }
                return null;
            }
           catch(Exception)
            {
                return null;
            }
        }

        public async Task<User> UpdateUser(User updatedUser)
        {
            try
            {
                return await httpClient.PutJsonAsync<User>("api/User", updatedUser);
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
