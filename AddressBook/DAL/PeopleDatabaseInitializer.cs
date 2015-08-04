using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

namespace AddressBook.DAL
{
    public class PeopleDatabaseInitializer : DropCreateDatabaseIfModelChanges<AddressBookContext>
    {
        protected override void Seed(AddressBookContext context)
        {
            GetPeople().ForEach(p => context.People.Add(p));
        }

        private static List<Person> GetPeople()
        {
            var people = new List<Person> 
            { 
             new Person 
             { 
              PersonId= 1, 
              FirstName = "John",
              LastName = "Q",
              City = "Indiana",
              Country = "USA",
              Email ="",
              PhoneNumber = "111", 
              AddedBy = "user44"
             },
             new Person 
             { 
              PersonId= 2, 
              FirstName = "Max",
              LastName = "Schmell",
              City = "Berlin",
              Country = "Germany",
              Email ="",
              PhoneNumber = "099524886",
              AddedBy = "user44"
             },
             new Person 
             { 
              PersonId= 3, 
              FirstName = "Jane",
              LastName = "Doe",
              City = "London",
              Country = "England",
              Email ="",
              PhoneNumber = "5234962",
              AddedBy = "noUSER"
             }
            };

            return people;
        }


    }
}