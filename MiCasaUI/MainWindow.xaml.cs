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

namespace MiCasaUI
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            addRecepyButton.Click += OnAddRecepyButtonClicked;
            removeRecepyButton.Click += OnRemoveRecepyButtonClicked;
            refreshRecepiesButton.Click += OnRefreshRecepiesButtonClicked;
        }

        private void OnSetDataBaseMenuItemClicked(object sender, RoutedEventArgs e)
        {
            OptionsWindow optionsWindow = new OptionsWindow();
            optionsWindow.Show();
        }

        private void OnAddRecepyButtonClicked(object sender, RoutedEventArgs e)
        {
            AddRecepyWindow addRecepyWindow = new AddRecepyWindow();
            addRecepyWindow.Show();
        }

        private void OnRemoveRecepyButtonClicked(object sender, RoutedEventArgs e)
        {

        }

        private void OnRefreshRecepiesButtonClicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
