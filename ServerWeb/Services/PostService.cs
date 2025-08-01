using Microsoft.AspNetCore.Components;
using ProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServerWeb.Services
{
    public class PostService :IPostService
    {
        private readonly HttpClient httpClient;

        public PostService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Post> AddPost(Post newPost)
        {
            return await httpClient.PostJsonAsync<Post>("api/Post", newPost);
        }

        

        public async Task DeletePost(int postid)
        {
             await httpClient.DeleteAsync($"api/Post/{postid}");
        }

        public async  Task<IEnumerable<Post>> GetUserPosts(int userid)
        {
            return await httpClient.GetJsonAsync<Post[]>($"api/Post/{userid}");
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await httpClient.GetJsonAsync<Post[]>("api/Post");
        }

        public async Task<Post> Search(int id)
        {
            return await httpClient.GetJsonAsync<Post>($"api/Post/Search/{id}");
        }

        public async Task<Post> UpdatePost(Post updatedPost)
        {
            return await httpClient.PutJsonAsync<Post>("api/Post", updatedPost);
        }

    }
}
