namespace ConsoleCafe.ConsoleUI.Options
{
    public class CurrencyOptions
    {
        public string Symbol { get; }

        public CurrencyOptions(string symbol = "$")
        {
            Symbol = symbol;
        }
    }
}
