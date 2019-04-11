namespace OnlineBankSystem.Web.Infrastructure.Filters
{
    using Common.Authentication;
    using Services.Models.Users;
    using System.Web.Mvc;
    using System.Web.Mvc.Filters;

    public class AddUserSessionAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (filterContext.HttpContext.Session[AuthenticationConstants.SessionUserKey] is User user)
            {
                filterContext.Principal = user;
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
        }
    }
}