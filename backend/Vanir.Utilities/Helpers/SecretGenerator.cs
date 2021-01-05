using System;
using System.Security.Cryptography;

namespace Vanir.Utilities.Helpers
{
    public static class SecretGenerator
    {
        public static string GenerateSecret()
        {
            var tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
            tripleDESCryptoServiceProvider.GenerateKey();

            return Convert.ToBase64String(tripleDESCryptoServiceProvider.Key);
        }
    }
}