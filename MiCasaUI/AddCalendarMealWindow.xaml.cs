using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AddCalendarMealWindow.xaml
    /// </summary>
    public partial class AddCalendarMealWindow : Window
    {
        public AddCalendarMealWindow()
        {
            InitializeComponent();

            mealComboBox.ItemsSource = Enum.GetValues(typeof(MealType)).Cast<MealType>();
            datePicker.SelectedDate = DateTime.Now;
        }
    }
}
