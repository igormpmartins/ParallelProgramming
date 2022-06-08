var cts = new CancellationTokenSource();
var token = cts.Token;

var t = new Task(() =>
{
    Console.WriteLine("Press any key to disarm; you have 5 seconds!");
    bool cancelled = token.WaitHandle.WaitOne(5000);
    Console.WriteLine(cancelled? "Bomb disarmed.": "BOOM!!!");
}, token);

t.Start();

Console.ReadKey();
cts.Cancel();

Console.WriteLine("End");
Console.ReadKey();
