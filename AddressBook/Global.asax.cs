using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using AddressBook;
using System.Data.Entity;
using AddressBook.DAL;
using AddressBook.Logic;

namespace AddressBook
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           // AuthConfig.RegisterOpenAuth();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Code that runs on application startup
            // Initialize the AddressBook database. 
            Database.SetInitializer(new PeopleDatabaseInitializer());

            // Create the administrator role and user.
            RoleActions roleActions = new RoleActions();
            roleActions.AddUserAndRole();
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            //Server.Transfer("~/ErrorPages/ApplicationError.aspx");
            
            // set in web.config under 'customerrors'

        }
    }
}
