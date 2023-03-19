using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Pr17.Pages
{
    /// <summary>
    /// Логика взаимодействия для Products.xaml
    /// </summary>
    public partial class Products : Page
    {

        public MainWindow mainWindow;
        GridViewColumnHeader lastHeaderClicked = null;
        ListSortDirection lastDirection = ListSortDirection.Ascending;

        public Products(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            LoadData();
        }

        public void LoadData()
        {
            listView.Items.Clear();
            for (int i = 0; i < mainWindow.items.Count; i++)
            {
                listView.Items.Add(mainWindow.items[i]);
            }
        }

        private void OpenSpecifications(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            MessageBox.Show(Convert.ToString(button.Tag));
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            var itm = listView.SelectedItem as MainWindow.Items;
            string query = String.Format("delete from mydb.electroshop where id = {0}", itm.id);
            DbConnect.Connection(query);
            mainWindow.LoadData();
            LoadData();
        }

        void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        {
            if (!(e.OriginalSource is GridViewColumnHeader sortBy)) return;
            var sortDirection = ListSortDirection.Ascending;
            if (sortBy == lastHeaderClicked && lastDirection == ListSortDirection.Ascending)
                sortDirection = ListSortDirection.Descending;
            Sort(sortBy, sortDirection);
            lastHeaderClicked = sortBy; lastDirection = sortDirection;
        }

        private void Sort(GridViewColumnHeader sortBy, ListSortDirection direction)
        {
            var headerToSort = (sortBy.Column.DisplayMemberBinding as Binding)?.Path.Path;
            headerToSort = headerToSort ?? sortBy.Column.Header as string;
            var defaultValueListView = CollectionViewSource.GetDefaultView(listView.Items);
            defaultValueListView.SortDescriptions.Clear();
            var sortDescription = new SortDescription(headerToSort, direction);
            defaultValueListView.SortDescriptions.Add(sortDescription);
            defaultValueListView.Refresh();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            new Windows.AddAndChange().ShowDialog();
            mainWindow.LoadData();
            LoadData();
        }

        private void Change(object sender, RoutedEventArgs e)
        {
            var itm = listView.SelectedItem as MainWindow.Items;
            new Windows.AddAndChange(itm.id, itm.name, itm.color, itm.max_memory, itm.diagonal, Convert.ToInt32(itm.resolution)).ShowDialog();
            mainWindow.LoadData();
            LoadData();

        }
    }
}
