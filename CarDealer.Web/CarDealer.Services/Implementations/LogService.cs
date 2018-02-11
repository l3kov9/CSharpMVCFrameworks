namespace CarDealer.Services.Implementations
{
    using Data;
    using Models.Logs;
    using System.Collections.Generic;
    using System.Linq;

    public class LogService : ILogService
    {
        private readonly CarDealerDbContext db;

        public LogService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<LogServiceModel> All(string search, int page = 1, int pageSize = 20)
        {
            if (search != null)
            {
                return this.db
                .Logs
                .Where(l => l.User.ToLower().Contains(search.ToLower()))
                .OrderByDescending(l => l.Time)
                .Select(l => new LogServiceModel
                {
                    User = l.User,
                    Operation = l.Operation,
                    ModifiedTable = l.ModifiedTable,
                    Time = l.Time
                })
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            }

            return this.db
                .Logs
                .OrderByDescending(l => l.Time)
                .Select(l => new LogServiceModel
                {
                    User = l.User,
                    Operation = l.Operation,
                    ModifiedTable = l.ModifiedTable,
                    Time = l.Time
                })
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public void ClearAll()
        {
            var logs = this.db.Logs;

            this.db
                .Logs
                .RemoveRange(logs);

            this.db.SaveChanges();
        }

        public int Total(string search)
        {
            if(search != null)
            {
                return this.db
                    .Logs
                    .Where(l=>l.User.Contains(search))
                    .Count();
            }

            return this.db.Logs.Count();
        }
    }
}
