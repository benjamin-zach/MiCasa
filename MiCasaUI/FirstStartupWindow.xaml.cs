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

namespace MiCasaUI
{
    /// <summary>
    /// Interaction logic for FirstStartup.xaml
    /// </summary>
    public partial class FirstStartupWindow : Window
    {
        private enum State
        {
            CREATE,
            OPEN
        }

        State state;

        public FirstStartupWindow()
        {
            InitializeComponent();

            openDBButton.Click += OnOpenDBButtonClicked;
            createDBButton.Click += OnCreateDBButtonClicked;
            okButton.Click += OnOkButtonClicked;
        }

        private void OnNewButtonClicked(object sender, RoutedEventArgs e)
        {
            //TODO: create db #zach
        }

        private void OnOpenButtonClicked(object sender, RoutedEventArgs e)
        {
            //backendFacilities.dataBaseFilePath = dataBaseFilePathTextBox.Text;
            Properties.Settings.Default.FirstStartup = false;
            Properties.Settings.Default.Save();
        }

        private void OnOpenDBButtonClicked(object sender, RoutedEventArgs e)
        {
            openDBButton.Background = Brushes.ForestGreen;
            createDBButton.Background = Brushes.Gray;

            openDBFileGrid.Visibility = Visibility.Visible;
            createDBFileGrid.Visibility = Visibility.Collapsed;
            okButton.Visibility = Visibility.Visible;
            state = State.OPEN;
        }

        private void OnCreateDBButtonClicked(object sender, RoutedEventArgs e)
        {
            createDBButton.Background = Brushes.ForestGreen;
            openDBButton.Background = Brushes.Gray;

            createDBFileGrid.Visibility = Visibility.Visible;
            openDBFileGrid.Visibility = Visibility.Collapsed;
            okButton.Visibility = Visibility.Visible;
            state = State.CREATE;
        }

        private void OnOkButtonClicked(object sender, RoutedEventArgs e)
        {
            LocalBackendFacilities backendFacilities = (LocalBackendFacilities)BackendFacilitiesRef.Get();
            if (state == State.OPEN)
            {
                backendFacilities.dataBaseFilePath = openDBFileTextBox.Text;
            }
            else if(state == State.CREATE)
            {

            }
            Close();
            MainWindow.instance.Show();
        }
    }
}
