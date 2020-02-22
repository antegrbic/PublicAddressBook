using PublicAddressBook.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PublicAddressBook.Domain.Models
{
    public class Contact
    {   
        [Key]
        public int ContactId { get; set; }

        public string Name { get; set; }

        public Address Address { get; set; }

        public DateTime DateOfBirth { get; set; }
        private List<PhoneNumber> _phoneNumbers { get; set; }

        public IEnumerable<PhoneNumber> PhoneNumbers => _phoneNumbers.AsEnumerable();

        public Contact()
        {
            _phoneNumbers = new List<PhoneNumber>();
        }

        public Contact(int ContactId, string Name, Address Address, DateTime DateOfBirth, List<PhoneNumber> phoneNumbers)
        {
            Validate(Name, Address, phoneNumbers);

            this.ContactId = ContactId;
            this.Name = Name;
            this.Address = new Address();
            this.Address.Copy(Address);
            this.DateOfBirth = DateOfBirth;
            _phoneNumbers = phoneNumbers;
        }

        public void Update(Contact contact)
        {
            Validate(contact.Name, contact.Address, contact._phoneNumbers);

            this.Name = contact.Name;
            this.DateOfBirth = contact.DateOfBirth;
            this.Address.Copy(contact.Address);

            ClearPhoneNumbers();
            foreach (var phoneNumber in contact.PhoneNumbers)
            {
                AssignPhoneNumber(phoneNumber.Number);
            }
        }

        public void AssignPhoneNumber(string phoneNumber)
        {
            _phoneNumbers.Add(new PhoneNumber(phoneNumber));
        }

        public void ClearPhoneNumbers()
        {
            _phoneNumbers.Clear();
        }

        public void Validate(string Name, Address Address, List<PhoneNumber> phoneNumbers)
        {
            if (String.IsNullOrEmpty(Name))
                throw new Exception(Constants.ContactConstants.Contact_MissingNameError);

            //Address field-by-field validation done on Address object
            if (Address is null)
                throw new Exception(Constants.ContactConstants.Contact_MissingAddress);

            if (phoneNumbers == null || phoneNumbers.Count == 0)
                throw new Exception(Constants.ContactConstants.Contact_MissingPhoneNumber);
        }
    }
}
