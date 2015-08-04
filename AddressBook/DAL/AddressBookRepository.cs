using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.Entity.Core.Objects;
using System.Data.Objects;

namespace AddressBook.DAL
{
    public class AddressBookRepository
    {
        private AddressBookContext context = new AddressBookContext();

        public List<Person> GetPeople()
        {
            try
            {
                return context.People.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<Person> GetPersonByID(int personID)
        {
            try
            {
                return context.People.Where(p => p.PersonId == personID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdatePersonByID(int personID, string firstName, string lastName, string city, string country, string phoneNumber, string email, string addedBy)
        {
            try
            {
                var personToUpdate = context.People.Where(p => p.PersonId == personID).FirstOrDefault();
                personToUpdate.FirstName = firstName;
                personToUpdate.LastName = lastName;
                personToUpdate.City = city;
                personToUpdate.Country = country;
                personToUpdate.PhoneNumber = phoneNumber;
                personToUpdate.Email = email;
                personToUpdate.AddedBy = addedBy;       

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletePersonByID(int personID) {
        
            try
            {
                var personToDelete = context.People.Where(p => p.PersonId == personID).FirstOrDefault();
                context.People.Remove(personToDelete);
                context.SaveChanges();
            }
            catch (OptimisticConcurrencyException ocex)
            {
                throw ocex;
            }
            catch (ArgumentNullException argex)
            {
                throw argex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertContact(string firstName, string lastName, string city, string country, string phone, string email, string addedBy)
        {
            try
            {
                Person insertContact = new Person();
                insertContact.FirstName = firstName;
                insertContact.LastName = lastName;
                insertContact.City = city;
                insertContact.Country = country;
                insertContact.PhoneNumber = phone;
                insertContact.Email = email;
                insertContact.AddedBy = addedBy;

                context.People.Add(insertContact);
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}