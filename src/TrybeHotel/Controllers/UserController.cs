using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;
using TrybeHotel.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("user")]

    public class UserController : Controller
    {
        private readonly IUserRepository _repository;
        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "Admin")]
        public IActionResult GetUsers(){
            
            try
            {
                var users = _repository.GetUsers();
                return Ok(users);
            }
            catch (Exception e)
            {
                return Unauthorized(new { message = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetUserByEmail([FromQuery] string email)
        {
            try
            {
                var user = _repository.GetUserByEmail(email);
                return Ok(user);
            }
            catch (Exception e)
            {
                return NotFound(new { message = e.Message });
            }
        }

        [HttpPost]        
        public IActionResult Add([FromBody] UserDtoInsert user)
        {
            try
            {
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

                var newUserWithHash = new UserDtoInsert
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = hashedPassword
                };

                var addUser = _repository.Add(newUserWithHash);
                return Created("", newUserWithHash);
            }
            catch (Exception e)
            {
                if (e.Message == "User email already exists")
                {
                    return Conflict(new { message = "User email already exists" });
                }
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult GetUserById([FromQuery] int userId)
        {
            try
            {
                var user = _repository.GetUserById(userId);
                return Ok(user);
            }
            catch (Exception e)
            {
                return NotFound(new { message = e.Message });
            }
        }
        
    }
}