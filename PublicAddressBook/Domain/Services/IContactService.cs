using PublicAddressBook.Domain.Models;
using PublicAddressBook.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicAddressBook.Domain.Services
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> ListAsync();

        Task<ContactResponse> SaveAsync(Contact contact);

        Task<ContactResponse> DeleteAsync(int id);

        Task<ContactResponse> UpdateAsync(int id, Contact category);
    }
}
