using NUnit.Framework;
using PublicAddressBook.Domain.Models;
using System;
using PublicAddressBook.Extensions;

namespace DomainModelTests
{
    public class AddressUnitTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Address_AllDetailsProvided()
        {
            Address target = new Address("Vukovarska", "1", "Zagreb", "HRV");

            Assert.AreEqual("Vukovarska", target.StreetName);
            Assert.AreEqual("1", target.StreetNumber);
            Assert.AreEqual("Zagreb", target.City);
            Assert.AreEqual("HRV", target.Country);
        }

        public void Address_CopyAddress()
        {
            Address first = new Address("Vukovarska", "1", "Zagreb", "HRV");
            Address second = new Address();

            second.Copy(first);

            Assert.AreEqual("Vukovarska", second.StreetName);
            Assert.AreEqual("1", second.StreetNumber);
            Assert.AreEqual("Zagreb", second.City);
            Assert.AreEqual("HRV", second.Country);
        }

        [Test]
        public void Address_NotAllDetailsWereProvided()
        {
            var ex = Assert.Throws<Exception>(() => new Address("", "13A", "Zadar", ""));
            Assert.That(ex.Message, Is.EqualTo(Constants.AddressConstants.Address_MissingDetails));
        }

        [Test]
        public void Address_CheckEquality()
        {
            Address first = new Address("Ilica", "10", "Zagreb", "HRV");
            Address second = new Address("Ilica", "10", "Zagreb", "HRV");

            Assert.IsTrue(first == second);
        }
    }
}