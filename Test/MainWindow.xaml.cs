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

namespace Test
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Page
    {
        private CoinGeckoService _coinGeckoService = new CoinGeckoService();

        public MainWindow()
        {
            InitializeComponent();
            currencyListView.SelectionChanged += CurrencyListView_SelectionChanged;
            LoadData();
        }

        private async void LoadData()
        {
            var currencies = await _coinGeckoService.GetTopCurrenciesAsync(10);
            if (currencies != null)
            {
                currencyListView.ItemsSource = currencies;
            }
            else
            {
                MessageBox.Show("Failed to fetch data from CoinGecko API.");
            }
        }

        private void CurrencyListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (currencyListView.SelectedItem != null && currencyListView.SelectedItem is Currency selectedCurrency)
            {
                // Create a new Details page and pass the selected currency as a parameter
                Details detailPage = new Details(selectedCurrency);

                // Navigate to the Details page in the Frame
                NavigationService.Navigate(detailPage);
            }
        }
    }
}
