namespace OnlineBankSystem.Services.Models.Users
{
    public class UserServiceModel : User
    {
        public UserServiceModel(int id, string username, string firstName, string lastName, string passwordHash, string passwordSalt)
            : base(id, username)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PasswordHash = passwordHash;
            this.PasswordSalt = passwordSalt;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }
    }
}
