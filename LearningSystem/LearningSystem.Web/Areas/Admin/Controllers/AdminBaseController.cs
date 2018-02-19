namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Helpers.WebConstants;

    [Area(AdminArea)]
    [Authorize(Roles = AdministratorRole)]
    public class AdminBaseController : Controller
    {
    }
}
