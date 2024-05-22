using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("city")]
    public class CityController : Controller
    {
        private readonly ICityRepository _repository;
        public CityController(ICityRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        public IActionResult GetCities(){
            var findAllCities = _repository.GetCities();
            return Ok(findAllCities);
        }

        [HttpPost]
        public IActionResult PostCity([FromBody] City city){
            var insertCity = _repository.AddCity(city);
            return Created("", insertCity);
        }
        
        
        [HttpPut]
        public IActionResult PutCity([FromBody] City city){
            var updateCity = _repository.UpdateCity(city);
            return Ok(updateCity);
        }
    }
}