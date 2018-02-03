namespace CarDealer.Web.Helpers.Extensions
{
    public static class StringExtensions
    {
        public static string ToPrice (this decimal price)
        {
            return $"${price:F2}";
        }

        public static string ToPercentage(this double percentageText)
        {
            return $"{percentageText * 100:f2} %";
        }
    }
}
