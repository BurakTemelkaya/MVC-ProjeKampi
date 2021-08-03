using BusinessLayer.Concrete;
using DataAccesLayer.Concrete;
using DataAccesLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace MvcProjeKampi.Roles
{
    public class AdminRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        AdminManager adminManager = new AdminManager(new EfAdminDal());
        WriterManager writerManager = new WriterManager(new EfWriterDal());
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            /*Context c = new Context();
            var x = c.Admins.FirstOrDefault(y=> y.AdminUserName.ToString()==username);
            return new string[] { x.AdminRole };*/
            using (var crypto = new System.Security.Cryptography.HMACSHA512())
            {
                var mailCrypto = crypto.ComputeHash(Encoding.UTF8.GetBytes(username));
                var admin = adminManager.GetList();
                var writer = writerManager.GetList();

                if (admin != null)
                {
                    foreach (var item in admin)
                    {
                        for (int i = 0; i < mailCrypto.Length; i++)
                        {
                            if (mailCrypto[i] == item.AdminMail[i])
                            {
                                return new string[] { item.AdminRole };
                            }
                        }
                    }
                }
                return new string[] { };
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}