using Microsoft.AspNetCore.Mvc;

//init

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("/status")]
    public class StatusController : Controller
    {
    
        [HttpGet]
        public IActionResult GetStatus()
        {
            return Ok(new { message = "online" });
        }


    }
}
