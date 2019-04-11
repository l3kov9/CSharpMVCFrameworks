namespace OnlineBankSystem.Web.Infrastructure.Filters
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new { controller = "Users", action = "Login" }));
        }
    }
}