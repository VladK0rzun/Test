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

namespace Test
{
    /// <summary>
    /// Логика взаимодействия для ConvertPage.xaml
    /// </summary>
    public partial class ConvertPage : Page
    {
        private CoinGeckoService _coinGeckoService = new CoinGeckoService();
        public ConvertPage()
        {
            InitializeComponent();

            // Populate ComboBoxes with currencies
            fromCurrencyComboBox.ItemsSource = fromCurrency; // Populate this with your currency data
            toCurrencyComboBox.ItemsSource = toCurrency; // Populate this with your currency data
        }


        private async void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(amountTextBox.Text, out double amount))
            {
                if (fromCurrencyComboBox.SelectedItem != null && toCurrencyComboBox.SelectedItem != null)
                {
                    string fromCurrency = fromCurrencyComboBox.SelectedItem.ToString();
                    string toCurrency = toCurrencyComboBox.SelectedItem.ToString();

                    double convertedAmount = await PerformCurrencyConversion(amount, fromCurrency, toCurrency);
                    resultLabel.Content = $"{amount} {fromCurrency} is equal to {convertedAmount} {toCurrency}.";
                }
                else
                {
                    MessageBox.Show("Please select currencies for conversion.");
                }
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter a valid numeric amount.");
            }
        }

        // Implement your currency conversion logic here
        private async Task<double> PerformCurrencyConversion(double amount, string fromCurrency, string toCurrency)
        {
            double exchangeRate = await _coinGeckoService.GetExchangeRateAsync(fromCurrency, toCurrency);
            double convertedAmount = amount * exchangeRate;
            return convertedAmount;
        }

        // Your list of available currencies
        private string[] fromCurrency = { "bitcoin","01coin", "0chain", "0-knowledge-network", "0-mee", "0vix-protocol", "0x", "0x0-ai-ai-smart-contract", "0x1-tools-ai-multi-tool", 
            "0xauto-io", "0xcoco", "0xdao", "0xdefcafe", "0xfreelance", "0xfriend", "0xgasless", "0x-leverage", "0xlsd", "0xmonero", "0xs", "0xshield", "0xsniper", "12ships",
            "1art", "1eco", "1hive-water", "1inch", "1inch-yvault", "1million-nfts", "1minbet", "1move token", "1peco", "1reward-token", "1safu", "1sol", "1sol-io-wormhole", 
            "-2", "2049", "2080", "20weth-80bal", "28", "28vck", "2acoin", "2dai-io", "2g-carbon-coin", "2moon", "2omb-finance", "2share", "300fit", "3d3d", "3dpass", 
            "3-kingdoms-multiverse", "3shares", "3xcalibur" };
        private string[] toCurrency = { "BTC", "ETH", "LTC", "BCH", "BNB", "EOS", "XRP", "XLM", "LINK", "DOT", "YFI", "USD", "AED", 
            "ARS", "AUD", "BDT", "BHD", "BMD", "BRL", "CAD", "CHF", "CLP", "CNY", "CZK", "DKK", "EUR", "GBP", "HKD", "HUF", "IDR", 
            "ILS", "INR", "JPY", "KRW", "KWD", "LKR", "MXN", "MYR", "NGN", "NOK", "NZD", "PHP", "PKR", "PLN", "RUB", "SAR", "SEK", 
            "SGD", "THB", "TRY", "TWD", "UAH", "VEF", "VND", "ZAR", "XDR", "XAG", "XAU", "BITS", "SATS" };
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                contentFrame.Navigate(new Uri("MainWindow.xaml", UriKind.Relative));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Navigation Error: {ex.Message}");
            }
        }
    }
}

