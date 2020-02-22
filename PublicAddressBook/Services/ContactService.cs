using PublicAddressBook.Domain.Models;
using PublicAddressBook.Domain.Repository;
using PublicAddressBook.Domain.Services;
using PublicAddressBook.Domain.Services.Communication;
using PublicAddressBook.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicAddressBook.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ContactService(IContactRepository contactRepository, IUnitOfWork unitOfWork)
        {
            _contactRepository = contactRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<ContactResponse> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Contact>> ListAsync()
        {
            return await _contactRepository.ListAsync();
        }

        public async Task<ContactResponse> SaveAsync(Contact contact)
        {
            var existingContact = await _contactRepository.GetContactAsync(contact.Name, contact.Address);

            if(existingContact != null && existingContact.Count > 0)
                return new ContactResponse(Constants.ContactConstants.Contact_AlreadyExist); 

            try
            {
                await _contactRepository.AddAsync(contact);
                await _unitOfWork.CompleteAsync();

                return new ContactResponse(contact);
            }
            catch (Exception ex)
            {
                return new ContactResponse($"An error occurred when saving the contact: {ex.Message}");
            }
        }

        public async Task<ContactResponse> UpdateAsync(int id, Contact contact)
        {
            var existingContact = await _contactRepository.GetContactByIdAsync(id);

            if (existingContact == null)
            {
                return new ContactResponse(Constants.ContactConstants.Contact_NotFound);
            }

            try
            {
                existingContact.Update(contact);

                _contactRepository.Update(existingContact);
                await _unitOfWork.CompleteAsync();

                return new ContactResponse(existingContact);
            }
            catch (Exception ex)
            {
                return new ContactResponse($"An error occurred when updating the category: {ex.Message}");
            }
        }
    }
}
