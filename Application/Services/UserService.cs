using Application.Interfaces;
using Domain;
using Infrastructure;
namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;

        public UserService(AppDbContext db)
        {
            _db = db;
        }

        public (User user, string error) GetUser(string id, string password)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return (null, $"User {id} NOT FOUND");

            if (user.Password != password)
                return (null, "Wrong Password");

            return (user, null);
        }
    }
}
