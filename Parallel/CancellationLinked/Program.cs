var planned = new CancellationTokenSource();
var prevent = new CancellationTokenSource();
var emergency = new CancellationTokenSource();

var paranoid = CancellationTokenSource.CreateLinkedTokenSource(
    planned.Token, prevent.Token, emergency.Token);

Task.Factory.StartNew(() =>
{
    int i = 0;
    while (true)
    {
        paranoid.Token.ThrowIfCancellationRequested();
        Console.WriteLine($"{i++}\t");
        Thread.Sleep(1000);
    }
});

Console.ReadKey();
planned.Cancel();

Console.WriteLine("End");
Console.ReadKey();
