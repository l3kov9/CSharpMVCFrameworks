namespace CarDealer.Web.Controllers
{
    using CarDealer.Web.Helpers.Filters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using System;

    public class LogsController : Controller
    {
        private const int PageSize = 20;

        public ILogService logs;

        public LogsController(ILogService logs)
        {
            this.logs = logs;
        }

        public IActionResult All(string search, int page = 1)
        {
            return View(new LogViewModel
            {
                Logs = this.logs.All(search, page, PageSize),
                Search = search,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)this.logs.Total(search) / PageSize)
            });
        }

        [Authorize]
        [Log]
        public IActionResult Clear()
        {
            this.logs.ClearAll();

            return RedirectToAction(nameof(All));
        }
    }
}
