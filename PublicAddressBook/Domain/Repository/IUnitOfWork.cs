using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicAddressBook.Domain.Repository
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
