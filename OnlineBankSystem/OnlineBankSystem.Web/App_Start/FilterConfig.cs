namespace OnlineBankSystem.Web
{
    using Infrastructure.Filters;
    using System.Web.Mvc;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            filters.Add(new AddUserSessionAttribute());

            filters.Add(new ValidateAntiForgeryTokenOnPostAttribute());
        }
    }
}
