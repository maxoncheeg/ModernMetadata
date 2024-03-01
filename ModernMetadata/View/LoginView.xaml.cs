﻿using ModernMetadata.Model.Metadata;
using ModernMetadata.Model.Metadata.Readers;
using ModernMetadata.Model.Metadata.Users;
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

namespace ModernMetadata.View
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void OnLoginClicked(object sender, RoutedEventArgs e)
        {
            MenuView view = new MenuView();
            view.ShowDialog();
        }
    }
}
