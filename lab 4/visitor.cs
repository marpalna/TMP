namespace visitor
{
    internal interface IBankAccountVisitor
    {
        void Visit(CreditAccount account);
        void Visit(SavingsAccount account);
    }
    internal abstract class BankAccount
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }

        // Метод принятия посетителя
        public abstract void Accept(IBankAccountVisitor visitor);
    }
    internal class BankReport : IBankAccountVisitor
    {
        public void Visit(CreditAccount account) =>
            Console.WriteLine($"Credit account {account.AccountNumber} has balance {account.Balance}");

        public void Visit(SavingsAccount account) =>
            Console.WriteLine($"Savings account {account.AccountNumber} has balance {account.Balance}");
    }
    internal class CreditAccount : BankAccount
    {
        public override void Accept(IBankAccountVisitor visitor) =>
            visitor.Visit(this);
    }
    internal class SavingsAccount : BankAccount
    {
        public override void Accept(IBankAccountVisitor visitor) =>
            visitor.Visit(this);
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var accounts = new BankAccount[]
            {
                new CreditAccount { AccountNumber = "111", Balance = 100 },
                new SavingsAccount { AccountNumber = "222", Balance = 200 },
                new CreditAccount { AccountNumber = "333", Balance = 300 }
            };

            IBankAccountVisitor bankReport = new BankReport();

            foreach (BankAccount account in accounts)
                account.Accept(bankReport);
        }
    }
}