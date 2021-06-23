using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wslib.Security
{
    public struct PasswordHash
    {
        public PasswordHash(byte[] salt, byte[] hash, int iterations)
        {
            this.Salt = salt;
            this.Hash = hash;
            this.Iterations = iterations;
        }

        public byte[] Hash { get; }
        public byte[] Salt { get; }
        public int Iterations { get; }
    }
}