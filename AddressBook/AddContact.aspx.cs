using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AddressBook.DAL;
using System.Data.SqlClient;
using System.Data.Entity.Core;

namespace AddressBook
{
    public partial class AddContact : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
             AddContactDetailsView.EnableDynamicData(typeof(Person));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {                        
                //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Access denied - You are not authorized to access this page. Please Login or Register to view this page.');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", " alert('Access denied - You are not authorized to access this page. Please Login or Register to view this page.'); window.location='../Account2/Login.aspx';", true);
            }
        }

        protected void AddContactDetailsView_ModeChanging(object sender, DetailsViewModeEventArgs e)
        {
            if (e.CancelingEdit)
            {
                Response.Redirect("~/Default.aspx");
            }
        }
               
        protected void AddContactDetailsView_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            string firstName = e.Values["FirstName"].ToString();
            string lastName = e.Values["LastName"].ToString();
            string city = e.Values["City"].ToString();
            string country = e.Values["Country"].ToString();
            string phone = e.Values["PhoneNumber"].ToString();
            string email = string.Empty;

            if (e.Values["Email"] != null)
                email = e.Values["Email"].ToString();
            else
            {
                email = "";
            }
            
     
                  
            try
            {
                AddressBookRepository context = new AddressBookRepository();
                context.InsertContact(firstName, lastName, city, country, phone, email, User.Identity.Name);
                lblMessage.Text = "Contact Added: " + e.Values[0] + e.Values[1];
            }

            catch (OptimisticConcurrencyException ocex)
            {
                lblMessage.Text = ocex.ToString();
            }             
            catch (Exception)
            {
                lblMessage.Text = "An error occured on inserting new contact. Try again.";
            }

        }

      
    }
}