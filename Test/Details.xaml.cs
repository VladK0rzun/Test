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
        public Details(Currency selectedCurrency)
        {
            InitializeComponent();
            DisplayCurrencyDetails(selectedCurrency);
        }
      

        private void DisplayCurrencyDetails(Currency currency)
        {
            // Оновіть елементи на сторінці деталей з використанням властивостей валюти
            nameLabel.Content = "Name: " + currency.Name;
            priceLabel.Content = "Price: " + currency.current_price.ToString("C2"); // Припустимо, що ціна - це double і ви хочете відобразити її як грошове значення
            marketCapLabel.Content = "Market Cap: " + currency.market_cap.ToString("C0"); // Аналогічно тут
                                                                                          // Додайте інші властивості валюти, які ви хочете відобразити
        }
    }

}
