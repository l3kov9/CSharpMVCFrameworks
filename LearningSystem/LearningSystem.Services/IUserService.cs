namespace LearningSystem.Services
{
    using Services.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<UserDetailsServiceModel> ProfileAsync(string id);

        Task<IEnumerable<UserListingServiceModel>> UserBySearchAsync(string search);
    }
}
