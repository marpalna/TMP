# Итератор

C#
```
internal class BankAccount
{
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }
}
internal class BankAccountCollection : IEnumerable<BankAccount>
{
    private List<BankAccount> _accounts = new List<BankAccount>();

    public IEnumerator<BankAccount> GetEnumerator() =>
        _accounts.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void AddAccount(BankAccount account) =>
        _accounts.Add(account);
}
internal class Program
{
    static void Main(string[] args)
    {
        var collection = new BankAccountCollection();

        collection.AddAccount(new BankAccount { AccountNumber = "111", Balance = 100 });
        collection.AddAccount(new BankAccount { AccountNumber = "222", Balance = 200 });
        collection.AddAccount(new BankAccount { AccountNumber = "333", Balance = 300 });

        foreach (var account in collection)
            Console.WriteLine($"Account number: {account.AccountNumber}, Balance: {account.Balance}");
    }
}
```

UML
```
@startuml
interface IEnumerator<BankAccount> {
  + Current: BankAccount
  + MoveNext(): bool
  + Reset(): void
}

interface IEnumerable<BankAccount> {
  + GetEnumerator(): IEnumerator<BankAccount>
}

class BankAccounts {
  - accounts: List<BankAccount>
  + GetEnumerator(): IEnumerator<BankAccount>
  + AddAccount(account: BankAccount): void
}

class BankAccount {
  - accountNumber: string
  - balance: decimal
  + AccountNumber: string
  + Balance: decimal
}

BankAccounts -> IEnumerable
BankAccounts ..> BankAccount
BankAccount -up-|> IEnumerator
BankAccount ..> BankAccounts: contains
@enduml
```
![iter-uml](https://github.com/Vanifatov/tmp/assets/90778174/bb504b6c-df0f-4216-b12d-4c08e35b0c72)


# Посетитель

C#
```
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
```

UML
```
@startuml
interface IBankAccountVisitor {
    + Visit(CreditAccount account)
    + Visit(SavingsAccount account)
}

abstract class BankAccount {
    + AccountNumber : string
    + Balance : decimal
    + Accept(visitor: IBankAccountVisitor) : void
}

class CreditAccount {
    + Accept(visitor: IBankAccountVisitor) : void
}

class SavingsAccount {
    + Accept(visitor: IBankAccountVisitor) : void
}

class BankReport {
    + Visit(CreditAccount account) : void
    + Visit(SavingsAccount account) : void
}

BankAccount <|-- CreditAccount 
BankAccount <|-- SavingsAccount

IBankAccountVisitor <|.. BankReport
@enduml
```
![visit-uml](https://github.com/Vanifatov/tmp/assets/90778174/3f0f72cc-5ca0-426e-a975-8be542763085)
