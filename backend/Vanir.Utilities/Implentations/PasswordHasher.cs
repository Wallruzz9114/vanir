using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Vanir.Utilities.Interfaces;

namespace Vanir.Utilities.Implentations
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(byte[] salt, string password) =>
            Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));
    }
}