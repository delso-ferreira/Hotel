using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;
using TrybeHotel.Dto;
using TrybeHotel.Services;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("login")]

    public class LoginController : Controller
    {

        private readonly IUserRepository _repository;
        public LoginController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDto login){
           try
            {
                var user = _repository.Login(login);
                var generator = new TokenGenerator();
                var token = generator.Generate(user);
                return Ok(new { token = token });
            }
            catch (Exception)
            {
                return Unauthorized(new { message = "Incorrect e-mail or password" });
            }
        }

        [HttpGet]
        public IActionResult Login([FromQuery] string email)
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
    }
    }