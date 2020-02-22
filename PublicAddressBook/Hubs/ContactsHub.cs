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
            public async void SendMessage()
            {
                
            }
        }
    }
}
