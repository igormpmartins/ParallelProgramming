ReaderWriterLockSlim padLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
Random random = new Random();

int x = 0;
var tasks = new List<Task>();

for (int i = 0; i < 10; i++)
{
    tasks.Add(Task.Factory.StartNew(() =>
    {
        //padLock.EnterReadLock();
        padLock.EnterUpgradeableReadLock();

        if (i%2 == 0)
        {
            padLock.EnterWriteLock();
            x = 123;
            padLock.ExitWriteLock();
        }

        global::System.Console.WriteLine($"Entered read lock, x = {x}");
        Thread.Sleep(5000);

        //padLock.ExitReadLock();
        padLock.ExitUpgradeableReadLock();
        global::System.Console.WriteLine($"Exited read lock, x = {x}");
    }));
}

try
{
    Task.WaitAll(tasks.ToArray());
}
catch (AggregateException ae)
{
    ae.Handle(e =>
    {
        global::System.Console.WriteLine(e);
        return true;
    });
}

while (true)
{
    Console.ReadKey();
    padLock.EnterWriteLock();
    Console.WriteLine("Write lock acquired.");

    int newValue = random.Next(10);
    x = newValue;

    Console.WriteLine($"Set x= {x}");

    padLock.ExitWriteLock();
    Console.WriteLine("Write lock released");

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