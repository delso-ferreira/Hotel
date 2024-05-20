using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class HotelRepository : IHotelRepository
    {
        protected readonly ITrybeHotelContext _context;
        public HotelRepository(ITrybeHotelContext context)
        {
            _context = context;
        }
        
        public IEnumerable<HotelDto> GetHotels()
        {
            var getAllHotels = _context.Hotels.Select(h => new HotelDto {
                HotelId = h.HotelId,
                Name = h.Name,
                Address = h.Address,
                CityId = h.CityId,
                CityName = h.City.Name != null ? h.City.Name : null,
                State = h.City.State

            }).ToList();

            return getAllHotels;
        }        
        
        public HotelDto AddHotel(Hotel hotel)
        {
            var city = _context.Cities.FirstOrDefault(c => c.CityId == hotel.CityId) ?? throw new Exception("Sorry, City not found!");

            var postHotel = new Hotel {
                Name = hotel.Name,
                Address = hotel.Address,
                CityId = hotel.CityId,              
            };

            _context.Hotels.Add(postHotel);
            _context.SaveChanges();

            var newHotel = new HotelDto {
                HotelId = postHotel.HotelId,
                Name = postHotel.Name,
                Address = postHotel.Address,
                CityId = postHotel.CityId,
                CityName = city.Name,
                State = city.State
            };

            return newHotel;            
        }
    }
}