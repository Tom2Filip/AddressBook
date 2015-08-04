using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AddressBook.DAL;
using System.Data.Entity.Core;

namespace AddressBook
{
    public partial class PersonDetails : System.Web.UI.Page
    {
        public int personID;

        protected void Page_Init(object sender, EventArgs e)
        {
            personDetailsView.EnableDynamicData(typeof(Person));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // if user typed link directly in addresss bar then redirect it to home page (user can only come to this page by clicking on a link - "View Details")
            if (Request.UrlReferrer != null)
            {
                lblMessage.Text = "Came from" + Request.UrlReferrer.ToString() + "page.";
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
            
            // check QueryString not null OR empty - if null OR empty redirect to Vehicles.aspx           
            if (Request.QueryString["personID"] == null || Request.QueryString["personID"].ToString() == "")
           {
                Response.Redirect("Default.aspx");
            }

            personID = Convert.ToInt32(Request.QueryString["personID"]);

            if (!IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                {
                    AddressBookRepository context2 = new AddressBookRepository();

                    try
                    {
                        IQueryable<Person> query2 = context2.GetPersonByID(personID);
                        personDetailsView.DataSource = query2.ToList();
                        personDetailsView.DataBind();

                    }
                    catch (Exception)
                    {
                        lblMessage.Text = "An error occurred. Please try again.";
                    }
                }
                else
                {
                    //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Access denied - You are not authorized to access this page. Please Login or Register to view this page.');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", " alert('Access denied - You are not authorized to access this page. Please Login or Register to view this page.'); window.location='../Account2/Login.aspx';", true);
                }
          }
             
        }


        protected void personDetailsView_ModeChanging(object sender, DetailsViewModeEventArgs e)
        {
            if (e.CancelingEdit == true)
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void personDetailsView_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            try 
	        {
                int id = Convert.ToInt32(e.Keys["PersonId"]);
                lblMessage.Text = id.ToString();

                string firstName = e.NewValues["FirstName"].ToString();
                string lastName = e.NewValues["LastName"].ToString();
                string city = e.NewValues["City"].ToString();
                string country = e.NewValues["Country"].ToString();
                string phoneNumber = e.NewValues["PhoneNumber"].ToString();

                string email = string.Empty;
                if (e.NewValues["Email"] == null)
                { email = ""; }
                else
                {
                    email = e.NewValues["Email"].ToString();
                }
                

                AddressBookRepository context = new AddressBookRepository();
                context.UpdatePersonByID(id, firstName, lastName, city, country, phoneNumber, email, User.Identity.Name);
	        }            
            catch (UpdateException)
            {
                lblMessage.Text = "An error occurred while updating person information. Please try again.";
            }
            catch (ArgumentNullException)
            {
                lblMessage.Text = "An error occurred while updating person information. Make sure that person exists.";
            }
            catch (Exception)
            {
                lblMessage.Text = "An error occurred while updating person information. Please try again.";
            }
        }


    }
}