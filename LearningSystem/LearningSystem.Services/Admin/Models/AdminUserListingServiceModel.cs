namespace LearningSystem.Services.Admin.Models
{
    using Data.Models;
    using Common.Mapping;

    public class AdminUserListingServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }
    }
}
