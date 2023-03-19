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
using MySql.Data.MySqlClient;

namespace Algs
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class User
        {
            public string id { get; set; }
            public string name { get; set; }
            public string dateOfBirth { get; set; }
            public string age { get; set; }
            public string gender { get; set; }

            public User(string _id, string _name, string _dateOfBirth, string _age, string _gender)
            {
                this.id = _id;
                this.name = _name;
                this.dateOfBirth = _dateOfBirth;
                this.age = _age;
                this.gender = _gender;
            }
        }

        public static List<User> user = new List<User>();

        public MainWindow()
        {
            InitializeComponent();
            LoadUser();
        }

        public void LoadUser(List<User> _user = null)
        {
            userList.Items.Clear();
            if (_user == null)
            {
                user.Clear();
                string query = "SELECT * FROM mydb.people";
                var users = DBConnect.Connection(query);
                while (users.Read())
                {
                    User usr = new User(users.GetValue(0).ToString(), users.GetValue(1).ToString(), users.GetValue(2).ToString(), users.GetValue(3).ToString(), users.GetValue(4).ToString());
                    user.Add(usr);
                    userList.Items.Add(usr);
                }
            }
            else
            {
                foreach(User usr in _user)
                {
                    userList.Items.Add(usr);
                }
            }


        }
        

        private void ActiveFilter(object sender, RoutedEventArgs e)
        {
            List<User> newUsers = new List<User>();

            if(genderFilter.SelectedIndex == 0)
            {               
                newUsers = user.FindAll(x => x.gender == "M");
            }
            else if(genderFilter.SelectedIndex == 1)
            {
                newUsers = user.FindAll(x => x.gender == "F");
            }

            if (newUsers.Count != 0)
            {
                newUsers = newUsers.FindAll(x => x.name.Contains(nameFilter.Text));
            }
            else
            {
                newUsers = user.FindAll(x => x.name.Contains(nameFilter.Text));
            }

            if (monthFilter.SelectedItem != null)
            {
                TextBlock selectedItem = monthFilter.SelectedItem as TextBlock;
                var month = selectedItem.Text.Split(':')[1];

                if (newUsers.Count != 0)
                {
                    newUsers = newUsers.FindAll(x => x.dateOfBirth[3] == month[0] && x.dateOfBirth[4] == month[1]);
                }
                else
                {
                    newUsers = user.FindAll(x => x.dateOfBirth[3] == month[0] && x.dateOfBirth[4] == month[1]);
                }
            }

            LoadUser(newUsers);
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            new AddAndUpdate().ShowDialog();
            LoadUser();
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            new AddAndUpdate(userList.SelectedItem as User).ShowDialog();
            LoadUser();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (userList.SelectedItem != null)
            {
                var usr = userList.SelectedItem as User;

                string query = String.Format("delete from mydb.people where id = {0}", usr.id);
                DBConnect.Connection(query);
                LoadUser();
            }
        }
    }
}
