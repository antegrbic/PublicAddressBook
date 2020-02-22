using Microsoft.EntityFrameworkCore;
using PublicAddressBook.Domain.Models;
using PublicAddressBook.Domain.Repository;
using PublicAddressBook.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicAddressBook.Persistence.Repositories
{
    public class ContactRepository : BaseRepository, IContactRepository
    {
        public ContactRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Contact>> ListAsync()
        {
            return await _context.Contacts
                                 .Include(c => c.PhoneNumbers)
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public async Task AddAsync(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);

            foreach (var phoneNumber in contact.PhoneNumbers)
                await _context.PhoneNumbers.AddAsync(phoneNumber);

            await _context.Addresses.AddAsync(contact.Address);
        }

        public void Remove(Contact contact)
        {
            foreach (var phoneNumber in contact.PhoneNumbers)
                _context.PhoneNumbers.Remove(phoneNumber);

            _context.Addresses.Remove(contact.Address);

            _context.Contacts.Remove(contact);
        }

        public async Task<List<Contact>> GetContactAsync(string name, Address address)
        {
            var contacts = await _context
                .Contacts
                .Include(c => c.PhoneNumbers)
                .AsNoTracking()
                .Where(c => c.Name == name)
                //  Doesnt work for some reason with .Where(c => c.Name == name && c.Address == address)
                // quick dirty fix instead
                .ToListAsync();

            if (contacts.Count > 0)
            {
                foreach (var contact in contacts)
                    if (contact.Address == address)
                        return contacts;
            }

            contacts.Clear();
            return contacts;
        }

        public void Update(Contact contact)
        {
            _context.Contacts.Update(contact);
        }

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            var contacts = await _context.Contacts
                .Include(c => c.PhoneNumbers)
                .Include(c => c.Address)
                .AsNoTracking()
                .Where(c => c.ContactId == id)
                .ToListAsync();

            return contacts.SingleOrDefault();
        }
    }
}
