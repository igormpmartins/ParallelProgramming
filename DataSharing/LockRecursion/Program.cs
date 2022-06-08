// See https://aka.ms/new-console-template for more information

Koisa.LockRecursion(5);

Console.WriteLine("Finished!");

static class Koisa
{
    //it must be true, otherwise, we run into a deadlock!
    static SpinLock sl = new SpinLock(true);

    public static void LockRecursion(int x)
    {
        bool lockTaken = false;
        try
        {
            sl.Enter(ref lockTaken);
        }
        catch (LockRecursionException ex)
        {
            Console.WriteLine("Exception: " + ex);
        }
        finally
        {
            if (lockTaken)
            {
                Console.WriteLine($"Took a lock, x = {x}");
                LockRecursion(x - 1);
                sl.Exit();
            } else
            {
                Console.WriteLine($"Failed to take a lock = {x}");
            }
        }
    }

}