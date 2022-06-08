// See https://aka.ms/new-console-template for more information

var cts = new CancellationTokenSource();
var token = cts.Token;

token.Register(() => Console.WriteLine("Cancellation requested"));

var t = new Task(() =>
{
    int i = 0;
    while (true)
    {

        //if (token.IsCancellationRequested)
        //    break;
        //throw new OperationCanceledException();
        token.ThrowIfCancellationRequested();

        Console.WriteLine(i++);
    }

}, token);
t.Start();

Task.Factory.StartNew(() =>
{
    token.WaitHandle.WaitOne();
    Console.WriteLine("Wait handle released!");
});

Console.ReadKey();
cts.Cancel();

Console.WriteLine("Finished!");
Console.ReadKey();
