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
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        class AuthToListView
        {
            public int index { get; set; }
            public string date { get; set; }
            public string time { get; set; }
            public string device { get; set; }
        }
        string accountId;
        MainWindow mainWindow;
        public UserPage(string accountId, MainWindow mainWindow)
        {
            InitializeComponent();
            this.accountId = accountId;
            this.mainWindow = mainWindow;
            LoadAuths();
        }
        void LoadAuths()
        {
            string query = String.Format("SELECT * FROM `mydb`.`auths` WHERE `account` = '{0}'", accountId);
            var auths = DBConnect.Select(query);
            while(auths.Read())
            {
                AuthToListView auth = new AuthToListView();
                auth.index = Convert.ToInt32(auths.GetValue(0));
                auth.date = auths.GetValue(1).ToString().Split(' ')[0];
                auth.time = auths.GetValue(2).ToString().Split('.')[0];
                auth.device = auths.GetValue(3).ToString();
                authListView.Items.Add(auth);
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            mainWindow.frame.Navigate(new Login(mainWindow));
        }
    }
}
