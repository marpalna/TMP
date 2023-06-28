namespace builder
{
	public class BankAccount
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string AccountHolder { get; set; }
        public string AccountType { get; set; }
    }
	public interface IBankAccountBuilder
    {
        void SetAccountNumber(string accountNumber);
        void SetBalance(decimal balance);
        void SetAccountHolder(string accountHolder);
        void SetAccountType(string accountType);
        BankAccount GetBankAccount();
    }
    public class BankAccountDirector
    {
        private IBankAccountBuilder _bankAccountBuilder;

        public BankAccountDirector(IBankAccountBuilder bankAccountBuilder) =>
            _bankAccountBuilder = bankAccountBuilder;

        public void CreateBankAccount(string accountNumber, decimal balance, string accountHolder, string accountType)
        {
            _bankAccountBuilder.SetAccountNumber(accountNumber);
            _bankAccountBuilder.SetBalance(balance);
            _bankAccountBuilder.SetAccountHolder(accountHolder);
            _bankAccountBuilder.SetAccountType(accountType);
        }

        public BankAccount GetBankAccount() =>
            _bankAccountBuilder.GetBankAccount();
    }
    public class BankAccountBuilder : IBankAccountBuilder
    {
        private BankAccount _bankAccount;

        public BankAccountBuilder() =>
            _bankAccount = new BankAccount();

        public void SetAccountNumber(string accountNumber) =>
            _bankAccount.AccountNumber = accountNumber;

        public void SetBalance(decimal balance) =>
            _bankAccount.Balance = balance;

        public void SetAccountHolder(string accountHolder) =>
            _bankAccount.AccountHolder = accountHolder;

        public void SetAccountType(string accountType) =>
            _bankAccount.AccountType = accountType;

        public BankAccount GetBankAccount() =>
            _bankAccount;
    }
	internal class Program
    {
        static void Main(string[] args)
        {
			IBankAccountBuilder bankAccountBuilder = new BankAccountBuilder();
			BankAccountDirector bankAccountDirector = new BankAccountDirector(bankAccountBuilder);

			bankAccountDirector.CreateBankAccount("123456789", 1000, "John Smith", "Checking");
			BankAccount bankAccount = bankAccountDirector.GetBankAccount();
		}
	}
}