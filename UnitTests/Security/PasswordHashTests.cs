using NUnit.Framework;
using wslib.Security;

namespace UnitTests.Security
{
    class PasswordHashTests
    {
        private PasswordHash _passwordHash1;
        private PasswordHash _passwordHash2;

        [OneTimeSetUp]
        public void SetUp()
        {
            byte[] bytes = new byte[16] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            byte[] bytes2 = new byte[16] { 13, 2, 3, 4, 5, 6, 74, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            int iterations = 0;
            this._passwordHash1 = new() { Hash = bytes, Iterations = iterations, Salt = bytes2 };
            this._passwordHash2 = new() { Hash = bytes, Iterations = iterations, Salt = bytes2 };
        }

        [Test]
        public void EqualOperator_Equal_ReturnsTrue()
        {
            Assert.IsTrue(this._passwordHash1 == this._passwordHash2);
            Assert.AreEqual(this._passwordHash1, this._passwordHash2);
        }

        [Test]
        public void Equal_Equal_ReturnsTrue()
        {
            Assert.IsTrue(this._passwordHash1.Equals(this._passwordHash2));
            PasswordHash.Equals(this._passwordHash1, this._passwordHash2);
        }
    }
}