using System.Linq;
namespace wslib.Security
{
    /// <summary>
    /// Represents an Hashed password.
    /// </summary>
    public struct PasswordHash
    {
        /// <summary>
        /// Initializes a new <see cref="PasswordHash"/>.
        /// </summary>
        /// <param name="salt">The used salt for the hash.</param>
        /// <param name="hash">The hash.</param>
        /// <param name="iterations">The iterations count.</param>
        public PasswordHash(byte[] salt, byte[] hash, int iterations)
        {
            this.Salt = salt;
            this.Hash = hash;
            this.Iterations = iterations;
            this.HashSize = (short)(hash.Length * 8);
        }
        /// <summary>
        /// Gets the hash.
        /// </summary>
        public byte[] Hash { get; }
        /// <summary>
        /// Gets the salt.
        /// </summary>
        public byte[] Salt { get; }
        /// <summary>
        /// Gets the iterations.
        /// </summary>
        public int Iterations { get; }
        /// <summary>
        /// The size of the hash in bits.
        /// </summary>
        public short HashSize { get; }

        /// <summary>
        /// Evaluates if two <see cref="PasswordHash"/> objects have the same value.
        /// </summary>
        /// <param name="left">Left object to compare</param>
        /// <param name="right">Right object to compare</param>
        /// <returns>True if the objects have the same value, otherwise false.</returns>
        public static bool operator ==(PasswordHash left, PasswordHash right) => left.Hash.SequenceEqual(right.Hash) && left.Salt.SequenceEqual(right.Salt) && (left.Iterations == right.Iterations) && (left.HashSize == right.HashSize);

        /// <summary>
        /// Evaluates if two <see cref="PasswordHash"/> objects haven't the same value.
        /// </summary>
        /// <param name="left">Left object to compare</param>
        /// <param name="right">Right object to compare</param>
        /// <returns>True if the objects haven't the same value. Otherwise false.</returns>
        public static bool operator !=(PasswordHash left, PasswordHash right) => !left.Hash.SequenceEqual(right.Hash) && (!left.Salt.SequenceEqual(right.Salt)) && (left.Iterations != right.Iterations) && (left.HashSize != right.HashSize);
    }
}