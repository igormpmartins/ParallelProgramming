/*var evt = new ManualResetEventSlim();

Task.Factory.StartNew(() =>
{
    Console.WriteLine("Boiling water");
    evt.Set();
});

var makeTea = Task.Factory.StartNew(() =>
{
    Console.WriteLine("Waiting for water...");
    evt.Wait();
    global::System.Console.WriteLine("Here is your tea");
});

makeTea.Wait();*/

var evt = new AutoResetEvent(false);
//var evt = new ManualResetEventSlim(false);

Task.Factory.StartNew(() =>
{
    Console.WriteLine("Boiling water");
    evt.Set();
});

var makeTea = Task.Factory.StartNew(() =>
{
    Console.WriteLine("Waiting for water...");
    evt.WaitOne();
    global::System.Console.WriteLine("Here is your tea");
    var ok = evt.WaitOne(1000);

    if (ok)
    {
        global::System.Console.WriteLine("Enjoy your tea");
    } else
    {
        global::System.Console.WriteLine("No tea for you");
    }


});

makeTea.Wait();