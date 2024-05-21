using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TrybeHotel.Dto;

//teste commit

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("booking")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  
    public class BookingController : Controller
    {
        private readonly IBookingRepository _repository;
        public BookingController(IBookingRepository repository)
        {
            _repository = repository;
        }

        // teste

        [HttpPost]        
        [Authorize(Policy = "Client")]
        public IActionResult Add([FromBody] BookingDtoInsert bookingInsert){
            
            var token = HttpContext.User.Identity as ClaimsIdentity;
            var email = token?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            
            try 
            {
             var Booking = _repository.Add(bookingInsert, email);
             return Created("", Booking);

            } catch (Exception e)
            {
              return BadRequest(new { message = e.Message });
            }


        }


        [HttpGet("{Bookingid}")]        
        [Authorize(Policy = "Client")]
        public IActionResult GetBooking(int Bookingid){
           
           var token = HttpContext.User.Identity as ClaimsIdentity;
           var email = token?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

           try
           {
            var getOneBooking = _repository.GetBooking(Bookingid, email);
            return Ok(getOneBooking);

           } catch (Exception e)
           {
            return Unauthorized(new { message = e.Message });
           }  
        }
    }
}