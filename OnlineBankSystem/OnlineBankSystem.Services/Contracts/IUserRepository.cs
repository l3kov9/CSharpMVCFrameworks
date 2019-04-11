namespace OnlineBankSystem.Services.Contracts
{
    using Data;
    using Models.Users;

    public interface IUserRepository : IDbConnector
    {
        UserServiceModel GetUserByUsername(string username);
    }
}
