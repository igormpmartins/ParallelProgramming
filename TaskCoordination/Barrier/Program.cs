Barrier barrier = new Barrier(2, b =>
{
    Console.WriteLine($"Phase {b.CurrentPhaseNumber} is finished");
});

var water = Task.Factory.StartNew(Water);
var cup = Task.Factory.StartNew(Cup);

var tea = Task.Factory.ContinueWhenAll(new[] { water, cup }, tasks =>
{
    global::System.Console.WriteLine("Enjoy your tea, lad!");
});

tea.Wait();

void Water()
{
    Console.WriteLine("Putting on kettle on (takes a big longer)");
    Thread.Sleep(2000);
    barrier.SignalAndWait();
    Console.WriteLine("Pourwing water into cup");
    barrier.SignalAndWait();
    Console.WriteLine("Putting the kettle away");
}

void Cup()
{
    Console.WriteLine("Finding the proper cup! (very quick)");
    barrier.SignalAndWait();
    Console.WriteLine("Adding tea.");
    barrier.SignalAndWait();
    Console.WriteLine("Adding sugar");
}
