using System;
using System.Linq;
using System.Windows;

namespace MiCasaUI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ChooseCategoryWindow : Window
    {
        public ChooseCategoryWindow()
        {
            InitializeComponent();

            categoryComboBox.ItemsSource = Enum.GetValues(typeof(IngredientCategory)).Cast<IngredientCategory>();
        }
    }
}
