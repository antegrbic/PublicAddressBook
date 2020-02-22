using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PublicAddressBook.Domain.Models;
using PublicAddressBook.Domain.Services;
using PublicAddressBook.Models;
using PublicAddressBook.Persistence.Contexts;

namespace PublicAddressBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContactService _contactService;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, IContactService contactService, AppDbContext appDbContext)
        {
            _logger = logger;
            _contactService = contactService;
            _context = appDbContext;
        }

        public async Task<IActionResult> Index(int? pageNumber)
        {
            var Contacts = from contact in _context.Contacts 
                select contact;

            int pageSize = 3;

            return View(await PaginatedList<Contact>.CreateAsync(Contacts.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
