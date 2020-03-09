using Microsoft.AspNetCore.SignalR;
using PublicAddressBook.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublicAddressBook.Hubs
{
    public class ContactsHub
    {
        public class ContactHub : Hub
        {
            public Task Reload() => Clients.Others.SendAsync("Reload");
             
        }
    }
}
