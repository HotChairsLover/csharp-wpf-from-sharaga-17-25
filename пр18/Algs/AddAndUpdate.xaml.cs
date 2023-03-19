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
using System.Windows.Shapes;

namespace Algs
{
    /// <summary>
    /// Логика взаимодействия для AddAndUpdate.xaml
    /// </summary>
    public partial class AddAndUpdate : Window
    {
        MainWindow.User user = null;
        public AddAndUpdate(MainWindow.User _user = null)
        {
            InitializeComponent();
            if(_user != null)
            {
                user = _user;
                Title = "Изменить";
                fullName.Text = _user.name;
                age.Text = _user.age;
                birthDate.Text = _user.dateOfBirth;
                gender.Text = _user.gender;
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            bool error = false;
            try
            {
                var age = Convert.ToInt32(this.age.Text);
            }
            catch
            {
                error = true;
                MessageBox.Show("Возраст должен быть цифрой");              
            }
            try
            {
                var birthday = Convert.ToDateTime(birthDate.Text);
            }
            catch
            {
                error = true;
                MessageBox.Show("Дата рождения должна быть записана в формате ДД.ММ.ГГГГ");
            }
            if (!error)
            {
                if (user == null)
                {
                    string query = String.Format("insert into `mydb`.`people` (`FullName`, `BirthDate`, `Age`, `Gender`) values ('{0}','{1}','{2}','{3}')",
                        fullName.Text, birthDate.Text, age.Text, gender.Text);
                    DBConnect.Connection(query);
                    Close();
                }
                else
                {
                    string query = String.Format("update mydb.people set `FullName` = '{0}', `BirthDate` = '{1}', `Age` = '{2}', `Gender` = '{3}' where id = {4}",
                        fullName.Text, birthDate.Text, age.Text, gender.Text, user.id);
                    DBConnect.Connection(query);
                    Close();
                }
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
