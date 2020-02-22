using PublicAddressBook.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicAddressBook.Domain.Services.Communication
{
    public class ContactResponse : BaseResponse<Contact>
    {
        public Contact Contact { get; private set; }

        private ContactResponse(bool success, string message, Contact contact) : base(success, message)
        {
            Contact = contact;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="category">Saved category.</param>
        /// <returns>Response.</returns>
        public ContactResponse(Contact contact) : this(true, string.Empty, contact)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public ContactResponse(string message) : this(false, message, null)
        { }
    }
}
