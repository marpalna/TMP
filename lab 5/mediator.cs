namespace mediator
{
	public class BankAccount
    {
        private string _accountNumber;
        private decimal _balance;
        private string _accountHolder;
        private string _accountType;
        private IBankMediator _bankMediator;

        public BankAccount(string accountNumber, decimal balance, string accountHolder, string accountType, IBankMediator bankMediator)
        {
            _accountNumber = accountNumber;
            _balance = balance;
            _accountHolder = accountHolder;
            _accountType = accountType;
            _bankMediator = bankMediator;
        }

        public bool Withdraw(decimal amount)
        {
            bool success = false;
            if (_balance >= amount)
            {
                _balance -= amount;
                _bankMediator.SendWithdrawal(this, amount);
                success = true;
            }
            return success;
        }

        public bool Transfer(BankAccount receiver, decimal amount)
        {
            bool success = false;
            if (_balance >= amount)
            {
                _balance -= amount;
                _bankMediator.SendTransfer(this, receiver, amount);
                success = true;
            }
            return success;
        }

        public void Deposit(decimal amount) =>
            _balance += amount;

        public string GetAccountNumber() =>
            _accountNumber;

        public decimal GetBalance() =>
            _balance;

        public string GetAccountHolder() =>
            _accountHolder;

        public string GetAccountType() =>
            _accountType;
    }
    public interface IBankMediator
    {
        void SendTransfer(BankAccount sender, BankAccount receiver, decimal amount);
        void SendWithdrawal(BankAccount sender, decimal amount);
    }
    public class BankMediator : IBankMediator
    {
        public void SendTransfer(BankAccount sender, BankAccount receiver, decimal amount)
        {
            if (sender.Withdraw(amount))
                receiver.Deposit(amount);
        }

        public void SendWithdrawal(BankAccount sender, decimal amount) =>
            sender.Withdraw(amount);
    }
	internal class Program
    {
        static void Main(string[] args)
        {
			IBankMediator bankMediator = new BankMediator();

			BankAccount senderAccount = new BankAccount("123456789", 1000.0m, "John Doe", "checking", bankMediator);
			BankAccount receiverAccount = new BankAccount("987654321", 0.0m, "Jane Smith", "savings", bankMediator);

			// Перевод средств со счета отправителя на счет получателя
			senderAccount.Transfer(receiverAccount, 500.0m);

			// Снятие денег
			senderAccount.Withdraw(200.0m);
		}
	}
}