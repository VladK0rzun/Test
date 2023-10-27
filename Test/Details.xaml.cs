using OxyPlot.Axes;
using OxyPlot;
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
using LiveCharts;
using LiveCharts.Wpf;

namespace Test
{
    /// <summary>
    /// Логика взаимодействия для Details.xaml
    /// </summary>
    public partial class Details : Page
    {
        private CoinGeckoService _coinGeckoService = new CoinGeckoService();
        private Currency selectedCurrency;
        public SeriesCollection YourSeriesCollection { get; set; }

        public Details(Currency selectedCurrency)
        {
            this.selectedCurrency = selectedCurrency;
            InitializeComponent();
            YourSeriesCollection = new SeriesCollection();
            DataContext = this;
            daysComboBox.SelectionChanged += DaysComboBox_SelectionChanged;
            LoadData();
            DisplayCurrencyDetails(selectedCurrency);
            UpdateChart();
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
        private async void UpdateChart()
        {
            if (daysComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                if (int.TryParse(selectedItem.Tag.ToString(), out int numberOfDays))
                {
                    var historicalPrices = await _coinGeckoService.GetHistoricalPricesAsync(selectedCurrency.Name, numberOfDays);

                    if (historicalPrices != null)
                    {
                        YourSeriesCollection.Clear();
                        YourSeriesCollection.Add(new LineSeries
                        {
                            Title = "Price",
                            Values = new ChartValues<double>(historicalPrices.Select(price => price.Item2))
                        });
                    }
                    else
                    {
                        MessageBox.Show("Failed to fetch historical price data from CoinGecko API.");
                    }
                }
            }
        }
        private void DaysComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateChart();
        }

    }

}
