using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

namespace AddressBook.DAL
{
    public class AddressBookContext : DbContext
    {
        public AddressBookContext() : base("AddresssBook") { }
        public DbSet<Person> People { get; set; }                
    }
}