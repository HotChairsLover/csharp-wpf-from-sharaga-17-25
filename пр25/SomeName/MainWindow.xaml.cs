﻿using System;
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

namespace SomeName
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum pages
        {
            login, regin
        }
        public MainWindow()
        {
            InitializeComponent();
            OpenPage(pages.login);
        }

        public void OpenPage(pages _pages)
        {
            if(_pages == pages.login)
            {
                frame.Navigate(new Pages.Login(this));
            }
            else if(_pages == pages.regin)
            {
                frame.Navigate(new Pages.Regin(this));
            }
        }
    }
}
