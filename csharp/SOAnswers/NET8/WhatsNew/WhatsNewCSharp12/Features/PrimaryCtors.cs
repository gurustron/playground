using System.Reflection;

namespace WhatsNewCSharp12.Features;

public class PrimaryCtors
{
    public static void Do()
    {
        new CtorLess();
        
        Console.WriteLine("-------------");
        Console.WriteLine(new Distance());
        PrintFields<Distance>();
        Console.WriteLine("-------------");

        Console.WriteLine(new DistanceClass(1, 2, 3));
        PrintFields<DistanceClass>();
        Console.WriteLine("-------------");

        Console.WriteLine(new SameNameProps("1234567890", "Owner"));
        PrintFields<SameNameProps>();
        Console.WriteLine("-------------");
        
        Console.WriteLine(new BankAccount("1234567890", "Owner", "1"));
        PrintFields<BankAccount>();
        Console.WriteLine("-------------");
        
        Console.WriteLine(new CheckingAccount("1234567890", "Owner", "2"));
        PrintFields<CheckingAccount>();
        Console.WriteLine("-------------");

        void PrintFields<T>()
        {
            var fieldInfos = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
            foreach (var fieldInfo in fieldInfos)
            {
                Console.WriteLine(fieldInfo.Name);
            }
        }
    }
}

public class CtorLess; // no parameters
public readonly struct Distance(double dx, double dy, double dz)
{
    public readonly double Magnitude { get; } = Math.Sqrt(dx * dx + dy * dy);
    public readonly double Direction { get; } = Math.Atan2(dy, dx);

    // this will result in dy introduced as field:
    // public readonly double MyDyWithField => dy; // <dx>P

    // this will result in backing field created by for MyDyWithBackingField:
    // public readonly double MyDyWithBackingField {get;} = dy; //<MyDyWithBackingField>k__BackingField
    
    public void Do() => Console.WriteLine(dx + 1);
}

public class DistanceClass(double dx, double dy, double dz)
{
    public double Magnitude { get; } = Math.Sqrt(dx * dx + dy * dy);
    public double Direction { get; } = Math.Atan2(dy, dx);

    public void Do() => Console.WriteLine(dx + 1);
}

public class BankAccount(string accountID, string owner, string check)
{
    public string AccountID { get; } = ValidAccountNumber(accountID) 
        ? accountID 
        : throw new ArgumentException("Invalid account number", nameof(accountID));

    public string Owner { get; } = string.IsNullOrWhiteSpace(owner) 
        ? throw new ArgumentException("Owner name cannot be empty", nameof(owner)) 
        : owner;

    public override string ToString() => $"Account ID: {AccountID}, Owner: {Owner}, Check: {check}";

    public static bool ValidAccountNumber(string accountID) => 
        accountID?.Length == 10 && accountID.All(c => char.IsDigit(c));

    // can be called in ordinary ctor:
    public bool ValidAccountNumberInstance(string accountID) => 
        accountID?.Length == 10 && accountID.All(c => char.IsDigit(c));
    // public BankAccount(string accountID, string owner)
    // {
    //     AccountID = ValidAccountNumberInstance(accountID) 
    //         ? accountID 
    //         : throw new ArgumentException("Invalid account number", nameof(accountID));
    //     Owner = string.IsNullOrWhiteSpace(owner) 
    //         ? throw new ArgumentException("Owner name cannot be empty", nameof(owner)) 
    //         : owner;
    // }
}

public class CheckingAccount(string accountID, string owner, string check, decimal overdraftLimit = 0) : BankAccount(accountID, owner, check)
{
    public decimal CurrentBalance { get; private set; }

    public void Deposit(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Deposit amount must be positive");
        }
        CurrentBalance += amount;
    }

    public void Withdrawal(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Withdrawal amount must be positive");
        }
        if (CurrentBalance - amount < -overdraftLimit)
        {
            throw new InvalidOperationException("Insufficient funds for withdrawal");
        }
        CurrentBalance -= amount;
    }
    
    public override string ToString() => $"Account ID: {AccountID}, Owner: {Owner}, Balance: {CurrentBalance}, Check: {check}";
}

public class FixedAdminAccount : BankAccount
{
    public FixedAdminAccount() : base("1234567890", "Admin", "true")
    {
    }
}

public class FixedAdminAccountEmptyCtor() : BankAccount("1234567890", "111", "111");

public class SameNameProps(string accountID, string owner)
{
    private readonly string accountID = accountID;
    private readonly string owner = owner;

    public void DoSomethingWrong()
    {
        // accountID = "Wrong";
    }
}