using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PublicAddressBook.Domain.Models
{
    [Table("Address")]
    public class Address
    {
        public string StreetName { get; set; }

        public string StreetNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        
        public int ContactId { get; set; }
        public Contact Contact { get; set; } 

        public Address() { }

        public Address(string streetName, string streetNumber, string city, string country) 
        {
            if(String.IsNullOrEmpty(streetName) || String.IsNullOrEmpty(streetNumber) || String.IsNullOrEmpty(city) || String.IsNullOrEmpty(country))
            {
                throw new Exception("Provide all details for address!");
            }

            StreetName = streetName;
            StreetNumber = streetNumber;
            City = city;
            Country = country;
        }

        public void Copy(Address other)
        {
            StreetName = other.StreetName;
            StreetNumber = other.StreetNumber;
            City = other.City;
            Country = other.Country;
        }

        public static bool operator ==(Address first, Address second) => first.Equals(second);
        public static bool operator !=(Address first, Address second) => !first.Equals(second);

        public bool Equals(Address other) =>  StreetName == other.StreetName && StreetNumber == other.StreetNumber && City == other.City && Country == other.Country;

        public override bool Equals(object other)
        {
            Address mod = other as Address;
            if (mod != null)
                return Equals(mod);
            return false;
        }

        public override string ToString()
        {
            return $"{StreetName} {StreetNumber}, {City}, {Country}";
        }

        public override int GetHashCode()
        {
            throw new Exception("Sorry I don't know what GetHashCode should do for this class");
        }
    }
}
