namespace OnlineBankSystem.Services
{
    using Common.Authentication;
    using Contracts;
    using Models.Users;
    using SqlService;

    public class UserService
    {
        private IUserRepository userRepository;

        public UserService()
            : this(new UserRepository())
        {
        }

        public UserService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserServiceModel GetUser(string username, string password)
        {
            var user = this.userRepository.GetUserByUsername(username);

            if (user == null)
            {
                return null;
            }

            var actualPasswordHash = PasswordUtilities.GeneratePasswordHash(password, user.PasswordSalt).ToUpper();

            if (actualPasswordHash != user.PasswordHash)
            {
                return null;
            }

            return user;
        }
    }
}
