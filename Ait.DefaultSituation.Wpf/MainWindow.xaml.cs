using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ait.DefaultSituation.Core.Entities;
using Ait.DefaultSituation.Core.Services;

namespace Ait.DefaultSituation.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Klik op OK om het proces te starten.  Dit kan even duren ...", "Start process", MessageBoxButton.OK, MessageBoxImage.Information);
            List<string> RequiredOUs = new List<string>();
            RequiredOUs.Add("LDAP://OU=OUGebruikers,DC=AIT,DC=LOCAL");
            RequiredOUs.Add("LDAP://OU=OUPersoneel,OU=OUGebruikers,DC=AIT,DC=LOCAL");
            RequiredOUs.Add("LDAP://OU=OUDocenten,OU=OUPersoneel,OU=OUGebruikers,DC=AIT,DC=LOCAL");
            RequiredOUs.Add("LDAP://OU=OUAdministratie,OU=OUPersoneel,OU=OUGebruikers,DC=AIT,DC=LOCAL");
            RequiredOUs.Add("LDAP://OU=OUDirectie,OU=OUPersoneel,OU=OUGebruikers,DC=AIT,DC=LOCAL");
            RequiredOUs.Add("LDAP://OU=OULeerlingen,OU=OUGebruikers,DC=AIT,DC=LOCAL");
            RequiredOUs.Add("LDAP://OU=OUGroepen,DC=AIT,DC=LOCAL");
            RequiredOUs.Add("LDAP://OU=OUPersoneelGroepen,OU=OUGroepen,DC=AIT,DC=LOCAL");
            RequiredOUs.Add("LDAP://OU=OULeerlingGroepen,OU=OUGroepen,DC=AIT,DC=LOCAL");

            foreach (string path in RequiredOUs)
            {
                if(!OUService.OUExists(path))
                {
                    string searchPath = path.Replace("LDAP://", "");
                    string[] parts = searchPath.Split(",");
                    string oUName = parts[0];
                    string parentPath = "LDAP://" + searchPath.Replace(oUName + ",", "");
                    OUService.CreateOU(parentPath, oUName);
                    lstFeedback.Items.Insert(0, "Created " + path);
                }
                else
                {
                    lstFeedback.Items.Insert(0, "OU ok : " + path);
                }
            }

            var usersList = new[]
            {
                new { firstname = "Bart", lastname = "Winters", path = "LDAP://OU=OUAdministratie,OU=OUPersoneel,OU=OUGebruikers,DC=AIT,DC=LOCAL" },
                new { firstname = "Steve", lastname = "Steyaert", path = "LDAP://OU=OUDocenten,OU=OUPersoneel,OU=OUGebruikers,DC=AIT,DC=LOCAL" },
                new { firstname = "Camiel", lastname = "Kafka", path = "LDAP://OU=OUDocenten,OU=OUPersoneel,OU=OUGebruikers,DC=AIT,DC=LOCAL" },
                new { firstname = "Marie", lastname = "Curry", path = "LDAP://OU=OUDocenten,OU=OUPersoneel,OU=OUGebruikers,DC=AIT,DC=LOCAL" },
                new { firstname = "Marianne", lastname = "Saelens", path = "LDAP://OU=OUDocenten,OU=OUPersoneel,OU=OUGebruikers,DC=AIT,DC=LOCAL" },
                new { firstname = "Shanaia", lastname = "Gooris", path = "LDAP://OU=OULeerlingen,OU=OUGebruikers,DC=AIT,DC=LOCAL" },
                new { firstname = "Kenji", lastname = "Gooris", path = "LDAP://OU=OULeerlingen,OU=OUGebruikers,DC=AIT,DC=LOCAL" },
                new { firstname = "Charly", lastname = "Sheen", path = "LDAP://OU=OULeerlingen,OU=OUGebruikers,DC=AIT,DC=LOCAL" },
                new { firstname = "Emilio", lastname = "Estevez", path = "LDAP://OU=OULeerlingen,OU=OUGebruikers,DC=AIT,DC=LOCAL" },
                new { firstname = "Uthred", lastname = "Babbenburg", path = "LDAP://OU=OULeerlingen,OU=OUGebruikers,DC=AIT,DC=LOCAL" },
                new { firstname = "Paco", lastname = "Di Lucia", path = "LDAP://OU=OULeerlingen,OU=OUGebruikers,DC=AIT,DC=LOCAL" },
                new { firstname = "Bruce", lastname = "Springsteen", path = "LDAP://OU=OULeerlingen,OU=OUGebruikers,DC=AIT,DC=LOCAL" },
                new { firstname = "Karel", lastname = "Degrootte", path = "LDAP://OU=OULeerlingen,OU=OUGebruikers,DC=AIT,DC=LOCAL" }

            };
            string username;
            string pasword = "P@sw00rd";
            foreach(var item in usersList)
            {
                username = item.firstname.Replace(" ", ".").ToLower() + "." + item.lastname.Replace(" ", ".").ToLower();
                if(!UserService.UserExists(username))
                {
                    OU oU = new OU(item.path);
                    UserService.CreateUser(oU, item.firstname, item.lastname, username, pasword, true, null);
                    lstFeedback.Items.Insert(0, $"User {username} created");
                }
                else
                {
                    lstFeedback.Items.Insert(0, $"User {username} OK");
                }
            }

            var groupList = new[]
            {
                new { groupname = "GRPKlasA", path = "LDAP://OU=OULeerlingGroepen,OU=OUGroepen,DC=AIT,DC=LOCAL" },
                new { groupname = "GRPKlasB", path = "LDAP://OU=OULeerlingGroepen,OU=OUGroepen,DC=AIT,DC=LOCAL" },
                new { groupname = "GRPKlasC", path = "LDAP://OU=OULeerlingGroepen,OU=OUGroepen,DC=AIT,DC=LOCAL" },
                new { groupname = "GRPAdministratie", path = "LDAP://OU=OUPersoneelGroepen,OU=OUGroepen,DC=AIT,DC=LOCAL" },
                new { groupname = "GRPDirectie", path = "LDAP://OU=OUPersoneelGroepen,OU=OUGroepen,DC=AIT,DC=LOCAL" },
                new { groupname = "GRPDocenten", path = "LDAP://OU=OUPersoneelGroepen,OU=OUGroepen,DC=AIT,DC=LOCAL" },
                new { groupname = "GRPLeerlingen", path = "LDAP://OU=OULeerlingGroepen,OU=OUGroepen,DC=AIT,DC=LOCAL" },
				new { groupname = "GRPPersoneel", path = "LDAP://OU=OUPersoneelGroepen,OU=OUGroepen,DC=AIT,DC=LOCAL" }

            };
            foreach (var item in groupList)
            {
                if(!GroupService.GroupExists(item.groupname))
                {
                    OU oU = new OU(item.path);
                    GroupService.CreateGroup(oU, item.groupname);
                    lstFeedback.Items.Insert(0, $"Group {item.groupname} created");
                }
                else
                {
                    lstFeedback.Items.Insert(0, $"Group {item.groupname} OK");
                }
            }

            var memberList = new[]
            {
                new{groupname = "GRPAdministratie", username = "bart.winters"},
                new{groupname = "GRPAdministratie", username = "steve.steyaert"},
                new{groupname = "GRPDocenten", username = "steve.steyaert"},
                new{groupname = "GRPDocenten", username = "camiel.kafka"},
                new{groupname = "GRPDocenten", username = "marie.curry"},
                new{groupname = "GRPDocenten", username = "marianne.saelens"},
                new{groupname = "GRPDirectie", username = "marie.curry"},
                new{groupname = "GRPKlasA", username = "shanaia.gooris"},
                new{groupname = "GRPKlasB", username = "kenji.gooris"},
                new{groupname = "GRPKlasA", username = "charly.sheen"},
                new{groupname = "GRPKlasB", username = "emilio.estevez"},
                new{groupname = "GRPKlasA", username = "uthred.babbenburg"},
                new{groupname = "GRPKlasB", username = "paco.di.lucia"},
                new{groupname = "GRPKlasC", username = "bruce.springsteen"},
                new{groupname = "GRPKlasC", username = "karel.degrootte"}
            };
            foreach (var item in memberList)
            {
                User user = new User(item.username);
                Group group = new Group(item.groupname);
                UserService.AddUserToGroup(group.GroupPrincipal, user.UserPrincipal);
                lstFeedback.Items.Insert(0, $"{item.username} belongs to {item.groupname}");
            }
            var groupMembers = new[]
            {
                new{groupname = "GRPAdministratie", groupParentName = "GRPPersoneel"},
                new{groupname = "GRPDocenten", groupParentName = "GRPPersoneel"},
                new{groupname = "GRPDirectie", groupParentName = "GRPPersoneel"},
				new{groupname = "GRPKlasA", groupParentName = "GRPLeerlingen"},
                new{groupname = "GRPKlasB", groupParentName = "GRPLeerlingen"},
                new{groupname = "GRPKlasC", groupParentName = "GRPLeerlingen"}
            };
            foreach (var item in groupMembers)
            {
                Group groupParent = new Group(item.groupParentName);
                Group group = new Group(item.groupname);
                GroupService.AddGroupToGroup(group.GroupPrincipal, groupParent.GroupPrincipal);
                lstFeedback.Items.Insert(0, $"{item.groupname} belongs to {item.groupParentName}");
            }

        }
    }
}
