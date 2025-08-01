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
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                return (await userRepository.GetUsers()).ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


        [HttpGet("{UserId:int}")]
        public async Task<ActionResult<User>> GetUser(int UserId)
        {
            try
            {
                var result = await userRepository.GetUser(UserId);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPut()]
        public async Task<ActionResult<User>> UpdateUser(User user)
        {
            try
            {


                var userToUpdate = await userRepository.GetUser(user.UserId);

                if (userToUpdate == null)
                    return NotFound($"User with Id = {user.UserId} not found");

                return await userRepository.UpdateUser(user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            try
            {
                var employeeToDelete = await userRepository.GetUser(id);

                if (employeeToDelete == null)
                {
                    return NotFound($"User with Id = {id} not found");
                }

                return await userRepository.DeleteUser(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
        

        [HttpGet("Search/{name}")]
        public async Task<ActionResult<User>> Search(string name)
        {
            try
            {
                var result = await userRepository.Search(name);

                if (result != null)
                {
                    return Ok(result);
                }

                return null;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


    }
}
