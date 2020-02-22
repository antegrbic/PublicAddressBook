using Microsoft.EntityFrameworkCore;
using PublicAddressBook.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicAddressBook.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Contact>().ToTable("Contacts");
            builder.Entity<Contact>().HasKey(c => c.ContactId);
            builder.Entity<Contact>().Property(c => c.ContactId).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Contact>().Property(c => c.Name).IsRequired().HasMaxLength(40);
            builder.Entity<Contact>().OwnsOne(c => c.Address);

            builder.Entity<Contact>().ToTable("PhoneNumber");
            builder.Entity<PhoneNumber>().HasKey(p => p.PhoneId);

            builder.Entity<PhoneNumber>()
            .HasOne(p => p.Contact)
            .WithMany(c => c.PhoneNumbers)
            .IsRequired();

            //GenerateSeedData.SeedData(builder);
        }
    }

    public static class GenerateSeedData
    {
        public static void SeedData(this ModelBuilder builder)
        {
            var c = new Contact
            {
                ContactId = 1,
                Name = "John Doe",
                DateOfBirth = new DateTime(1990, 8, 8),
                Address = new Address
                {
                    City = "Zagreb",
                    Country = "Croatia",
                    StreetName = "Vukovarska 1",
                    StreetNumber = "1"
                } 
            };

            //var p = new PhoneNumber { PhoneId = 1, ContactId = 1, Contact = c, TelephoneNumber = "123-456-789" };
            //c.PhoneNumbers.Add(p);
/*
            builder.Entity<PhoneNumber>().HasData
            (
                p
            ); */

            builder.Entity<Contact>().HasData
            (
                c
            );
        }
    }
}
