namespace OnlineBankSystem.Common.Authentication
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public static class PasswordUtilities
    {
        private const string SaltCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private static RandomNumberGenerator random;
        private static SHA256 hashingAlgorithm;

        static PasswordUtilities()
        {
            random = new RNGCryptoServiceProvider();
            hashingAlgorithm = new SHA256Managed();
        }

        public static string GeneratePasswordSalt(int length = AuthenticationConstants.DefaultSaltLength)
        {
            return new string(
                Enumerable.Repeat(SaltCharacters, length)
                  .Select(s => s[GetRandomIntegerBetween(0, s.Length)])
                  .ToArray());
        }

        public static string GeneratePasswordHash(string password, string salt)
        {
            var passwordHashBytes = hashingAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
            var passwordHash = string.Join(string.Empty, passwordHashBytes.Select(b => b.ToString("x2")));
            return passwordHash;
        }

        private static int GetRandomIntegerBetween(int min, int max)
        {
            var scale = uint.MaxValue;

            while (scale == uint.MaxValue)
            {
                var fourBytes = new byte[4];
                random.GetBytes(fourBytes);

                scale = BitConverter.ToUInt32(fourBytes, 0);
            }

            return (int)((min + (max - min)) * (scale / (double)uint.MaxValue));
        }
    }
}
