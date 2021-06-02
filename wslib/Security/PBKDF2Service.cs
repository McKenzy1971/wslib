using System.Security;
using System.Security.Cryptography;

namespace wslib.Security
{
    /// <summary>
    /// Implements Rfc2898DerivedBytes for hashing a password.
    /// </summary>
    public static class PBKDF2Service
    {
        #region Methods
        /// <summary>
        /// Generates a Hash(256bit) from an given password, an random generated Salt(256bit) and random generated iterations (Between 10K and 100K).
        /// </summary>
        /// <param name="password">The password to Hash</param>
        /// <returns>The Hashed password with salt and iterations as struct PasswordHash</returns>
        public static PasswordHash HashPassword(SecureString password)
        {
            PasswordHash result = new();
            RNGCryptoServiceProvider rng = new();
            result.Salt = new byte[32];
            rng.GetBytes(result.Salt);
            result.Iterations = GetRandomInt();
            Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password.ToString(), result.Salt, result.Iterations);
            result.Hash = deriveBytes.GetBytes(32);
            return result;
        }

        public static PasswordHash HashPassword(SecureString password, int iterations)
        {
            PasswordHash result = new();
            RNGCryptoServiceProvider rng = new();
            result.Salt = new byte[32];
            rng.GetBytes(result.Salt);
            result.Iterations = iterations;
            Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password.ToString(), result.Salt, result.Iterations);
            result.Hash = deriveBytes.GetBytes(32);
            return result;
        }

        public static PasswordHash HashPassword(SecureString password, int iterations, byte[] salt)
        {
            PasswordHash result = new();
            result.Salt = salt;
            result.Iterations = iterations;
            Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password.ToString(), result.Salt, result.Iterations);
            result.Hash = deriveBytes.GetBytes(32);
            return result;
        }

        public static bool ValidatePassword(PasswordHash original, SecureString password)
        {
            PasswordHash hash = HashPassword(password, original.Iterations, original.Salt);
            return hash == original;
        }

        private static int GetRandomInt() => RandomNumberGenerator.GetInt32(10000, 100000);
        #endregion

    }
}