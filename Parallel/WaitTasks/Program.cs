var cts = new CancellationTokenSource();
var token = cts.Token;
var t = new Task(() =>
{
    global::System.Console.WriteLine("I take 5 seconds");
    for (int i = 0; i < 5; i++)
    {
        token.ThrowIfCancellationRequested();
        Thread.Sleep(1000);
    }
    global::System.Console.WriteLine("foi");
}, token);
t.Start();

Task t2 = Task.Factory.StartNew(() => Thread.Sleep(3000), token);

//cts.Cancel();
//t.Wait(token);
Task.WaitAny(t, t2);
//Task.WaitAll(t, t2);

Console.WriteLine($"Task t status is {t.Status}");
Console.WriteLine($"Task t2 status is {t2.Status}");

Console.WriteLine("end");
Console.ReadKey();
