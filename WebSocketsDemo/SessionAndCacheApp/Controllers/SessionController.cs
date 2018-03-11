namespace SessionAndCacheApp.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;

    public class SessionController : Controller
    {
        public IActionResult Index()
        {
            const string sessionDateKey = "Session_Current_Date";

            var value = this.HttpContext.Session.GetString(sessionDateKey);

            if (value == null)
            {
                value = DateTime.UtcNow.ToLongTimeString();
                this.HttpContext.Session.SetString(sessionDateKey, value);
            }

            return View(model: value);
        }
    }
}
