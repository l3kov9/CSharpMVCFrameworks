namespace LearningSystem.Services.Admin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AdminUserService : IAdminUserService
    {
        private readonly LearningSystemDbContext db;

        public AdminUserService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminUserListingServiceModel>> All()
            => await this.db
                .Users
                .ProjectTo<AdminUserListingServiceModel>()
                .ToListAsync();
    }
}
