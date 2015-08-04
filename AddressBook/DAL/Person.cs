using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AddressBook.DAL
{
    public class Person
    {
        [ScaffoldColumn(false)]
        public int PersonId { get; set; }
        [Required, StringLength(20), Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required, StringLength(20), Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required, StringLength(20), Display(Name = "City")]
        public string City { get; set; }
        [Required, StringLength(20), Display(Name = "Country")]
        public string Country { get; set; }
        [Required, StringLength(20), Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        
        public string Email { get; set; }

        [StringLength(20), Display(Name = "Added By")]
        public string AddedBy { get; set; }
    }
}