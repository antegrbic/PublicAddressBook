using PublicAddressBook.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicAddressBook.Domain.Repository
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> ListAsync();

        Task<List<Contact>> GetContactAsync(string name, Address address);
        Task<Contact> GetContactByIdAsync(int id);

        Task AddAsync(Contact contact);

        void Remove(Contact contact);

        void Update(Contact contact);
    }
}
