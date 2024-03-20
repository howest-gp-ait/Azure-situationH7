using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using Ait.DefaultSituation.Core.Services;

namespace Ait.DefaultSituation.Core.Entities
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]

    public class OU
    {
        public string Path { get; set; }
        public string Name
        {
            get
            {
                string longName = DirectoryEntry.Name;
                string[] parts = longName.Split("=");
                return parts[1];
            }
        }
        public DirectoryEntry DirectoryEntry { get; set; } // het OU-object op de AD

        public OU(string path)
        {
            Path = path;
            DirectorySearcher directorySearcher = new DirectorySearcher(new DirectoryEntry(path))
            {
                Filter = "(objectCategory=organizationalUnit)",
                SearchScope = SearchScope.Base
            };
            DirectoryEntry = directorySearcher.FindOne().GetDirectoryEntry();
            if (DirectoryEntry != null)
            {
                Path = DirectoryEntry.Path;
            }

        }
        public override string ToString()
        {
            return Path;
        }
    }
}
