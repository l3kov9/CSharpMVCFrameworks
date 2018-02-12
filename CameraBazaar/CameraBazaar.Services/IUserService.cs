namespace CameraBazaar.Services
{
    using Models;

    public interface IUserService
    {
        UserServiceModel ByUsername(string username);
    }
}
