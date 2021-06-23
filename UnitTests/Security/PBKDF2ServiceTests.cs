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
            Pbkdf2Service pbkdf2Service = new(_saltsize, _hashsize, _iterations);

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
            Pbkdf2Service pbkdf2Service = new(_saltsize, _hashsize, 20);

            Assert.IsNotNull(pbkdf2Service);
            Assert.AreEqual(_saltsize, pbkdf2Service.SaltSize);
            Assert.AreEqual(_hashsize, pbkdf2Service.HashSize);
            Assert.GreaterOrEqual(pbkdf2Service.Iterations, _minSaltSize);
        }

        [Test]
        [Category("HashTest")]
        public void HashPassword_DefaultConstructor_HashedPasswordAndSaltAsByteArray()
        {
            Pbkdf2Service pbkdf2Service = new();
            PasswordHash passwordHash = pbkdf2Service.ComputeHash(_password);

            Assert.IsNotNull(passwordHash);
            Assert.IsNotEmpty(passwordHash.Salt);
            Assert.IsNotEmpty(passwordHash.Hash);
            Assert.NotZero(passwordHash.Iterations);
        }
    }
}