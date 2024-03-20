using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using Ait.DefaultSituation.Core.Services;

namespace Ait.DefaultSituation.Core.Entities
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]

    public class User
    {
        public string SamAccountName { get; set; } // bv jan.de.deurwaerder
        public UserPrincipal UserPrincipal { get; set; }  // het user-object op de AD
        public DirectoryEntry DirectoryEntry { get; set; }  // de OU waar user lid van is
        public User(string samAccountName)
        {
            SamAccountName = samAccountName;

            PrincipalContext principalContext = new PrincipalContext(ContextType.Domain);
            UserPrincipal = UserPrincipal.FindByIdentity(principalContext, IdentityType.SamAccountName, samAccountName);
            if (UserPrincipal == null)
            {
                throw new Exception($"{samAccountName} kon niet gevonden worden in AD");
            }
            DirectoryEntry = (DirectoryEntry)UserPrincipal.GetUnderlyingObject();
        }
        public override string ToString()
        {
            return UserPrincipal.Name;
        }


    }
}
