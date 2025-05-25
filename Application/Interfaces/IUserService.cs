using Domain;

namespace Application.Interfaces
{
    public interface IUserService
    {
        (User user, string error) GetUser(string name, string password);
    }
}
