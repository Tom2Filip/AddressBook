using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AddressBook.DAL;
using System.Data.Entity.Core;
using Microsoft.AspNet.Identity;

namespace AddressBook
{
    public partial class _Default : Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            contactsGridView.EnableDynamicData(typeof(Person));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetPeople();        
            }
        }

        protected void GetPeople()
        {
            try
            {
                AddressBookRepository context = new AddressBookRepository();
                var query = context.GetPeople();
                contactsGridView.DataSource = query;
                contactsGridView.DataBind();
            }
            catch (Exception)
            {
                lblError.Text = "There was an error while retrieving data from database. Please try again.";                     
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                AddressBookRepository context = new AddressBookRepository();
                context.DeletePersonByID(1);
                GetPeople();
                lblError.Text = "Contact deleted.";            
            }
            catch (OptimisticConcurrencyException)
            {
                lblError.Text = "There was an error while deleting record from database. The record you attempted to delete was modified by another " +
                                "user after you got the original value. The delete operation was canceled.";
            }
            catch (ArgumentNullException)
            {
                lblError.Text = "There was an error while deleting record from database. Make sure that user exist.";
            }
            catch (Exception)
            {
                lblError.Text = "There was an error while deleting record from database. Please try again.";
            }
         }

        protected void contactsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
           // int index = e.RowIndex; // index of the row to delete -> where Delete button clicked 
            int personID = Convert.ToInt32(contactsGridView.DataKeys[e.RowIndex].Value);
            
            try
            {
                AddressBookRepository context = new AddressBookRepository();
                context.DeletePersonByID(personID);
                GetPeople();
                lblError.Text = "Contact " + " " + e.Values["FirstName"] + " " + e.Values["LastName"] + " deleted!";                
            }
            catch (OptimisticConcurrencyException)
            {
                lblError.Text = "There was an error while deleting record from database. The record you attempted to delete was modified by another " +
                                 "user after you got the original value. The delete operation was canceled.";
            }
            catch (ArgumentNullException)
            {
                lblError.Text = "An error occured while deleting contact. Make sure that contact exists.";
            }
            catch (Exception)
            {
                lblError.Text = "An error occured while deleting contact. Please try again.";
            }
        }

        protected void contactsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                /*
                 cell refers to the column index of your CommandField and ctrl refers to the control index (Delete button) within the cell you're referencing.
                 stackoverflow.com/questions/14397309/how-to-call-javascript-function-in-gridview-command-field
                 */
                // reference Delete button
                LinkButton btnDelete = (LinkButton)e.Row.Cells[7].Controls[0];
                
                btnDelete.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to delete this record: " +
                                            DataBinder.Eval(e.Row.DataItem, "FirstName") + " " + DataBinder.Eval(e.Row.DataItem, "LastName") + " PersonID: " + contactsGridView.DataKeys[e.Row.RowIndex].Values[0] + "')");

                int personID = Convert.ToInt16(contactsGridView.DataKeys[e.Row.RowIndex].Values[0]);

                             
                try
                {                                     
                    AddressBookRepository context = new AddressBookRepository();
                    var query = context.GetPersonByID(personID).FirstOrDefault();
                    string addedBy = query.AddedBy.Trim();

                    //  reference ViewDetails button
                    HyperLink btnDetails = (HyperLink)e.Row.Cells[8].Controls[0];
                    // show ViewDetails button only if current user is "Admin" or if contact is added by current user (logged in user)
                    btnDetails.Visible = ((User.IsInRole("canEdit")) || User.Identity.Name.ToUpper() == addedBy.ToUpper());
                    //User.Identity.GetUserId();
                    if ((User.IsInRole("canEdit")) || (User.Identity.Name.ToUpper() == addedBy.ToUpper())) 
                    {
                        btnDelete.Visible = true;
                    }
                    else
                    {
                        btnDelete.Visible = false;
                    }
                }
                catch (Exception)
                {
                    lblError.Text = "An error occurred. Please try again.";
                }                                             
                
            }
        }

              
    }
}