using System;

namespace wslib.Security
{
    /// <summary>
    /// Represents a hashed password with salt and iterations.
    /// </summary>
    public struct PasswordHash
    {
        /// <summary>
        /// The Hashed password
        /// </summary>
        public byte[] Hash { get; set; }
        /// <summary>
        /// The iterations used for the hash
        /// </summary>
        public int Iterations { get; set; }
        /// <summary>
        /// The salt used for the hash
        /// </summary>
        public byte[] Salt { get; set; }
        /// <summary>
        /// Compares two PasswordHash objects for equality.
        /// </summary>
        /// <param name="left">wslib.Security.PasswordHash one</param>
        /// <param name="right">wslib.Security.PasswordHash two</param>
        /// <returns>true if left has the same value as right; otherwise, false.</returns>
        public static bool operator ==(PasswordHash left, PasswordHash right)
        {
            return (left.Hash == right.Hash) && (left.Iterations == right.Iterations) && (left.Salt == right.Salt);
        }
        /// <summary>
        /// Compares two PasswordHash objects for equality.
        /// </summary>
        /// <param name="left">wslib.Security.PasswordHash one</param>
        /// <param name="right">wslib.Security.PasswordHash two</param>
        /// <returns>true if left hasn't the same value as right; otherwise, false.</returns>
        public static bool operator !=(PasswordHash left, PasswordHash right)
        {
            return (left.Hash != right.Hash) || (left.Iterations != right.Iterations) || (left.Salt != right.Salt);
        }
        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified wslib.Security.PasswordHash object.
        /// </summary>
        /// <param name="obj">A wslib.Security.PasswordHash value to compare to this instance.</param>
        /// <returns>true if obj has the same value as this instance; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A hash code for the current wslib.Security.PasswordHash.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Hash, Iterations, Salt);
        }
    }
}