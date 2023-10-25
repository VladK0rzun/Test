namespace Test
{
    public class Currency
    {
        public string Name { get; set; }
        public double current_price { get; set; }
        public double market_cap { get; set; }
        public double total_volume { get; set; }
        public double price_change_24h { get; set; }
        public string symbol { get; set; }
        public CurrencyMarket[] CurrencyMarkets { get; set; }

    }

    public class CurrencyMarket
    {
        public string Name { get; set; }
        public double last { get; set; }
        public CurrencyMarket()
        {
        }

        public CurrencyMarket(string name, double last)
        {
            Name = name;
            this.last = last;
        }
    }
}