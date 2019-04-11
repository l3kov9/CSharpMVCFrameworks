namespace OnlineBankSystem.Services.Models.Users
{
    using Common.Authentication;
    using System.Security.Principal;

    public class User : IPrincipal
    {
        public User(int id, string username)
        {
            this.Id = id;
            this.Username = username;
            this.Identity = new GenericIdentity(this.Username, AuthenticationConstants.CookieAuthType);
        }

        public int Id { get; private set; }

        public string Username { get; private set; }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            return false;
        }
    }
}
