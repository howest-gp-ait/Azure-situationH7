using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using Ait.DefaultSituation.Core.Services;


namespace Ait.DefaultSituation.Core.Entities
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]

    public class Group
    {
        public string SamAccountName { get; set; }
        public GroupPrincipal GroupPrincipal { get; set; } // het Group object op de AD
        public DirectoryEntry DirectoryEntry { get; set; }  // de OU waar group lid van is

        public Group(string samAccountName)
        {
            SamAccountName = samAccountName;
            PrincipalContext principalContext = new PrincipalContext(ContextType.Domain);
            GroupPrincipal = GroupPrincipal.FindByIdentity(principalContext, IdentityType.SamAccountName, samAccountName);
            if (GroupPrincipal == null)
            {
                throw new Exception($"{samAccountName} kon niet gevonden worden in AD");
            }
            DirectoryEntry = (DirectoryEntry)GroupPrincipal.GetUnderlyingObject();
        }
        public override string ToString()
        {
            return GroupPrincipal.Name;
        }


    }
}
