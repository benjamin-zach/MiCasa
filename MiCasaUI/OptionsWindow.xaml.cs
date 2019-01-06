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
using System.Windows.Shapes;

namespace MiCasaUI
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        public OptionsWindow()
        {
            InitializeComponent();

            okButton.Click += OnOkButtonClicked;
            cancelButton.Click += OnCancelButtonClicked;
        }

        private void OnOkButtonClicked(object sender, RoutedEventArgs e)
        {
            LocalBackendFacilities backendFacilities = (LocalBackendFacilities) BackendFacilitiesRef.Get();
            backendFacilities.dataBaseFilePath = localDataBaseFilePathTextBox.Text;
            Close();
        }

        private void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
