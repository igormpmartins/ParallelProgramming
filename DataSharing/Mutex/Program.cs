GlobalMutex();

var tasks = new List<Task>();
var ba = new BankAccount(0);
var ba2 = new BankAccount(0);

Mutex mutex = new Mutex();
Mutex mutex2 = new Mutex();

for (int i = 0; i < 10; i++)
{
    tasks.Add(
        Task.Factory.StartNew(() =>
        {
            for (int j = 0; j < 1000; j++)
            {
                var haveLock = mutex.WaitOne();
                try
                {
                    ba.Deposit(1);
                }
                finally
                {
                    if (haveLock) mutex.ReleaseMutex();
                }
            }
        }));

    tasks.Add(
        Task.Factory.StartNew(() =>
        {
            for (int j = 0; j < 1000; j++)
            {
                var haveLock = mutex2.WaitOne();
                try
                {
                    ba2.Deposit(1);
                }
                finally
                {
                    if (haveLock) mutex2.ReleaseMutex();
                }
            }
        }));

    tasks.Add(
        Task.Factory.StartNew(() =>
        {
            for (int j = 0; j < 1000; j++)
            {
                var haveLock = Mutex.WaitAll(new[] { mutex, mutex2 });
                try
                {
                    ba.Transfer(ba2, 1);
                }
                finally
                {
                    if (haveLock)
                    {
                        mutex.ReleaseMutex();
                        mutex2.ReleaseMutex();
                    }
                }
            }
        }));

}

Task.WaitAll(tasks.ToArray());
Console.WriteLine("Final balance 1: " + ba.Balance);
Console.WriteLine("Final balance 2: " + ba2.Balance);

static void GlobalMutex()
{
    const string appName = "MyTreku";

    Mutex appMutex;

    try
    {
        appMutex = Mutex.OpenExisting(appName);
        Console.WriteLine("Sorry, already running!");
        return;
    }
    catch (WaitHandleCannotBeOpenedException e)
    {
        Console.WriteLine("Running...");
        appMutex = new Mutex(false, appName);
    }

    Console.ReadKey();
    //appMutex.ReleaseMutex();
}

public class BankAccount
{
    private int balance;

    public int Balance { get => balance; private set => balance = value; }

    public BankAccount(int balance)
    {
        Balance = balance;
    }

    public void Deposit(int amount)
    {
        Balance += amount;
    }

    public void Withdraw(int amount)
    {
        Balance -= amount;
    }

    public void Transfer(BankAccount destination, int amount)
    {
        Balance -= amount;
        destination.Deposit(amount);
    }

}