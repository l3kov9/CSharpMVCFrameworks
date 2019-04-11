namespace OnlineBankSystem.Services.Extensions
{
    using Models.Users;
    using System.Security.Principal;

    public static class UserExtensions
    {
        public static int GetUserId(this IPrincipal principal)
        {
            if (principal is User user)
            {
                return user.Id;
            }

            return 0;
        }
    }
}
