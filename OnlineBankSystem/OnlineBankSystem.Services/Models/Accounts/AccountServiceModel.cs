namespace OnlineBankSystem.Services.Models.Accounts
{
    using Bills;
    using System.Collections.Generic;

    public class AccountServiceModel
    {
        public AccountServiceModel(string username, string firstName, string lastName)
        {
            this.Username = username;
            this.FirstName = firstName;
            this.LastName = lastName;

            this.Bills = new List<BillServiceModel>();
        }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IList<BillServiceModel> Bills { get; set; }
    }
}
