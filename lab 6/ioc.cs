namespace ioc
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
    public class BankClient
    {
        private readonly IBalanceChecker balanceChecker;

        public BankClient(IBalanceChecker balanceChecker) =>
            this.balanceChecker = balanceChecker;

        public bool CheckBalance(string accountNumber)
        {
            decimal balance = balanceChecker.GetBalance(accountNumber);

            if (balance > 0)
            {
                Console.WriteLine("Баланс положительный.");
                // Выполнение дополнительных действий для положительного баланса
                return true;
            }
            else
            {
                Console.WriteLine("Баланс отрицательный или равен нулю.");
                // Выполнение дополнительных действий для отрицательного или нулевого баланса
                return false;
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            IBalanceChecker balanceChecker = new ExternalBalanceChecker();
            BankClient bankClient = new BankClient(balanceChecker);

            // Выполнение проверки баланса для определенного счета
            string accountNumber = "123456789";
            bool isBalancePositive = bankClient.CheckBalance(accountNumber);

            // Дальнейшие действия в зависимости от результата проверки
            if (isBalancePositive)
            {
                // Выполнение действий для положительного баланса
            }
            else
            {
                // Выполнение действий для отрицательного или нулевого баланса
            }
        }
    }
}