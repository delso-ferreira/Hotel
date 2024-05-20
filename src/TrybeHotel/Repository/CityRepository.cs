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

        // 4. Refatore o endpoint GET /city
        public IEnumerable<CityDto> GetCities()
        {

            var allCities = _context.Cities.Select(c => new CityDto {
                CityId = c.CityId,
                Name = c.Name,
                State = c.State
            }).ToList();

            return allCities;            
        }

        // 2. Refatore o endpoint POST /city
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

        // 3. Desenvolva o endpoint PUT /city
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