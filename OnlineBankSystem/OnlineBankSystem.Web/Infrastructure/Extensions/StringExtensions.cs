namespace OnlineBankSystem.Web.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string ToPrice(this decimal price)
        {
            return string.Format("$ {0:F2}", price);
        }
    }
}