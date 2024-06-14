using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly ITrybeHotelContext _context;
        public UserRepository(ITrybeHotelContext context)
        {
            _context = context;
        }
        public UserDto GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public UserDto Login(LoginDto login)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == login.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
            {
                throw new Exception("Invalid email or password");
            }
            return new UserDto
           {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                UserType = user.UserType
            };
        }

        public UserDto Add(UserDtoInsert user)
        {
            var enableUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);

            if (enableUser != null)
            {
                throw new Exception("User email already exists");
            }

            _context.Users.Add(new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                UserType = "client"
            });
            _context.SaveChanges();

            var addUser = _context.Users.FirstOrDefault(u => u.Email == user.Email) ?? throw new Exception("Can't add new user");
            return new UserDto
            {
                UserId = addUser.UserId,
                Name = addUser.Name,
                Email = addUser.Email,
                Password = addUser.Password,
                UserType = addUser.UserType
            }; 
        }

        public UserDto GetUserByEmail(string userEmail)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDto> GetUsers()
        {           
            var findAllUsers = _context.Users;
            return findAllUsers.Select(u => new UserDto
            {
                UserId = u.UserId,
                Name = u.Name,                
                Email = u.Email,        
                UserType = u.UserType
            }).ToList();
            
        }        


    }
}