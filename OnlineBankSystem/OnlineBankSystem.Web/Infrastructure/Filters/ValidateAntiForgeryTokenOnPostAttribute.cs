namespace OnlineBankSystem.Web.Infrastructure.Filters
{
    using System.Web.Helpers;
    using System.Web.Mvc;

    public class ValidateAntiForgeryTokenOnPostAttribute : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.HttpMethod != "GET")
            {
                AntiForgery.Validate();
            }
        }
    }
}