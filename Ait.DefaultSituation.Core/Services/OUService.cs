using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using Ait.DefaultSituation.Core.Entities;

namespace Ait.DefaultSituation.Core.Services
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]

    public class OUService
    {

        public static void MovePrincipal(User user, OU destinationOU)
        {
            DirectoryEntry currentDirectoryEntry = user.DirectoryEntry;
            DirectoryEntry destinationDirectory = destinationOU.DirectoryEntry;
            currentDirectoryEntry.MoveTo(destinationDirectory);
        }
        public static void MovePrincipal(Group group, OU destinationOU)
        {
            DirectoryEntry currentDirectoryEntry = group.DirectoryEntry;
            DirectoryEntry destinationDirectory = destinationOU.DirectoryEntry;
            currentDirectoryEntry.MoveTo(destinationDirectory);
        }

        public static bool OUExists(string path)
        {
            if (DirectoryEntry.Exists(path))
                return true;
            else
                return false;

        }
        public static bool CreateOU(string parentPath, string ouName)
        {
            if (!OUExists(parentPath))
            {
                return false;
            }
            try
            {
                if (ouName.Length > 3)
                {
                    if (ouName.Substring(0, 3).ToUpper() != "OU=")
                        ouName = "OU=" + ouName;
                }
                else
                {
                    ouName = "OU=" + ouName;
                }
                DirectoryEntry directoryEntry = new DirectoryEntry(parentPath);
                DirectoryEntry newOU = directoryEntry.Children.Add(ouName, "OrganizationalUnit");
                newOU.CommitChanges();
                return true;
            }
            catch (Exception fout)
            {
                return false;
            }

        }



    }
}
