namespace CameraBazaar.Web.Helpers
{
    public static class StringExtensions
    {
        public static string ToPrice(this decimal price)
        {
            return $"${price:F2}";
        }

        public static string ToPercentage(this double percent)
        {
            return $"{percent:F2} %";
        }
    }
}
