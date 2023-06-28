namespace adapter
{
    public interface IBankAccount
    {
        string GetAccountNumber();
        decimal GetBalance();
    }
    public class ThirdPartyBankAccount
    {
        public string AccountNumber { get; set; }
        public double Balance { get; set; }
    }
    public class BankAccountAdapter : IBankAccount
    {
        private ThirdPartyBankAccount _thirdPartyBankAccount;

        public BankAccountAdapter(ThirdPartyBankAccount thirdPartyBankAccount) =>
            _thirdPartyBankAccount = thirdPartyBankAccount;

        public string GetAccountNumber() =>
            _thirdPartyBankAccount.AccountNumber;

        public decimal GetBalance() =>
            (decimal)_thirdPartyBankAccount.Balance;
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            ThirdPartyBankAccount thirdPartyBankAccount = new ThirdPartyBankAccount()
            {
                AccountNumber = "123456789",
                Balance = 1000.0
            };

            IBankAccount bankAccount = new BankAccountAdapter(thirdPartyBankAccount);
        }
    }
}
