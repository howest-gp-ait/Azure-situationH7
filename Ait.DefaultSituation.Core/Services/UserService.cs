using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using Ait.DefaultSituation.Core.Entities;

namespace Ait.DefaultSituation.Core.Services
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]

    public class UserService
    {
        public static bool AddUserToGroup(GroupPrincipal groupPrincipal, UserPrincipal userPrincipal)
        {
            try
            {
                groupPrincipal.Members.Add(userPrincipal);
                groupPrincipal.Save();
                return true;
            }
            catch(Exception fout)
            {
                return false;
            }
        }
        public static User CreateUser(OU targetOU, string firstname, string lastname, string loginName, string password, bool isEnabled, DateTime? accountExpirationDate)
        {
            // onderstaande zou moeten werken (= gebruiker meteen in correcte OU plaatsen) maar werkt niet
            //PrincipalContext principalContext = new PrincipalContext(ContextType.Domain, AD.ADDomainNameShort, targetOU.Path);
            // dan maar nieuwe gebruiker in de OU in de "CN=Users,DC=ait,DC=local" plaatsen en achteraf verplaatsen naar targetOU
            PrincipalContext principalContext = new PrincipalContext(ContextType.Domain);  
            UserPrincipal userPrincipal = new UserPrincipal(principalContext);
            userPrincipal.GivenName = firstname;
            userPrincipal.Surname = lastname;
            userPrincipal.DisplayName = firstname + " " + lastname;
            userPrincipal.SamAccountName = loginName;
            userPrincipal.UserPrincipalName = loginName + AD.ADDomainEmail;
            userPrincipal.SetPassword(password);
            userPrincipal.Enabled = isEnabled;
            userPrincipal.AccountExpirationDate = accountExpirationDate;
            try
            {
                userPrincipal.Save();
                User user = new User(userPrincipal.SamAccountName);
                OUService.MovePrincipal(user, targetOU);
                return user;
            }
            catch(Exception error)
            {
                throw new Exception(error.Message);
            }
        }
        public static bool UserExists(string loginname)
        {
            PrincipalContext principalContext = new PrincipalContext(ContextType.Domain);
            UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(principalContext, IdentityType.SamAccountName, loginname);
            if (userPrincipal == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
