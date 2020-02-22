using PublicAddressBook.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PublicAddressBook.Domain.Models
{
    [Table("PhoneNumber")]
    public class PhoneNumber
    {
        [Key]
        public Guid PhoneId { get; set; }
        public string Number { get; set; }
        public int ContactId { get; set; }
        public Contact Contact { get; set; }

        public PhoneNumber() { }

        public PhoneNumber(string value)
        {
            PhoneId = new Guid();
            if (!Regex.IsMatch(value, @"^?\(?\d{3}?\)??-??\(?\d{3}?\)??-??\(?\d{4}?\)??-?$"))
            {
                throw new ArgumentException(Constants.PhoneConstants.Phone_Format);
            }
            Number = value;
        }
    }
}
