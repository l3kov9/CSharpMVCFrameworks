namespace OnlineBankSystem.Web.Infrastructure.Extensions
{
    using System.Web.Mvc;

    public static class TempDataExtensions
    {
        public static void AddSuccessMessage(this TempDataDictionary tempData, string message)
        {
            tempData["SuccessMessage"] = message;
        }

        public static void AddErrorMessage(this TempDataDictionary tempData, string message)
        {
            tempData["ErrorMessage"] = message;
        }
    }
}