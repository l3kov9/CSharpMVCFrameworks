namespace LearningSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly LearningSystemDbContext db;

        public UserService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<UserDetailsServiceModel> ProfileAsync(string id)
            =>  await this.db
                .Users
                .Where(u => u.Id == id)
                .ProjectTo<UserDetailsServiceModel>(new { studentId = id })
                .FirstOrDefaultAsync();
    }
}
