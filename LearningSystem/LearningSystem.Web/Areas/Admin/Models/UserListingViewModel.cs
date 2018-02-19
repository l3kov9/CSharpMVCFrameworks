namespace LearningSystem.Web.Areas.Admin.Models
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Admin.Models;
    using System.Collections.Generic;

    public class UserListingViewModel
    {
        public IEnumerable<AdminUserListingServiceModel> Users { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
