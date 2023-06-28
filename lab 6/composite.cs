namespace composite
{
	public abstract class Account
    {
        protected string accountNumber;

        public Account(string accountNumber) =>
            this.accountNumber = accountNumber;

        public abstract decimal GetBalance();
    }
	public class BankAccount : Account
    {
        private decimal balance;

        public BankAccount(string accountNumber, decimal balance) : base(accountNumber) =>
            this.balance = balance;

        public override decimal GetBalance() =>
            balance;
    }
	public class BankAccountGroup : Account
    {
        private List<Account> accounts;

        public BankAccountGroup(string accountNumber) : base(accountNumber) =>
            accounts = new List<Account>();

        public void AddAccount(Account account) =>
            accounts.Add(account);

        public override decimal GetBalance()
        {
            decimal totalBalance = 0;
            foreach (var account in accounts)
                totalBalance += account.GetBalance();
            return totalBalance;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
			var account1 = new BankAccount("123456789", 1000);
			var account2 = new BankAccount("987654321", 2000);

			// Создание композитного узла (группы счетов)
			var accountGroup = new BankAccountGroup("Group 1");
			accountGroup.AddAccount(account1);
			accountGroup.AddAccount(account2);

			// Получение баланса для отдельного счета
			var balance1 = account1.GetBalance();
			Console.WriteLine("Баланс счета 1: " + balance1);

			// Получение баланса для отдельного счета
			var balance2 = account2.GetBalance();
			Console.WriteLine("Баланс счета 2: " + balance2);

			// Получение баланса для группы счетов
			var groupBalance = accountGroup.GetBalance();
			Console.WriteLine("Общий баланс группы счетов: " + groupBalance);
		}
	}
}