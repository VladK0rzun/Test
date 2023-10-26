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
using System.Xml;

namespace Test
{
    /// <summary>
    /// Логика взаимодействия для Details.xaml
    /// </summary>
    public partial class Details : Page 
    {
        private CoinGeckoService _coinGeckoService = new CoinGeckoService();
        private Currency selectedCurrency;
        public Details(Currency selectedCurrency)
        {
            this.selectedCurrency = selectedCurrency;
            InitializeComponent();
            DataContext = this;
            LoadData();
            DisplayCurrencyDetails(selectedCurrency);

        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService != null && NavigationService.CanGoBack)
            {
                NavigationService.Navigate(new Uri("MainWindow.xaml", UriKind.Relative));
            }
        }

        private void DisplayCurrencyDetails(Currency currency)
        {

            nameLabel.Content = $"Name: {selectedCurrency.Name}";
            priceLabel.Content = $"Price: {selectedCurrency.current_price.ToString("C2")}";
            marketCapLabel.Content = $"Market Cap: {selectedCurrency.market_cap.ToString("C0")}";

          
        }
        private async void LoadData()
        {
            var currencies = await _coinGeckoService.GetCurrencyMarketsAsync(selectedCurrency.Name);
            if (currencies != null)
            {
                marketsListView.ItemsSource = currencies;
            }
            else
            {
                MessageBox.Show("Failed to fetch data from CoinGecko API.");
            }
        }
    }

}
