using ProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApi.Models
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetPosts();
        Task<IEnumerable<Post>> GetUserPosts(int userid);
        Task<Post> AddPost(Post post);
        Task<Post> DeletePost(int PostId);
        Task<Post> Search(int id);

        Task<Post> UpdatePost(Post post);
    }
}
