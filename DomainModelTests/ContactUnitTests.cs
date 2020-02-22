using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using PublicAddressBook.Domain.Models;
using PublicAddressBook.Extensions;

namespace DomainModelTests
{
    class ContactUnitTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Contact_MissingName()
        {
            var ex = Assert.Throws<Exception>(() => ContactTestHelper_CreateContact());
            Assert.That(ex.Message, Is.EqualTo(Constants.ContactConstants.Contact_MissingNameError));
        }

        public void Contact_MissingAddress()
        {
            var ex = Assert.Throws<Exception>(
                () => new Contact(
                1, "Ante", null, new DateTime(1990, 1, 1), new List<PhoneNumber>() { new PhoneNumber("123-345-6789") })
            );

            Assert.That(ex.Message, Is.EqualTo(Constants.ContactConstants.Contact_MissingAddress));
        }

        public void Contact_MissingPhoneNumbers()
        {
            var address = new Address("Vukovarska", "2A", "Zadar", "HRV");

            var ex = Assert.Throws<Exception>(
                () => new Contact(
                1, "Ante", address, new DateTime(1990, 1, 1), null)
            );

            Assert.That(ex.Message, Is.EqualTo(Constants.ContactConstants.Contact_MissingPhoneNumber));
        }

        public void Contact_UpdateDateOfBirth()
        {
            var firstAddress = new Address("Vukovarska", "2A", "Zadar", "HRV");

            var first = new Contact(
                1, "Ante", firstAddress, new DateTime(1990, 1, 1), new List<PhoneNumber>() { new PhoneNumber("123-345-6789") });

            var secondAddress = new Address("Zagrebačka", "2A", "Zagreb", "HRV");

            var second = new Contact(
                1, "Ante", secondAddress, new DateTime(2000, 1, 1), new List<PhoneNumber>() { new PhoneNumber("222-345-6789") });

            first.Update(second);

            Assert.AreEqual(first.DateOfBirth, new DateTime(2000, 1, 1));
            Assert.AreEqual(first.Address.StreetName, "Zagrebačka");
            Assert.AreEqual(first.Address.City, "Zagreb");
            
            foreach(var phoneNumber in first.PhoneNumbers)
            {
                Assert.AreEqual(phoneNumber.Number, "222-345-6789");
            }

        }

        public Contact ContactTestHelper_CreateContact()
        {
            var address = new Address("Vukovarska", "2A", "Zadar", "HRV");
            return new Contact(
                1, "", address, new DateTime(1990, 1, 1), new List<PhoneNumber>() { new PhoneNumber("123-345-6789") });
        }
    }
}
