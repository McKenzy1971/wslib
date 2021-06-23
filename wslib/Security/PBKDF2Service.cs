using System;
using System.Security.Cryptography;

namespace wslib.Security
{
    /// <summary>
    /// Implements Rfc2898DerivedBytes for hashing a password.
    /// </summary>
    public class Pbkdf2Service
    {
        /// <summary>
        /// Initializes a new Instance of Pbkdf2Service with default values. <see cref="SaltSize"/> = 8 bytes. <see cref="HashSize"/> = 128 bit. <see cref="Iterations"/> = 10000.
        /// </summary>
        public Pbkdf2Service() : this(8, 128, 10000) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Pbkdf2Service"/> class with default values. <see cref="SaltSize"/> = 8 bytes. <see cref="Iterations"/> = 10000.
        /// </summary>
        /// <param name="hashsize">The size of the Hash it bit.</param>
        public Pbkdf2Service(short hashsize) : this(8, hashsize, 10000) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Pbkdf2Service"/> class.
        /// </summary>
        /// <param name="saltSize">The size of Salt in bytes</param>
        /// <param name="hashSize">The size of Hash in bits</param>
        /// <param name="iterations">The Iterations count.</param>
        public Pbkdf2Service(short saltSize, short hashSize, int iterations)
        {
            this.SaltSize = saltSize;
            this.HashSize = hashSize;
            this.Iterations = iterations;
        }

        private int _iterations;
        /// <summary>
        /// Gets oder Sets the Salt size in bytes.
        /// </summary>
        public short SaltSize { get; set; }
        /// <summary>
        /// Gets or sets the Hash size in bits.
        /// </summary>
        public short HashSize { get; set; }
        /// <summary>
        /// Gets or sets the Iterations. Minimum is 10000.
        /// </summary>
        public int Iterations
        {
            get => this._iterations;
            set => this._iterations = value >= 10000 ? value : 10000;
        }
        /// <summary>
        /// Computes the hash for this password with random salt.
        /// </summary>
        /// <param name="password">The password to hash</param>
        /// <returns>The hash, salt and iterations as <see cref="PasswordHash"/>.</returns>
        public PasswordHash ComputeHash(string password)
        {
            byte[] salt = this.GetRandomSalt();
            Rfc2898DeriveBytes deriveBytes = new(password, salt, this.Iterations);
            return new PasswordHash(salt, deriveBytes.GetBytes(this.HashSize / 8), this.Iterations);
        }

        private byte[] GetRandomSalt()
        {
            try
            {
                RNGCryptoServiceProvider cryptoServiceProvider = new();
                byte[] result = new byte[this.SaltSize];
                cryptoServiceProvider.GetBytes(result);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Getting an random salt throws an Exception.", e);
            }
        }
    }
}