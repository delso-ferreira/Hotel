using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("room")]    
    public class RoomController : Controller
    {
        private readonly IRoomRepository _repository;
        public RoomController(IRoomRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{HotelId}")]
        public IActionResult GetRoom(int HotelId)
        {
            var getAllRooms = _repository.GetRooms(HotelId);
            return Ok(getAllRooms);
        }

        [HttpPost] 
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]       
        [Authorize(Policy = "Admin")]
        public IActionResult PostRoom([FromBody] Room room)
        {
            var postNewRoom = _repository.AddRoom(room);
            return Created("", postNewRoom);
        }

        [HttpDelete("{RoomId}")]   
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]     
        [Authorize(Policy = "Admin")]
        public IActionResult Delete(int RoomId)
        {
            try
            {
                _repository.DeleteRoom(RoomId);
                return NoContent();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}