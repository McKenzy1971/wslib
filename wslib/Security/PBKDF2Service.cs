﻿using System;
using System.Security.Cryptography;

namespace wslib.Security
{
    /// <summary>
    /// Implements Rfc2898DerivedBytes for hashing a password.
    /// </summary>
    public class Pbkdf2Service
    {
        #region Constructors
        /// <summary>
        /// Initializes a new Instance of <see cref="Pbkdf2Service"/> with default values. <see cref="SaltSize"/> = 8 bytes. <see cref="HashSize"/> = 128 bit. <see cref="Iterations"/> = 10000.
        /// </summary>
        public Pbkdf2Service() : this(128, 8, 10000) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Pbkdf2Service"/> class with default values. <see cref="SaltSize"/> = 8 bytes. <see cref="Iterations"/> = 10000.
        /// </summary>
        /// <param name="hashSize">The size of the Hash it bit.</param>
        public Pbkdf2Service(short hashSize) : this(hashSize, 8, 10000) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Pbkdf2Service"/> class.
        /// </summary>
        /// <param name="hashSize">The size of Hash in bits</param>
        /// <param name="saltSize">The size of Salt in bytes</param>
        /// <param name="iterations">The Iterations count.</param>
        public Pbkdf2Service(short hashSize, short saltSize, int iterations)
        {
            this.SaltSize = saltSize;
            this.HashSize = hashSize;
            this.Iterations = iterations;
        }
        #endregion

        #region Fields
        private int _iterations;
        #endregion

        #region Properties
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
        #endregion

        #region Methods
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

        /// <summary>
        /// Computes the hash for this password with given salt.
        /// </summary>
        /// <param name="password">The password to hash</param>
        /// <param name="salt">The hash for computing the hash</param>
        /// <returns>The hash, salt and iterations as <see cref="PasswordHash"/>.</returns>
        public PasswordHash ComputeHash(string password, byte[] salt)
        {
            Rfc2898DeriveBytes deriveBytes = new(password, salt, this.Iterations);
            return new PasswordHash(salt, deriveBytes.GetBytes(this.HashSize / 8), this.Iterations);
        }
        /// <summary>
        /// Verifys a password using previously hashed <see cref="PasswordHash"/> generated by <see cref="ComputeHash(string)"/>.
        /// </summary>
        /// <param name="passwordHash">The original hash.</param>
        /// <param name="password">The password to verify.</param>
        /// <returns>True if the output of hashing <paramref name="password"/> is the same as <paramref name="passwordHash"/>.</returns>
        public static bool VerifyPassword(PasswordHash passwordHash, string password)
        {
            Pbkdf2Service pbkdf2Service = new(passwordHash.HashSize, (short)passwordHash.Salt.Length, passwordHash.Iterations);

            PasswordHash result = pbkdf2Service.ComputeHash(password, passwordHash.Salt);
            return result == passwordHash;
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
        #endregion
    }
}