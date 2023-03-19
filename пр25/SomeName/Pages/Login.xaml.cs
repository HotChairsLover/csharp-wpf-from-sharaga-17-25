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

namespace SomeName.Pages
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        MainWindow mainWindow;
        public Login(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();
        }

        private void UserLogin(object sender, RoutedEventArgs e)
        {
            if(login_tb.Text.Length > 0)
            {
                if(password_tb.Text.Length > 0)
                {
                    MySqlDataReader user = DBConnect.Select(String.Format("SELECT * FROM `users` WHERE login='{0}' AND password='{1}'",login_tb.Text, password_tb.Text));
                    if(user.HasRows)
                    {
                        user.Read();
                        if(Convert.ToInt32(user.GetValue(3)) == 1)
                        {
                            mainWindow.frame.Navigate(new AdminPage(mainWindow));
                        }
                        else
                        {
                            string date = DateTime.Now.Date.ToString();
                            string time = DateTime.Now.TimeOfDay.ToString();
                            string device = String.Format("{0}, {1}",Environment.UserName, Environment.MachineName);
                            string query = String.Format("INSERT INTO `mydb`.`auths`(`date`,`time`,`device`,`account`) " +
                                "VALUES('{0}','{1}','{2}','{3}')", date, time, device, user.GetValue(0));
                            DBConnect.Select(query);
                            mainWindow.frame.Navigate(new UserPage(user.GetValue(0).ToString(), mainWindow));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пароль или логин введены не верно", "Уведомление");
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, укажите пароль пользователя", "Уведомление");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, укажите логин пользователя", "Уведомление");
            }
        }

        private void UserRegin(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.regin);
        }
    }
}
