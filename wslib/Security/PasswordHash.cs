using System;
using System.Text;

namespace wslib.Security
{
    /// <summary>
    /// Represents a hashed password with salt and iterations.
    /// </summary>
    public struct PasswordHash
    {
        private byte[] _salt;
        /// <summary>
        /// The Hashed password
        /// </summary>
        public byte[] Hash { get; set; }
        /// <summary>
        /// The iterations used for the hash
        /// </summary>
        public int Iterations { get; set; }
        /// <summary>
        /// The salt used for the hash.
        /// </summary>
        public byte[] Salt
        {
            get => this._salt;
            set
            {
                if ((value.Length % 8 == 0) && (value != null) && value.Length < 1000)
                    this._salt = value;
                else
                {
                    throw new Exception();
                }
            }
        }
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
        public override int GetHashCode() => HashCode.Combine(this.Hash, this.Iterations, this.Salt);


        /// <summary>
        /// Gets the full Password hash as string
        /// </summary>
        /// <returns>Salt, iterations and Hash as string</returns>
        public override string ToString()
        {
            string saltSize = Salt.Length switch
            {
                int n when (n < 10) => String.Format("  {0}", n.ToString()),
                int n when (n < 100) && (n >= 10) => String.Format(" {0}", n.ToString()),
                int n when n > 100 => String.Format("{0}", n.ToString()),
                _ => throw new Exception(),
            };
            return String.Format("{0}{1}{2}{3}", saltSize, Convert.ToBase64String(this.Salt), this.Hash, this.Iterations.ToString());
        }

        /// <summary>
        /// Converts a string build with PasswordHash.ToString() method to a PasswordHash.
        /// </summary>
        /// <param name="pwHashString">The string builded with PasswordHash.ToString() method</param>
        /// <returns>An PasswordHash with salt, iterations and Hash</returns>
        public static PasswordHash FromString(string pwHashString)
        {
            try
            {
                PasswordHash passwordHash = new();
                short saltsize = short.Parse(pwHashString.Substring(0, 3).Trim());
                passwordHash.Salt = Convert.FromBase64String(pwHashString.Substring(3, saltsize));
                StringBuilder sb = new();
                for (int i = pwHashString.Length - 1; i > saltsize + 3; i--)
                {
                    if (Byte.TryParse(pwHashString[i].ToString(), out byte value))
                        sb.Append(value);
                    else
                        break;
                }
                passwordHash.Iterations = int.Parse(sb.ToString());
                int hashSize = pwHashString.Length - saltsize - 3 - sb.Length;
                passwordHash.Hash = Convert.FromBase64String(pwHashString.Substring(saltsize + 3, hashSize));
                return passwordHash;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}