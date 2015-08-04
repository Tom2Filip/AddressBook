using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Core;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AddressBook.DAL
{
    public class ApplicationUserRepository
    {
      private ApplicationUserContext context1 = new ApplicationUserContext();

        public IQueryable<ApplicationUser> GetAppUserByID(string userID)
        {
            try
            {
                var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context1));
                var userToGet = userMgr.Users.Where(us => us.Id == userID);
                return userToGet as IQueryable<ApplicationUser>;  
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

        public List<ApplicationUser> GetAppUsers()
        {
            try
            {
                var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context1));
                var appUsers = userMgr.Users.ToList();
                return appUsers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateUserByID(string userID, string email)
        {
            try
            {
                var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context1));
                var userToUpdate1 = userMgr.FindById(userID);
                    userToUpdate1.Email = email;
                userMgr.Update(userToUpdate1);

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

        public void DeleteUserByID(string userID)
        {
            try
            {
                var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context1));
                var userToDelete = userMgr.FindById(userID);
                userMgr.DeleteAsync(userToDelete);
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

        public bool appUserHasRole(string userID) 
        {
            try
            {
                if (context1.Users.Where(us => us.Id == userID.ToString()).First().Roles.Any())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetUserRoleName(string userID)
        {
            try
            {
                string roleID = context1.Users.Where(us => us.Id == userID.ToString()).First().Roles.FirstOrDefault().RoleId;
                string roleName = context1.Roles.Where(r => r.Id == roleID).FirstOrDefault().Name;
                return roleName;
            }
            catch (Exception ex)
            {
               throw ex;
            }
        }

    }
}