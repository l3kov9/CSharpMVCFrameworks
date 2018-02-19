namespace LearningSystem.Web.Helpers.Extensions
{
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    using static WebConstants;

    public static class TempDataExtensions
    {
        public static void AddSuccessMessage(this ITempDataDictionary tempData, string message)
        {
            tempData[TempDataSuccessMessageKey] = message;
        }

        public static void AddErrorMessage(this ITempDataDictionary tempData, string message)
        {
            tempData[TempDataErrorMessageKey] = message;
        }
    }
}
