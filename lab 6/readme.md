# Инверсия управления

C#
```
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
```

UML
```
@startuml

interface IBalanceChecker {
    + GetBalance(accountNumber: string): decimal
}

class ExternalBalanceChecker {
    + GetBalance(accountNumber: string): decimal
}

class BankClient {
    - balanceChecker: IBalanceChecker
    + BankClient(balanceChecker: IBalanceChecker)
    + CheckBalance(accountNumber: string): bool
}

IBalanceChecker <|.. ExternalBalanceChecker
BankClient --> IBalanceChecker

@enduml
```
![ioc-uml](https://github.com/Vanifatov/tmp/assets/90778174/1110f668-7dce-47c9-880d-f8f97bec146f)


# Заместитель

C#
```
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
```

UML
```
@startuml

interface IBalanceChecker {
    + GetBalance(accountNumber: string): decimal
}

class ExternalBalanceChecker {
    + GetBalance(accountNumber: string): decimal
}

class BalanceCheckerProxy {
    - balanceChecker: IBalanceChecker
    - balanceCache: Dictionary<string, decimal>
    + BalanceCheckerProxy(balanceChecker: IBalanceChecker)
    + GetBalance(accountNumber: string): decimal
}

IBalanceChecker <|.. ExternalBalanceChecker
IBalanceChecker <|.. BalanceCheckerProxy
BalanceCheckerProxy o-- IBalanceChecker

@enduml
```
![proxy-uml](https://github.com/Vanifatov/tmp/assets/90778174/33f6e750-a7e1-44dd-b244-58a6123fc7a3)


# Компоновщик

C#
```
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
```

UML
```
@startuml

abstract class Account {
    - accountNumber: string
    + Account(accountNumber: string)
    + GetBalance(): decimal
}

class BankAccount {
    - balance: decimal
    + BankAccount(accountNumber: string, balance: decimal)
    + GetBalance(): decimal
}

class BankAccountGroup {
    - accounts: List<Account>
    + BankAccountGroup(accountNumber: string)
    + AddAccount(account: Account): void
    + GetBalance(): decimal
}

Account <|-- BankAccount
Account <|-- BankAccountGroup
BankAccountGroup o-- Account

@enduml
```
![composite-uml](https://github.com/Vanifatov/tmp/assets/90778174/0f494888-72ff-4e2a-90d5-0ee2d135f63f)
