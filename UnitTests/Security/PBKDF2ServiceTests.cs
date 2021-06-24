using NUnit.Framework;
using wslib.Security;

namespace UnitTests.Security
{
    [TestFixture]
    class PBKDF2ServiceTests
    {
        private const int _minSaltSize = 10000;
        const short _saltsize = 16;
        const short _hashsize = 32;
        const int _iterations = 15000;
        const string _password = "Password";

        [Test]
        [Category("ConstructorTests")]
        public void Initialization_DefaultConstructor_NoExceptions()
        {
            Pbkdf2Service pbkdf2Service = new();

            Assert.NotZero(pbkdf2Service.SaltSize);
            Assert.NotZero(pbkdf2Service.HashSize);
            Assert.NotZero(pbkdf2Service.Iterations);
            Assert.GreaterOrEqual(pbkdf2Service.Iterations, 10000);
            Assert.IsNotNull(pbkdf2Service);
        }

        [Test]
        [Category("ConstructorTests")]
        public void Initialization_CustomConstructor_NoExceptions()
        {
            Pbkdf2Service pbkdf2Service = new(_hashsize, _saltsize, _iterations);

            Assert.IsNotNull(pbkdf2Service);
            Assert.AreEqual(_saltsize, pbkdf2Service.SaltSize);
            Assert.AreEqual(_hashsize, pbkdf2Service.HashSize);
            Assert.AreEqual(_iterations, pbkdf2Service.Iterations);
            Assert.GreaterOrEqual(pbkdf2Service.Iterations, _minSaltSize);
        }

        [Test]
        [Category("ConstructorTests")]
        public void Initialization_CustomConstructor_ChangedIterationsToMinimum()
        {
            Pbkdf2Service pbkdf2Service = new(_hashsize, _saltsize, 20);

            Assert.IsNotNull(pbkdf2Service);
            Assert.AreEqual(_saltsize, pbkdf2Service.SaltSize);
            Assert.AreEqual(_hashsize, pbkdf2Service.HashSize);
            Assert.GreaterOrEqual(pbkdf2Service.Iterations, _minSaltSize);
        }

        [Test]
        [Category("HashTests")]
        public void HashPassword_DefaultConstructor_PasswordHash()
        {
            Pbkdf2Service pbkdf2Service = new();
            PasswordHash passwordHash = pbkdf2Service.ComputeHash(_password);
            PasswordHash passwordHash2 = pbkdf2Service.ComputeHash(_password);

            Assert.IsNotNull(passwordHash);
            Assert.IsNotEmpty(passwordHash.Salt);
            Assert.IsNotEmpty(passwordHash.Hash);
            Assert.NotZero(passwordHash.Iterations);
            Assert.AreNotEqual(passwordHash.Salt, passwordHash2.Salt);
            Assert.AreNotEqual(passwordHash.Hash, passwordHash2.Hash);
            Assert.AreEqual(passwordHash.Iterations, passwordHash2.Iterations);
        }

        [Test]
        [Category("HashTests")]
        public void HashPassword_CustomConstructorSaltGiven_EqualPasswordHashs()
        {
            Pbkdf2Service pbkdf2Service = new(32, 128, 12000);
            PasswordHash passwordHash = pbkdf2Service.ComputeHash(_password);
            PasswordHash passwordHash2 = pbkdf2Service.ComputeHash(_password, passwordHash.Salt);

            Assert.IsNotNull(passwordHash);
            Assert.IsNotEmpty(passwordHash.Salt);
            Assert.IsNotEmpty(passwordHash.Hash);
            Assert.NotZero(passwordHash.Iterations);
            Assert.AreEqual(passwordHash.Salt, passwordHash2.Salt);
            Assert.AreEqual(passwordHash.Hash, passwordHash2.Hash);
            Assert.AreEqual(passwordHash.Iterations, passwordHash2.Iterations);
        }

        [Test]
        [Category("VerifyHashTests")]
        public void Verify_DefaultValues_True()
        {
            Pbkdf2Service pbkdf2Service = new();
            PasswordHash passwordHash = pbkdf2Service.ComputeHash(_password);

            bool result = Pbkdf2Service.VerifyPassword(passwordHash, _password);

            Assert.IsTrue(result);
        }

        [Test]
        [Category("VerifyHashTests")]
        public void Verify_DifferentPassword_False()
        {
            Pbkdf2Service pbkdf2Service = new();
            PasswordHash passwordHash = pbkdf2Service.ComputeHash("pass");

            bool result = Pbkdf2Service.VerifyPassword(passwordHash, _password);

            Assert.IsFalse(result);
        }
    }
}