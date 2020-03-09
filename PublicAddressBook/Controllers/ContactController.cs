using Microsoft.AspNetCore.Mvc;
using PublicAddressBook.Domain.Models;
using PublicAddressBook.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PublicAddressBook.Extensions;
using PublicAddressBook.DTOs;
using static PublicAddressBook.Hubs.ContactsHub;
using Microsoft.AspNetCore.SignalR;

namespace PublicAddressBook.Controllers
{
    [Route("/api/[controller]")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IHubContext<ContactHub> _hubContext;

        public ContactController(IContactService contactService, IHubContext<ContactHub> hubContext)
        {
            _contactService = contactService;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IEnumerable<ContactDTO>> GetAllAsync()
        {
            var contacts = await _contactService.ListAsync();

            return contacts.Select(x => ContactDTO.Create(x));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ContactDTO resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var contact = resource.Create();
            var result = await _contactService.SaveAsync(contact);

            if (!result.Success)
                return BadRequest(result.Message);

            await _hubContext.Clients.All.SendAsync("Reload");

            return Ok(ContactDTO.Create(result.Contact));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _contactService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            await _hubContext.Clients.All.SendAsync("Reload");

            return Ok(ContactDTO.Create(result.Contact));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] ContactDTO resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var contact = resource.Create();
            var result = await _contactService.UpdateAsync(id, contact);

            if (!result.Success)
                return BadRequest(result.Message);

            await _hubContext.Clients.All.SendAsync("Reload");

            return Ok(ContactDTO.Create(result.Contact));
        }

        public async Task<IActionResult> Index()
        {
            var contacts = await _contactService.ListAsync();
            return View(contacts );
        }
    }
}
