using ProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerWeb.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetPosts();
        Task<IEnumerable<Post>> GetUserPosts(int userid);
        Task<Post> AddPost(Post newPost);
        Task DeletePost(int postid);

        Task<Post> Search(int id);
        Task<Post> UpdatePost(Post post);

    }
}
