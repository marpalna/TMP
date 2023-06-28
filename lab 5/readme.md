# Абстрактная фабрика

C#
```
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
```

UML
```
@startuml

interface IBankAccount {
    + Deposit(amount: decimal): void
    + Withdraw(amount: decimal): void
    + GetBalance(): decimal
}

interface ICreditCard {
    + MakePayment(amount: decimal): bool
    + GetBalance(): decimal
}

interface ICreditProduct {
    + GetInterestRate(): decimal
    + GetMonthlyPayment(principal: decimal, term: int): decimal
}

abstract class BankFactory {
    + CreateBankAccount(accountNumber: string, balance: decimal): IBankAccount
    + CreateCreditCard(cardNumber: string, balance: decimal): ICreditCard
    + CreateCreditProduct(principal: decimal, term: int): ICreditProduct
}

class AlphaBankAccount {
    - accountNumber: string
    - balance: decimal
    + Deposit(amount: decimal): void
    + Withdraw(amount: decimal): void
    + GetBalance(): decimal
}

class AlphaCreditCard {
    - cardNumber: string
    - balance: decimal
    + MakePayment(amount: decimal): bool
    + GetBalance(): decimal
}

class AlphaCreditProduct {
    - principal: decimal
    - term: int
    + GetInterestRate(): decimal
    + GetMonthlyPayment(principal: decimal, term: int): decimal
}

class AlphaBankFactory {
    + CreateBankAccount(accountNumber: string, balance: decimal): IBankAccount
    + CreateCreditCard(cardNumber: string, balance: decimal): ICreditCard
    + CreateCreditProduct(principal: decimal, term: int): ICreditProduct
}

class BankClient {
    - factory: BankFactory
    + OpenBankAccount(accountNumber: string, initialDeposit: decimal): IBankAccount
    + ApplyForCreditCard(cardNumber: string, creditLimit: decimal): ICreditCard
    + ApplyForCreditProduct(principal: decimal, term: int): ICreditProduct
}

BankFactory <|-- AlphaBankFactory
IBankAccount <|.. AlphaBankAccount
ICreditCard <|.. AlphaCreditCard
ICreditProduct <|.. AlphaCreditProduct
BankFactory <|-- AlphaBankFactory
BankClient --> BankFactory

@enduml
```
![absfact-uml](https://github.com/Vanifatov/tmp/assets/90778174/2918191a-6e18-4f95-a182-b3b45903b896)


# Строитель

C#
```
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
```

UML
```
@startuml

class BankAccount {
    + AccountNumber: string
    + Balance: decimal
    + AccountHolder: string
    + AccountType: string
}

interface IBankAccountBuilder {
    + SetAccountNumber(accountNumber: string): void
    + SetBalance(balance: decimal): void
    + SetAccountHolder(accountHolder: string): void
    + SetAccountType(accountType: string): void
    + GetBankAccount(): BankAccount
}

class BankAccountBuilder {
    - bankAccount: BankAccount
    + SetAccountNumber(accountNumber: string): void
    + SetBalance(balance: decimal): void
    + SetAccountHolder(accountHolder: string): void
    + SetAccountType(accountType: string): void
    + GetBankAccount(): BankAccount
}

class BankAccountDirector {
    - bankAccountBuilder: IBankAccountBuilder
    + CreateBankAccount(accountNumber: string, balance: decimal, accountHolder: string, accountType: string): void
    + GetBankAccount(): BankAccount
}

BankAccountBuilder ..|> IBankAccountBuilder
BankAccountDirector--> IBankAccountBuilder
BankAccountDirector --> BankAccountBuilder
BankAccountBuilder --> BankAccount

@enduml
```
![builder-uml](https://github.com/Vanifatov/tmp/assets/90778174/214a4a03-029e-4d3a-a4cc-d4080e6e84da)


# Адаптер

C#
```
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
```

UML
```
@startuml

interface IBankAccount {
    + GetAccountNumber(): string
    + GetBalance(): decimal
}

class ThirdPartyBankAccount {
    + AccountNumber: string
    + Balance: double
}

class BankAccountAdapter {
    - thirdPartyBankAccount: ThirdPartyBankAccount
    + BankAccountAdapter(thirdPartyBankAccount: ThirdPartyBankAccount)
    + GetAccountNumber(): string
    + GetBalance(): decimal
}

IBankAccount <|.. BankAccountAdapter
BankAccountAdapter --> ThirdPartyBankAccount

@enduml
```
![adapter-uml](https://github.com/Vanifatov/tmp/assets/90778174/463d5c31-18ca-4bf7-9336-a2d61baba475)


# Посредник

C#
```
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
```

UML
```
@startuml

interface IBankMediator {
    + SendTransfer(sender: BankAccount, receiver: BankAccount, amount: decimal)
    + SendWithdrawal(sender: BankAccount, amount: decimal)
}

class BankMediator {
    + SendTransfer(sender: BankAccount, receiver: BankAccount, amount: decimal)
    + SendWithdrawal(sender: BankAccount, amount: decimal)
}

class BankAccount {
    - accountNumber: string
    - balance: decimal
    - accountHolder: string
    - accountType: string
    - bankMediator: IBankMediator
    + BankAccount(accountNumber: string, balance: decimal, accountHolder: string, accountType: string, bankMediator: IBankMediator)
    + Withdraw(amount: decimal): bool
    + Transfer(receiver: BankAccount, amount: decimal): bool
    + Deposit(amount: decimal)
    + GetAccountNumber(): string
    + GetBalance(): decimal
    + GetAccountHolder(): string
    + GetAccountType(): string
}

IBankMediator <|.. BankMediator
BankMediator ..> BankAccount

@enduml
```
![mediator-uml](https://github.com/Vanifatov/tmp/assets/90778174/6066c8f1-780a-4e71-9fa5-1af954da36af)
