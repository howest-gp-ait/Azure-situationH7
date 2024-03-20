using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using Ait.DefaultSituation.Core.Entities;

namespace Ait.DefaultSituation.Core.Services
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]

    public class GroupService
    {
        public static bool AddGroupToGroup(GroupPrincipal childGroupPrincipal, GroupPrincipal parentGroupPrincipal)
        {
            try
            {
                parentGroupPrincipal.Members.Add(childGroupPrincipal);
                parentGroupPrincipal.Save();
                return true;
            }
            catch (Exception fout)
            {
                return false;
            }
        }

        public static Group CreateGroup(OU targetOU, string groupName)
        {
            PrincipalContext principalContext = new PrincipalContext(ContextType.Domain);
            GroupPrincipal groupPrincipal = new GroupPrincipal(principalContext);
            groupPrincipal.Name = groupName;
            groupPrincipal.SamAccountName = groupName;
            try
            {
                groupPrincipal.Save();
                Group group = new Group(groupPrincipal.SamAccountName);
                OUService.MovePrincipal(group, targetOU);
                return group;
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        public static bool GroupExists(string groupname)
        {
            PrincipalContext principalContext = new PrincipalContext(ContextType.Domain);
            GroupPrincipal groupPrincipal = GroupPrincipal.FindByIdentity(principalContext, IdentityType.Name, groupname);
            if (groupPrincipal == null)
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
