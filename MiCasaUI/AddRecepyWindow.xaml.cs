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
    /// Interaktionslogik für AddRecepyWindow.xaml
    /// </summary>
    public partial class AddRecepyWindow : Window
    {
        public AddRecepyWindow()
        {
            InitializeComponent();

            addIngredientButton.Click += OnAddIngredientButtonClicked;
            removeIngredientButton.Click += OnRemoveIngredientButtonClicked;
            okButton.Click += OnOkButtonClicked;
            cancelButton.Click += OnCancelButtonClicked;
        }

        private void OnAddIngredientButtonClicked(object sender, RoutedEventArgs e)
        {

        }

        private void OnRemoveIngredientButtonClicked(object sender, RoutedEventArgs e)
        {

        }

        private void OnOkButtonClicked(object sender, RoutedEventArgs e)
        {

        }

        private void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
