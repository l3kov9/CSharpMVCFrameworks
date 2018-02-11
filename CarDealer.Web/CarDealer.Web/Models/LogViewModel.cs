namespace CarDealer.Web.Models
{
    using Services.Models.Logs;
    using System.Collections.Generic;

    public class LogViewModel
    {
        public IEnumerable<LogServiceModel> Logs { get; set; }

        public string Search { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PreviousPage => this.CurrentPage - 1;

        public int NextPage => this.CurrentPage + 1;
    }
}
