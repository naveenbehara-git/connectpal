using Microsoft.EntityFrameworkCore;
using ProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApi.Models
{
    public class PostRepository:IPostRepository
    {
        private readonly AppDbContext appDbContext;

        public PostRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async  Task<Post> AddPost(Post post)
        {
            var result = await appDbContext.Posts.AddAsync(post);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async  Task<Post> DeletePost(int PostId)
        {
            var result = await appDbContext.Posts
               .FirstOrDefaultAsync(e => e.PostId == PostId);
            if (result != null)
            {
                appDbContext.Posts.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await appDbContext.Posts
                .Include(e=>e.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetUserPosts(int userid)
        {
            IQueryable<Post> posts = appDbContext.Posts;
            posts = posts.Where(e => e.UserId == userid);

            return await posts.ToListAsync();
        }

        public async Task<Post> Search(int id)
        {
            if (!string.IsNullOrEmpty((id).ToString()))
            {
                var result = await appDbContext.Posts
                .FirstOrDefaultAsync(e => e.PostId == id);
                return result;
            }
            return null;
        }

        public async Task<Post> UpdatePost(Post post)
        {

            var result = await appDbContext.Posts
              .FirstOrDefaultAsync(e => e.PostId == post.PostId);
            if (result != null)
            {


                result.PostId = post.PostId;
                result.Image = post.Image;
                result.ImageUrl = post.ImageUrl;
                result.Message = post.Message;
                result.UserId = post.UserId;
                result.TimeOfPost = post.TimeOfPost;


                await appDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }

    }
}
