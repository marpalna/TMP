namespace proxy
{
	public interface IBalanceChecker
    {
        decimal GetBalance(string accountNumber);
    }
	public class ExternalBalanceChecker : IBalanceChecker
    {
        public decimal GetBalance(string accountNumber)
        {
            // Взаимодействие с внешним сервисом для получения баланса
            // В данном примере возвращается случайное значение
            Random random = new Random();
            return (decimal)random.NextDouble() * 10000;
        }
    }
    public class BalanceCheckerProxy : IBalanceChecker
    {
        private readonly IBalanceChecker balanceChecker;
        private Dictionary<string, decimal> balanceCache;

        public BalanceCheckerProxy(IBalanceChecker balanceChecker)
        {
            this.balanceChecker = balanceChecker;
            balanceCache = new Dictionary<string, decimal>();
        }

        public decimal GetBalance(string accountNumber)
        {
            // Проверка наличия кэша для баланса по заданному счету
            if (balanceCache.ContainsKey(accountNumber))
            {
                Console.WriteLine("Баланс получен из кэша.");
                return balanceCache[accountNumber];
            }
            else
            {
                decimal balance = balanceChecker.GetBalance(accountNumber);

                // Добавление баланса в кэш
                balanceCache.Add(accountNumber, balance);

                Console.WriteLine("Баланс получен из внешнего сервиса и добавлен в кэш.");
                return balance;
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
			// Создание экземпляра класса заместителя с передачей реального объекта в конструктор
			IBalanceChecker balanceChecker = new ExternalBalanceChecker();
			IBalanceChecker proxy = new BalanceCheckerProxy(balanceChecker);

			// Выполнение проверки баланса для определенного счета
			string accountNumber = "123456789";
			decimal balance = proxy.GetBalance(accountNumber);

			Console.WriteLine("Баланс на счете: " + balance);
		}
	}
}