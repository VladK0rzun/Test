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
        private List<Currency> originalCurrencies;
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
                originalCurrencies = currencies.ToList();
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

                Details detailPage = new Details(selectedCurrency);


                NavigationService.Navigate(detailPage);
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = searchTextBox.Text.ToLower();
            List<Currency> filteredCurrencies = new List<Currency>();

            if (string.IsNullOrEmpty(searchText))
            {
                // Если строка поиска пуста, восстанавливаем оригинальный список
                filteredCurrencies = originalCurrencies;
            }
            else
            {
                // Фильтруем валюты на основе введенного текста
                foreach (Currency currency in originalCurrencies)
                {
                    if (currency.Name.ToLower().Contains(searchText) || currency.symbol.ToLower().Contains(searchText))
                    {
                        filteredCurrencies.Add(currency);
                    }
                }
            }

            // Обновляем источник данных для currencyListView
            currencyListView.ItemsSource = filteredCurrencies;
        }
    }
}
