namespace CarDealer.Web.Helpers.Filters
{
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Globalization;
    using System.Linq;

    public class LogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var user = context.HttpContext.User.Identity.Name;

            var tokens = context
                    .HttpContext
                    .Request
                    .Path
                    .ToString()
                    .Split(new[] { '/', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var operation = tokens.Last();

            var modifiedTable = tokens.First();

            var db = context
                .HttpContext
                .RequestServices
                .GetService<CarDealerDbContext>();

            db
                .Logs
                .Add(new Log
                {
                    User = user,
                    Operation = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(operation),
                    ModifiedTable = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(modifiedTable),
                    Time = DateTime.UtcNow
                });

            db.SaveChanges();

            base.OnActionExecuted(context);
        }
    }
}
