using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
        private BackendFacilities backendFacilities;

        private struct IngredientInRecepyView
        {
            public string name { get; set; }
            public float amount { get; set; }
            public Unit unit { get; set; }
        }

        private ObservableCollection<IngredientInRecepyView> ingredients = new ObservableCollection<IngredientInRecepyView>();

        public AddRecepyWindow()
        {
            backendFacilities = BackendFacilitiesRef.Get();

            InitializeComponent();

            addIngredientButton.Click += OnAddIngredientButtonClicked;
            removeIngredientButton.Click += OnRemoveIngredientButtonClicked;
            okButton.Click += OnOkButtonClicked;
            cancelButton.Click += OnCancelButtonClicked;

            ingredientsListView.ItemsSource = ingredients;
            newIngredientUnitComboBox.ItemsSource = Enum.GetValues(typeof(Unit)).Cast<Unit>();
        }

        private void OnAddIngredientButtonClicked(object sender, RoutedEventArgs e)
        {
            if (newIngredientNameTextBox.Text == "")
                return;
            IngredientInRecepyView ingredient = new IngredientInRecepyView() { name = newIngredientNameTextBox.Text, amount = float.Parse(newIngredientAmountTextBox.Text), unit = (Unit)newIngredientUnitComboBox.SelectedItem}; //TODO: parse unit
            Ingredient newIngredient = backendFacilities.GetIngredient(ingredient.name);
            if(newIngredient == null)
            {
                ChooseCategoryWindow ccw = new ChooseCategoryWindow();
                ccw.Show();
                this.Hide();
                ccw.okButton.Click += new RoutedEventHandler(delegate (object o, RoutedEventArgs a)
                {
                    newIngredient = new Ingredient();
                    newIngredient.category = (IngredientCategory) ccw.categoryComboBox.SelectedItem;
                    newIngredient.name = ingredient.name;

                    backendFacilities.AddIngredient(newIngredient);
                    this.Show();
                    ccw.Close();
                });

            }
            ingredients.Add(ingredient);

            newIngredientNameTextBox.Text = "";
            newIngredientAmountTextBox.Text = "";
            newIngredientUnitComboBox.SelectedItem = null;            
        }

        private void OnRemoveIngredientButtonClicked(object sender, RoutedEventArgs e)
        {
            ingredients.RemoveAt(ingredientsListView.SelectedIndex);
        }

        private void OnOkButtonClicked(object sender, RoutedEventArgs e)
        {
            Recepy recepy = new Recepy();
            recepy.description = recepyDescriptionTextBox.Text;
            recepy.name = recepyNameTextBox.Text;
            foreach(var ingredient in ingredients)
            {
                Ingredient i = backendFacilities.GetIngredient(ingredient.name);
                IngredientInRecepy iir = new IngredientInRecepy();
                iir.amount = ingredient.amount;
                iir.ingredientId = i != null ? i.id : -1;
                iir.unit = ingredient.unit;
                recepy.ingredients.Add(iir);                    
            }
            backendFacilities.AddRecepy(recepy);
        }

        private void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /*public class StarWidthConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                ListView listview = value as ListView;
                double width = listview.Width;
                GridView gv = listview.View as GridView;
                for (int i = 0; i < gv.Columns.Count; i++)
                {
                    if (!Double.IsNaN(gv.Columns[i].Width))
                        width -= gv.Columns[i].Width;
                }
                return width - 5;// this is to take care of margin/padding
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return null;
            }
        }*/
    }
}
