using TrybeHotel.Models;
using TrybeHotel.Dto;
using Microsoft.EntityFrameworkCore;

namespace TrybeHotel.Repository
{
    public class RoomRepository : IRoomRepository
    {
        protected readonly ITrybeHotelContext _context;
        public RoomRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 6. Desenvolva o endpoint GET /room/:hotelId
        public IEnumerable<RoomDto> GetRooms(int HotelId)
        {
            try
            {
                var getAllRooms = _context.Rooms.Where(rm => rm.HotelId == HotelId)
                
                .Select(rm => new RoomDto
                {
                    RoomId = rm.RoomId,
                    Name = rm.Name,
                    Capacity = rm.Capacity,
                    Image = rm.Image,
                    Hotel = new HotelDto
                    {
                        HotelId = rm.Hotel.HotelId,
                        Name = rm.Hotel.Name,
                        Address = rm.Hotel.Address,
                        CityId = rm.Hotel.CityId,
                        CityName = rm.Hotel.City.Name,
                        State = rm.Hotel.City.State
                    }
                }).ToList();

                return getAllRooms;

            }
            catch (Exception e)
            {
                throw new Exception("Ops, quartos não encontrados!", e);
            }
        }

        // 7. Desenvolva o endpoint POST /room
        public RoomDto AddRoom(Room room)
        {
               try
            {
                _context.Rooms.Add(room);
                _context.SaveChanges();

                var newRoom = _context.Rooms
                    .Include(rm => rm.Hotel)
                    .ThenInclude(h => h.City)
                    .FirstOrDefault(rm => rm.RoomId == room.RoomId);

                var roomDto = newRoom ?? throw new Exception("Ops, não encontramos o quarto que acabou de ser adicionado!");

                return new RoomDto
                {
                    RoomId = roomDto.RoomId,
                    Name = roomDto.Name,
                    Capacity = roomDto.Capacity,
                    Image = roomDto.Image,
                    Hotel = new HotelDto
                    {
                        HotelId = roomDto.Hotel.HotelId,
                        Name = roomDto.Hotel.Name,
                        Address = roomDto.Hotel.Address,
                        CityId = roomDto.Hotel.CityId,
                        CityName = roomDto.Hotel.City?.Name,
                        State = roomDto.Hotel.City?.State
                    }
                };
            }
            catch (Exception e)
            {
                throw new Exception("Ops, algo deu errado ao tentar adicionar os quartos!", e);
            }
        }

        // 8. Desenvolva o endpoint DELETE /room/:roomId
        public void DeleteRoom(int RoomId)
        {
               try
            {
                var deleteRoom = _context.Rooms.Find(RoomId);
                if (deleteRoom != null)
                {
                    _context.Rooms.Remove(deleteRoom);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Ops, algo deu errado ao tentar excluir o quarto", e);
            }
        }
    }
}