using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AddressBook.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Core;
using System.Collections;

namespace AddressBook.Admin
{
    public partial class AdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindUsersGridView();
                //canEditRoleCheckBox.Visible = false;
                rolesCheckBoxes.Visible = false;
            }            
        }

        protected void BindUsersGridView()
        {
            try
            {
                ApplicationUserRepository context = new ApplicationUserRepository();
                var query = context.GetAppUsers();
                usersGridView.DataSource = query;
                usersGridView.DataBind(); 
            }
            catch (Exception)
            {
                lblMessage.Text = "An error occured while retrieving users from database. Please try again.";
            }
        }
     
        protected void usersGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    ApplicationUserRepository context1 = new ApplicationUserRepository();
                    LiteralControl label3 = (LiteralControl)e.Row.Cells[4].Controls[0];

                    //var userName = usersGridView.DataKeys[e.Row.RowIndex].Values[0];
                    string userID = usersGridView.DataKeys[e.Row.RowIndex].Values[1].ToString();

                    // check if appUser has Role
                    if (context1.appUserHasRole(userID))
                    {
                        string roleName = context1.GetUserRoleName(userID);
                        // set text for roleName label in usersGridView
                        label3.Text = roleName;
                    }
                    else
                    {
                        label3.Text = "N/A";
                    }

                    // reference Delete button
                    LinkButton btnDelete = (LinkButton)e.Row.Cells[5].Controls[0];

                    btnDelete.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to delete this record: " +
                                                DataBinder.Eval(e.Row.DataItem, "UserName") + " -UserID: " + usersGridView.DataKeys[e.Row.RowIndex].Values[1] + "')");

                    ////////////////////////
                    string userName = context1.GetAppUserByID(userID.ToString()).First().UserName;
                    //if selected user is 'canEditUser' then do not allow Role change and disable 'Delete' button in usersGridView
                    if (userName == "canEditUser")
                    {
                        rolesCheckBoxes.Visible = false;                                             
                        btnDelete.Visible = false;
                    }
                    //////////////
                }
                catch (Exception)
                {
                    lblMessage.Text = "An error occured while populating GridView. Please try again.";                     
                }
                
            }  
        }

        protected void usersGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string userID = e.Keys[1].ToString();
            try
            {
                ApplicationUserRepository context = new ApplicationUserRepository();
                context.DeleteUserByID(userID);
                // Rebind the gridView
                BindUsersGridView();
                Response.Redirect("~/Admin/AdminPage.aspx");
            }
            catch (OptimisticConcurrencyException)
            {
                lblMessage.Text = "There was an error while deleting record from database. The record you attempted to delete was modified by another " +
                                 "user after you got the original value. The delete operation was canceled.";
            }
            catch (ArgumentNullException)
            {
                lblMessage.Text = "An error occured while deleting user information. Make sure that user exists.";
            }
            catch (Exception)
            {
                lblMessage.Text = "An error occured while deleting user information. Please try again.";
            }
        }

        protected void usersGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            string userID = usersGridView.SelectedDataKey.Values[1].ToString();

            if (UserDetailsView.CurrentMode.Equals(DetailsViewMode.ReadOnly))
            {
                UserDetailsView.ChangeMode(DetailsViewMode.Edit);
            }
            BindUserDetailsView(userID);
            lblMessage.Text = "";
        }

        protected void BindUserDetailsView(string userID)
        {
            try
            {
               ApplicationUserRepository context1 = new ApplicationUserRepository();
                IQueryable<ApplicationUser> query1 = (IQueryable<ApplicationUser>)context1.GetAppUserByID(userID);
                UserDetailsView.DataSource = query1.ToList();
                UserDetailsView.DataBind();
            }
            catch (ArgumentNullException)
            {
                lblMessage.Text = "An error occured while retrieving record from database. Make sure that user exists.";
            }
            catch (Exception)
            {
                lblMessage.Text = "An error occured while retrieving user information. Please try again.";
            }
        }

        protected void usersGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            usersGridView.PageIndex = e.NewPageIndex;
            BindUsersGridView();
        }
        
        protected void UserDetailsView_ModeChanging(object sender, DetailsViewModeEventArgs e)
        {
            if (e.CancelingEdit)
            {
                UserDetailsView.ChangeMode(DetailsViewMode.ReadOnly);
                string userID = usersGridView.SelectedDataKey.Values[1].ToString();
                BindUserDetailsView(userID);
            }
                // if not canceling than edit button is clicked
            else
            {
                UserDetailsView.ChangeMode(DetailsViewMode.Edit);
                string userID = usersGridView.SelectedDataKey.Values[1].ToString();
                BindUserDetailsView(userID);
            }
                                                 
            }

        protected void UserDetailsView_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
          string userID = e.Keys[0].ToString();
          string email = string.Empty;
          if (e.NewValues["Email"] == null || string.IsNullOrEmpty(e.NewValues["Email"].ToString()))
          {
              email = string.Empty;
          }
            else
          {
              email = e.NewValues["Email"].ToString();
          }
                        
          //string userName = e.NewValues["UserName"].ToString();
            
            try 
	        {
            ApplicationUserRepository context = new ApplicationUserRepository();
            context.UpdateUserByID(userID, email);
            
                if (canEditRoleCheckBox.Checked)
	                {
                     // Access the application context and create result variables.
                     ApplicationDbContext context1 = new ApplicationDbContext();
                     // IdentityResult IdRoleResult;
                     IdentityResult IdUserResult;

                     // Create a RoleStore object by using the ApplicationDbContext object. 
                     // The RoleStore is only allowed to contain IdentityRole objects.
                     var roleStore = new RoleStore<IdentityRole>(context1);

                     // Create a RoleManager object that is only allowed to contain IdentityRole objects.
                     // When creating the RoleManager object, you pass in (as a parameter) a new RoleStore object. 
                     var roleMgr = new RoleManager<IdentityRole>(roleStore);
                     string roleName = "member";
                     var role = roleMgr.FindByName(roleName).Id;

                     var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context1));
                                         
                         if (!userMgr.IsInRole(userMgr.FindById(userID).Id, "canEdit"))
                         {
                            IdUserResult = userMgr.AddToRole(userMgr.FindById(userID).Id, "canEdit");
                            IdUserResult = userMgr.RemoveFromRole(userMgr.FindById(userID).Id, roleName);
                         }  
	                }
            
                //////

                if (CheckBox2.Checked)
                {
                    // Access the application context and create result variables.
                    ApplicationDbContext context1 = new ApplicationDbContext();
                    // IdentityResult IdRoleResult;
                    IdentityResult IdUserResult;

                    // Create a RoleStore object by using the ApplicationDbContext object. 
                    // The RoleStore is only allowed to contain IdentityRole objects.
                    var roleStore = new RoleStore<IdentityRole>(context1);

                    // Create a RoleManager object that is only allowed to contain IdentityRole objects.
                    // When creating the RoleManager object, you pass in (as a parameter) a new RoleStore object. 
                    var roleMgr = new RoleManager<IdentityRole>(roleStore);
                    var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context1));

                    //var userRoles = await UserManager.GetRolesAsync(user.Id);
                    var userRoles = userMgr.GetRoles(userID);

                    string roleName = "canEdit";
                    var role = roleMgr.FindByName(roleName).Id;
                    
                    // if user is 'Admin' - 'canEdit' then remove user from that Role
                    if (userMgr.IsInRole(userMgr.FindById(userID).Id, "canEdit"))
                    {
                       // IdUserResult = userMgr.RemoveFromRole(userMgr.FindByName(userID).Id, "canEdit");
                        IdUserResult = userMgr.RemoveFromRole(userMgr.FindById(userID).Id, roleName);
                        IdUserResult = userMgr.AddToRole(userMgr.FindById(userID).Id, "member");
                    }
                }

                /////
                
             // set text(RoleName) for each cell in Role Column
             foreach (GridViewRow row1 in usersGridView.Rows)
             {
                if (row1.RowType == DataControlRowType.DataRow)
                {
                   // ApplicationUserRepository context1 = new ApplicationUserRepository();
                    //LiteralControl label3 = (LiteralControl)e.Row.Cells[4].Controls[0];
                    LiteralControl label3 = (LiteralControl)row1.Cells[4].Controls[0];
                    //var userName = usersGridView.DataKeys[e.Row.RowIndex].Values[0];
                    string userID2 = usersGridView.DataKeys[row1.RowIndex].Values[1].ToString();

                    // check if appUser has Role
                    if (context.appUserHasRole(userID2))
                    {
                        string roleName = context.GetUserRoleName(userID2);
                        // set text for roleName label in usersGridView
                        label3.Text = roleName;
                    }
                    else
                    {
                        label3.Text = "N/A";
                    }
                }
             }

             BindUsersGridView();
             ShowCheckBoxes(userID);
             lblMessage.Text = "User data updated!";

            }
            catch (OptimisticConcurrencyException)
            {
                lblMessage.Text = "There was an error while updating record from database. The record you attempted to update was modified by another " +
                                 "user after you got the original value. The update operation was canceled.";
            }
            catch (ArgumentNullException)
            {
                lblMessage.Text = "An error occured while updating user information. Make sure that user exists.";
            }
            catch (Exception)
            {
                lblMessage.Text = "An error occured while updating user information. Please try again.";
            }
        }

        protected void usersGridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {                     
            string userID2 = usersGridView.DataKeys[e.NewSelectedIndex].Values[1].ToString();
            ShowCheckBoxes(userID2);

            ////////////////////////            
            //int selectedRowIndex = usersGridView.SelectedRow.RowIndex;
            int selectedRowIndex = usersGridView.Rows[e.NewSelectedIndex].RowIndex;
            var context = new ApplicationUserRepository();
            string userName = context.GetAppUserByID(userID2.ToString()).First().UserName;
            
            // show checkboxes depending on user roles - if use is admin then don't show 'Add user to Admin role'
            if (userName == "canEditUser")
            {
                rolesCheckBoxes.Visible = false;
            }
            else
            {
                rolesCheckBoxes.Visible = true;
            }
            //////////////

            foreach (GridViewRow row1 in usersGridView.Rows)
            {
                if (row1.RowType == DataControlRowType.DataRow)
                {
                    try
                    {
                         ApplicationUserRepository context1 = new ApplicationUserRepository();
                        //LiteralControl label3 = (LiteralControl)e.Row.Cells[4].Controls[0];
                        LiteralControl label3 = (LiteralControl)row1.Cells[4].Controls[0];
                        //var userName = usersGridView.DataKeys[e.Row.RowIndex].Values[0];
                        string userID = usersGridView.DataKeys[row1.RowIndex].Values[1].ToString();

                        // check if appUser has Role
                        if (context1.appUserHasRole(userID))
                        {
                            string roleName = context1.GetUserRoleName(userID);
                            // set text for roleName label in usersGridView
                            label3.Text = roleName;
                        }
                        else
                        {
                            label3.Text = "N/A";
                        }
                    }
                    catch (Exception)
                    {
                        lblMessage.Text = "An error occured. Please try again.";
                    }
                    
                }
            }
        }

        protected void ShowCheckBoxes(string userID)
        {
            canEditRoleCheckBox.Checked = false;
            CheckBox2.Checked = false;
            // Access the application context and create result variables.
            ApplicationDbContext context = new ApplicationDbContext();           
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            
            if (userMgr.IsInRole(userMgr.FindById(userID).Id, "canEdit"))
            {
                addToAdmin.Visible = false;
                removeFromAdmin.Visible = true;
            }
            else
            {
                addToAdmin.Visible = true;
                removeFromAdmin.Visible = false;
            }
        }

    }
}