namespace ConsoleCafe.ConsoleUI.Options
{
    public class CurrencyOptions
    {
        public CurrencyOptions(string symbol = "$")
        {
            Symbol = symbol;
        }

        public string Symbol { get; }
    }
}