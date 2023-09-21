// From the website
using System;
using System.Threading.Tasks;

public class Account
{
    private readonly object balanceLock = new object();
    private decimal balance;

    public Account(decimal initialBalance) => balance = initialBalance;

    public decimal withDraw(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "The debit amount cannot be negative.");
        }

        decimal appliedAmount = 0;
        lock (balanceLock)
        {
            if (balance >= amount)
            {
                balance -= amount;
                appliedAmount = amount;
            }
        }
        return appliedAmount;
    }

    public void Deposit(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "The credit amount cannot be negative.");
        }

        lock (balanceLock)
        {
            balance += amount;
        }
    }

    public decimal GetBalance()
    {
        lock (balanceLock)
        {
            return balance;
        }
    }
}

class AccountTest
{
    static async Task Main()
    {
        var account = new Account(1000);
        var tasks = new Task[100];
        for (int i = 0; i < tasks.Length; i++)
        {
            tasks[i] = Task.Run(() => Update(account));
        }
        await Task.WhenAll(tasks);
        Console.WriteLine($"Account's balance is {account.GetBalance()}");
        // Output:
        // Account's balance is 2000
    }

    static void Update(Account account)
    {
        decimal[] amounts = { 0, 2, -3, 6, -2, -1, 8, -5, 11, -6 };
        foreach (var amount in amounts)
        {
            if (amount >= 0)
            {
                account.Deposit(amount);
            }
            else
            {
                account.withDraw(Math.Abs(amount));
            }
        }
    }
}












//The one I made according to questions need

using System;
using System.Threading;

class Account
{
    private readonly object balLock = new object();
    private string name;
    private double amount;

    public Account(string name, double initialAmount)
    {
        this.name = name;
        this.amount = initialAmount;
    }

    public void Withdraw(double withdrawalAmount)
    {
        lock (balLock)
        {
            if (withdrawalAmount > 0 && withdrawalAmount <= amount)
            {
                amount -= withdrawalAmount;
                Console.WriteLine($"{name} withdrew ${withdrawalAmount}. New balance: ${amount}");
            }
            else
            {
                Console.WriteLine($"{name} could not withdraw ${withdrawalAmount}. Insufficient balance.");
            }
        }
    }

    public void Deposit(double depositAmount)
    {
        lock (balLock)
        {
            if (depositAmount > 0)
            {
                amount += depositAmount;
                Console.WriteLine($"{name} deposited ${depositAmount}. New balance: ${amount}");
            }
            else
            {
                Console.WriteLine($"{name} could not deposit ${depositAmount}. Invalid amount.");
            }
        }
    }

    public void BalanceCheck()
    {
        lock (balLock)
        {
            Console.WriteLine($"{name}'s account balance: ${amount}");
        }
    }
}

class Program
{
    static void Main()
    {
        Account account = new Account("Abir", 1000);

        Thread thread1 = new Thread(() => account.Withdraw(200));
        Thread thread2 = new Thread(() => account.Deposit(500));
        Thread thread3 = new Thread(() => account.BalanceCheck());

        thread1.Start();
        thread2.Start();
        thread3.Start();

        thread1.Join();
        thread2.Join();
        thread3.Join();

        Console.WriteLine("All threads completed.");
    }
}
