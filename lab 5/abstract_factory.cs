namespace abstract_factory
{
	public interface IBankAccount
    {
        void Deposit(decimal amount);
        bool Withdraw(decimal amount);
        decimal GetBalance();
    }
    public interface ICreditCard
    {
        bool MakePayment(decimal amount);
        decimal GetBalance();
    }
    public interface ICreditProduct
    {
        decimal GetInterestRate();
        decimal GetMonthlyPayment(decimal principal, int term);
    }
	public abstract class BankFactory
    {
        public abstract IBankAccount CreateBankAccount(string accountNumber, decimal balance);
        public abstract ICreditCard CreateCreditCard(string cardNumber, decimal balance);
        public abstract ICreditProduct CreateCreditProduct(decimal principal, int term);
    }
     public class AlphaBankAccount : IBankAccount
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string AccountHolder { get; set; }
        public string AccountType { get; set; }

        public AlphaBankAccount(string accountNumber, decimal balance)
        {
            AccountNumber = accountNumber;
            Balance = balance;
        }

        public void Deposit(decimal amount) =>
            Balance += amount;

        public bool Withdraw(decimal amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                return true;
            }
            else
                return false;
        }

        public decimal GetBalance() => Balance;
    }
    public class AlphaBankFactory : BankFactory
    {
        public override IBankAccount CreateBankAccount(string accountNumber, decimal balance) =>
            new AlphaBankAccount(accountNumber, balance);
        public override ICreditCard CreateCreditCard(string cardNumber, decimal balance)
            => new AlphaCreditCard(cardNumber, balance);
        
        public override ICreditProduct CreateCreditProduct(decimal principal, int term)
            => new AlphaCreditProduct(principal, term);
        
    }
    internal class AlphaCreditCard : ICreditCard
    {
        public string CardNumber { get; set; }
        public decimal Balance { get; set; }

        public AlphaCreditCard(string cardNumber, decimal balance)
        {
            CardNumber = cardNumber;
            Balance = balance;
        }

        public decimal GetBalance() => Balance;

        public bool MakePayment(decimal amount)
        {
            if (amount <= Balance)
            {
                Balance -= amount;
                return true;
            }
            else return false;
        }
    }
    public class AlphaCreditProduct : ICreditProduct
    {
        public decimal Principal { get; set; }
        public int Term { get; set; }

        public AlphaCreditProduct(decimal principal, int term)
        {
            Principal = principal;
            Term = term;
        }

        public decimal GetInterestRate() => 1;

        public decimal GetMonthlyPayment(decimal principal, int term) => 1;
    }
    public class BankClient
    {
        private BankFactory _factory;

        public BankClient(BankFactory factory) =>
            _factory = factory;

        public IBankAccount OpenBankAccount(string accountNumber, decimal initialDeposit) =>
            _factory.CreateBankAccount(accountNumber, initialDeposit);

        public ICreditCard ApplyForCreditCard(string cardNumber, decimal creditLimit) =>
            _factory.CreateCreditCard(cardNumber, creditLimit);

        public ICreditProduct ApplyForCreditProduct(decimal principal, int term) =>
            _factory.CreateCreditProduct(principal, term);
    }
	internal class Program
    {
        static void Main(string[] args)
        {
			BankFactory alphaBankFactory = new AlphaBankFactory();
			BankClient client = new BankClient(alphaBankFactory);

			IBankAccount account = client.OpenBankAccount("123456789", 1000);
			ICreditCard card = client.ApplyForCreditCard("1111222233334444", 5000);
			ICreditProduct loan = client.ApplyForCreditProduct(10000, 12);
		}
	}
}