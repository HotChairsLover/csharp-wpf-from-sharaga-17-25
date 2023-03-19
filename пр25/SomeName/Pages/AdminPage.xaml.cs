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

namespace SomeName.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        class User
        {
            public int index { get; set; }
            public string login { get; set; }
            public string password { get; set; }
            public int roll { get; set; }
        }
        MainWindow mainWindow;
        public AdminPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            LoadUsers();
        }
        void LoadUsers()
        {
            usersListView.Items.Clear();
            string query = "SELECT * FROM `mydb`.`users`";
            var users = DBConnect.Select(query);
            while(users.Read())
            {
                User user = new User();
                user.index = Convert.ToInt32(users.GetValue(0));
                user.login = users.GetValue(1).ToString();
                user.password = users.GetValue(2).ToString();
                user.roll = Convert.ToInt32(users.GetValue(3));
                usersListView.Items.Add(user);
            }
        }

        private void AddUser(object sender, RoutedEventArgs e)
        {
            mainWindow.frame.Navigate(new Regin(mainWindow, "1"));
        }

        private void EditUser(object sender, RoutedEventArgs e)
        {
            if (usersListView.SelectedItem != null)
            {
                User user = usersListView.SelectedItem as User;
                mainWindow.frame.Navigate(new Regin(mainWindow, accEditId: user.index.ToString()));
            }
            else
            {
                MessageBox.Show("Выберите пользователя для изменения");
            }
        }

        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            if(usersListView.SelectedItem != null)
            {
                User user = usersListView.SelectedItem as User;
                string query = String.Format("DELETE FROM `mydb`.`users` WHERE `index` = '{0}'", user.index);
                DBConnect.Select(query);
                LoadUsers();
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            mainWindow.frame.Navigate(new Login(mainWindow));
        }
    }
}
