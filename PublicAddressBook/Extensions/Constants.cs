using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicAddressBook.Extensions
{
    public static class Constants
    {
        public static class ContactConstants
        {
            public static readonly string Contact_MissingNameError = "Contact name must be provided!";
            public static readonly string Contact_MissingAddress = "Contact address must be provided!";
            public static readonly string Contact_MissingPhoneNumber = "At least one phone number must provided for a given contact!";

            public static readonly string Contact_AlreadyExist = "Contact already exists!";
            public static readonly string Contact_NotFound = "Contact not found!";
        }

        public static class AddressConstants
        {
            public static readonly string Address_MissingDetails = "Provide all details for address!";
        }

        public static class PhoneConstants
        {
            public static readonly string Phone_Format = "Telehpone number must be in (123)-345-6789 or 123-345-6789 format!";
        }
    }
}
