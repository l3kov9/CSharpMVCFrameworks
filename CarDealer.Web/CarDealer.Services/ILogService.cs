namespace CarDealer.Services
{
    using Models.Logs;
    using System.Collections.Generic;

    public interface ILogService
    {
        IEnumerable<LogServiceModel> All(string search, int page = 1, int pageSize = 20);

        void ClearAll();

        int Total(string search);
    }
}
