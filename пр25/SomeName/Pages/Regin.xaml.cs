using MySql.Data.MySqlClient;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SomeName.Pages
{
    /// <summary>
    /// Логика взаимодействия для Regin.xaml
    /// </summary>
    public partial class Regin : Page
    {
        MainWindow mainWindow;
        string adminAdd;
        string accEditId;
        public Regin(MainWindow mainWindow, string adminAdd = "0", string accEditId = "0")
        {
            this.mainWindow = mainWindow;
            this.adminAdd = adminAdd;
            this.accEditId = accEditId;
            InitializeComponent();
            if (adminAdd == "1")
            {
                password_tb_2.Visibility = Visibility.Hidden;
                password_tb_2_label.Visibility = Visibility.Hidden;
                register.Content = "Добавить";
                registerLabel.Content = "Добавить";
                    
            }
            else if (accEditId != "0")
            {
                register.Content = "Изменить";
                registerLabel.Content = "Изменить";
                string query = String.Format("SELECT * FROM `mydb`.`users` WHERE `index` = '{0}'",accEditId);
                var user = DBConnect.Select(query);
                user.Read();
                login_tb.Text = user.GetValue(1).ToString();
                login_tb.IsReadOnly = true;
                password_tb.Text = user.GetValue(2).ToString();
                
            }
        }

        private void UserRegin(object sender, RoutedEventArgs e)
        {
            if(login_tb.Text.Length > 0)
            {
                if(password_tb.Text.Length > 0)
                {
                    if(password_tb_2.Text.Length > 0 || adminAdd == "1")
                    {
                        if(password_tb.Text == password_tb_2.Text || adminAdd == "1")
                        {
                            if (accEditId != "0")
                            {
                                string query = String.Format("UPDATE `mydb`.`users` SET `password` = '{0}' WHERE `index` = '{1}'", password_tb.Text, accEditId);
                                DBConnect.Select(query);
                                mainWindow.frame.Navigate(new AdminPage(mainWindow));
                            }
                            else
                            {
                                MySqlDataReader user = DBConnect.Select(String.Format("SELECT * FROM `mydb`.`users` WHERE `login`='{0}'", login_tb.Text));
                                if (!user.HasRows)
                                {
                                    DBConnect.Select(String.Format("INSERT INTO `mydb`.`users`(`login`,`password`,`roll`)" +
                                        " VALUES ('{0}','{1}','{2}')", login_tb.Text, password_tb.Text, "2"));
                                    MessageBox.Show("Пользователь успешно зарегистрирован", "Уведомление");
                                    if (adminAdd == "1")
                                    {
                                        mainWindow.frame.Navigate(new AdminPage(mainWindow));
                                    }
                                    else
                                    {
                                        mainWindow.OpenPage(MainWindow.pages.login);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Логин занят", "Уведомление");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пароли не совпадают", "Уведомление");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Повторите пароль", "Уведомление");
                    }
                }
                else
                {
                    MessageBox.Show("Укажите пароль", "Уведомление");
                }
            }
            else
            {
                MessageBox.Show("Укажите логин", "Уведомление");
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            if(adminAdd == "1" || accEditId != "0")
            {
                mainWindow.frame.Navigate(new AdminPage(mainWindow));
            }
            else
            {
                mainWindow.OpenPage(MainWindow.pages.login);
            }           
        }
    }
}
