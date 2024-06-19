using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class CityRepository : ICityRepository
    {
        protected readonly ITrybeHotelContext _context;
        public CityRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        
        public IEnumerable<CityDto> GetCities()
        {

            var allCities = _context.Cities.Select(c => new CityDto {
                CityId = c.CityId,
                Name = c.Name,
                State = c.State
            }).ToList();

            return allCities;            
        }

        public CityDto GetCityById(int id)
        {
            var city = _context.Cities.Find(id);
            return new CityDto {
                CityId = city.CityId,
                Name = city.Name,
                State = city.State
            };
        }

        
        public CityDto AddCity(City city)
        {
            _context.Cities.Add(city);
            _context.SaveChanges();

            return new CityDto {
                CityId = city.CityId,
                Name = city.Name,
                State = city.State

            };
        }

        
        public CityDto UpdateCity(City city)
        {
            var cityToUpdate = _context.Cities.Find(city.CityId);
            cityToUpdate.Name = city.Name;
            cityToUpdate.State = city.State;
            _context.SaveChanges();

            return new CityDto {
                CityId = city.CityId,
                Name = city.Name,
                State = city.State
            };
        }

    }
}