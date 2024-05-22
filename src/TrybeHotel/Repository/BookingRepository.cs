using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class BookingRepository : IBookingRepository
    {
        protected readonly ITrybeHotelContext _context;
        public BookingRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public BookingResponse Add(BookingDtoInsert booking, string email)
        {
            try {


                var getRoom = _context.Rooms.Find(booking.RoomId);

                if(getRoom.Capacity < booking.GuestQuant)                
                {
                throw new Exception("Guest quantity over room capacity");
                }

                var user = _context.Users.Where(e => e.Email == email).First();

                var newbooking = new Booking
                {
                    CheckIn = booking.CheckIn,
                    CheckOut = booking.CheckOut,
                    GuestQuant = booking.GuestQuant,
                    UserId = user.UserId,
                    RoomId = booking.RoomId,
                };
                
                _context.Bookings.Add(newbooking);
                _context.SaveChanges();
               
                
                var addBokkings = _context.Bookings.Where(b => b.UserId == user.UserId).Select(b => new BookingResponse
                {
                    BookingId = b.BookingId,
                    CheckIn = b.CheckIn,
                    CheckOut = b.CheckOut,
                    GuestQuant = b.GuestQuant,
                    Room = new RoomDto
                    {
                        RoomId = b.Room.RoomId,
                        Name = b.Room.Name,
                        Capacity = b.Room.Capacity,
                        Image = b.Room.Image,
                        Hotel = new HotelDto
                        {
                            HotelId = b.Room.Hotel.HotelId,
                            Name = b.Room.Hotel.Name,
                            Address = b.Room.Hotel.Address,
                            CityId = b.Room.Hotel.CityId,
                            CityName = b.Room.Hotel.City.Name,
                            State = b.Room.Hotel.City.State
                        }
                    }
                }).ToList();

                return addBokkings.Last();          
                

            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }

        public BookingResponse GetBooking(int bookingId, string email)
        {
            
            var user = _context.Users.Where(e => e.Email == email).First();
            
            var getBooking = _context.Bookings.Where(b => b.BookingId == bookingId && b.UserId == user.UserId)
            .Select(b => new BookingResponse
            {
                BookingId = b.BookingId,
                CheckIn = b.CheckIn,
                CheckOut = b.CheckOut,
                GuestQuant = b.GuestQuant,
                Room = new RoomDto
                {
                    RoomId = b.Room.RoomId,
                    Name = b.Room.Name,
                    Capacity = b.Room.Capacity,
                    Image = b.Room.Image,
                    Hotel = new HotelDto
                    {
                        HotelId = b.Room.Hotel.HotelId,
                        Name = b.Room.Hotel.Name,
                        Address = b.Room.Hotel.Address,
                        CityId = b.Room.Hotel.CityId,
                        CityName = b.Room.Hotel.City.Name,
                        State = b.Room.Hotel.City.State
                    }
                }
            }).ToList();

            return getBooking.Last();
        }

        public Room GetRoomById(int RoomId)
        {
            throw new NotImplementedException();
        }

    }

}