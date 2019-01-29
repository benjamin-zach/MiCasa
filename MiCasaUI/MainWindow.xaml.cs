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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MiCasaUI
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow instance;
        private BackendFacilities backendFacilities;
        private ObservableCollection<Recepy> recepies = new ObservableCollection<Recepy>();
        public ObservableCollection<Meal> meals = new ObservableCollection<Meal>();

        public MainWindow()
        {
            instance = this;
            InitializeComponent();

            if(Properties.Settings.Default.FirstStartup)
            {
                FirstStartupWindow firstStartupWindow = new FirstStartupWindow();
                Hide();
                firstStartupWindow.Show();
            }

            OnRefreshRecepiesButtonClicked(null, null);
            recepiesListView.ItemsSource = recepies;
            mealsListView.ItemsSource = meals;

            addRecepyButton.Click += OnAddRecepyButtonClicked;
            removeRecepyButton.Click += OnRemoveRecepyButtonClicked;
            refreshRecepiesButton.Click += OnRefreshRecepiesButtonClicked;

            addMealButton.Click += OnAddMealButtonClicked;
            removeMealButton.Click += OnRemoveMealButtonClicked;
            refreshMealsButton.Click += OnRefreshMealsButtonClicked;
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
            addRecepyWindow.okButton.Click += new RoutedEventHandler(delegate (object o, RoutedEventArgs args)
            {
                CollectionViewSource.GetDefaultView(recepies).Refresh();
                addRecepyWindow.Close();
            });

        }

        private void OnAddMealButtonClicked(object sender, RoutedEventArgs e)
        {
            AddCalendarMealWindow addCalendarMealWindow = new AddCalendarMealWindow();
            addCalendarMealWindow.recepyComboBox.ItemsSource = recepies;
            addCalendarMealWindow.okButton.Click += new RoutedEventHandler(delegate (object o, RoutedEventArgs a)
            {
                Meal meal = new Meal();
                meal.date = (DateTime)addCalendarMealWindow.datePicker.SelectedDate;
                meal.type = (MealType)addCalendarMealWindow.mealComboBox.SelectedItem;
                meal.recepy = (Recepy)addCalendarMealWindow.recepyComboBox.SelectedItem;
                meals.Add(meal);
                CollectionViewSource.GetDefaultView(meals).Refresh();
                addCalendarMealWindow.Close();
            });
            addCalendarMealWindow.cancelButton.Click += new RoutedEventHandler(delegate (object o, RoutedEventArgs a)
            {
                addCalendarMealWindow.Close();
            });
            addCalendarMealWindow.Show();
        }

        private void OnRemoveRecepyButtonClicked(object sender, RoutedEventArgs e)
        {
            recepies.Remove((Recepy)recepiesListView.SelectedItem);
            CollectionViewSource.GetDefaultView(recepies).Refresh();
        }

        private void OnRemoveMealButtonClicked(object sender, RoutedEventArgs e)
        {
            meals.Remove((Meal)mealsListView.SelectedItem);
            CollectionViewSource.GetDefaultView(meals).Refresh();
        }

        private void OnRefreshRecepiesButtonClicked(object sender, RoutedEventArgs e)
        {
            backendFacilities = BackendFacilitiesRef.Get();
            recepies = new ObservableCollection<Recepy>(backendFacilities.GetRecepies());
        }

        private void OnRefreshMealsButtonClicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
