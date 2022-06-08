﻿var tasks = new List<Task>();
var ba = new BankAccount();

SpinLock sl = new SpinLock();

for (int i = 0; i < 10; i++)
{
    tasks.Add(
        Task.Factory.StartNew(() =>
        {
            for (int j = 0; j < 1000; j++)
            {
                var lockTaken = false;
                try
                {
                    sl.Enter(ref lockTaken);
                    ba.Deposit(100);
                }
                finally
                {
                    if (lockTaken) sl.Exit();
                }
            }
        }));

    tasks.Add(
        Task.Factory.StartNew(() =>
        {
            for (int j = 0; j < 1000; j++)
            {
                var lockTaken = false;
                try
                {
                    sl.Enter(ref lockTaken);
                    ba.Withdraw(100);
                }
                finally
                {
                    if (lockTaken) sl.Exit();
                }
            }
        }));
}

Task.WaitAll(tasks.ToArray());
Console.WriteLine("Final balance: " + ba.Balance);

public class BankAccount
{
    private int balance;

    public int Balance { get => balance; private set => balance = value; }

    public void Deposit(int amount)
    {
        Balance += amount;
    }

    public void Withdraw(int amount)
    {
        Balance -= amount;
    }

}