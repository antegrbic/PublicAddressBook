using PublicAddressBook.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PublicAddressBook.DTOs
{
    public class ContactDTO
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Name is mandatory")]
        [MaxLength(30)]
        public string Name { get; set; }

        public string DateOfBirth { get; set; }

        [Required(ErrorMessage = "Address details are mandatory")]
        public string StreetName { get; set; }

        [Required(ErrorMessage = "Address details are mandatory")]
        public string StreetNumber { get; set; }

        [Required(ErrorMessage = "Address details are mandatory")]
        public string City { get; set; }

        [Required(ErrorMessage = "Address details are mandatory")]
        public string Country { get; set; }

        public IEnumerable<string> PhoneNumbers { get; set; } = new List<string>();

        public static ContactDTO Create(Contact contact)
        {
            return new ContactDTO
            {
                Id = contact.ContactId.ToString(),
                Name = contact.Name,
                DateOfBirth = contact.DateOfBirth.ToString("yyyy-mm-dd"),
                StreetName = contact.Address.StreetName,
                StreetNumber = contact.Address.StreetNumber,
                City = contact.Address.City,
                Country = contact.Address.Country,
                PhoneNumbers = contact.PhoneNumbers.Select(t => t.Number)
            };
        }

        public Contact Create()
        {
            return new Contact(Convert.ToInt32(Id),
                Name, 
                new Address(StreetName, StreetNumber, City, Country), 
                DateTime.Parse(DateOfBirth), 
                PhoneNumbers.Select(t => new PhoneNumber(t)).ToList());
        }
    }
}
