using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.Models;
using ProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController:ControllerBase
    {
        private readonly IPostRepository postRepository;

        public PostController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            try
            {
                return (await postRepository.GetPosts()).ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }



        [HttpPost]
        public async Task<ActionResult<Post>> AddPost(Post post)
        {
            try
            {
                if (post == null)
                    return BadRequest();

                var createdPost = await postRepository.AddPost(post);

                return createdPost;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }

        
        [HttpDelete("{postid:int}")]
        public async Task<ActionResult<Post>> DeletePost(int postid)
        {
            try
            {
                var postToDelete = await postRepository.Search(postid);

                if (postToDelete == null)
                {
                    return NotFound($"Post with Id = {postid} not found");
                }

                return await postRepository.DeletePost(postid);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

        [HttpGet("{userid:int}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetUserPosts(int userid)
        {
            try
            {
                return (await postRepository.GetUserPosts(userid)).ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("Search/{id}")]
        public async Task<ActionResult<Post>> Search(int id)
        {
            try
            {
                var result = await postRepository.Search(id);

                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPut()]
        public async Task<ActionResult<Post>> UpdatePost(Post post)
        {
            try
            {


                var userToUpdate = await postRepository.Search(post.PostId);

                if (userToUpdate == null)
                    return NotFound($"User with Id = {post.PostId} not found");

                return await postRepository.UpdatePost(post);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

    }
}

