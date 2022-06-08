using System.Collections.Concurrent;

BlockingCollection<int> messages = new BlockingCollection<int>(new ConcurrentBag<int>(), 10);
CancellationTokenSource cts = new CancellationTokenSource();
Random random = new Random();

Task.Factory.StartNew(ProduceAndConsume, cts.Token);
Console.ReadKey();
cts.Cancel();

void ProduceAndConsume()
{
    var producer = Task.Factory.StartNew(RunProducer);
    var consumer = Task.Factory.StartNew(RunConsumer);

    try
    {
        Task.WaitAll(new[] { producer, consumer }, cts.Token);
    }
    catch (AggregateException ae)
    {
        ae.Handle(e => true);
    }

}

void RunConsumer()
{
    foreach (var item in messages.GetConsumingEnumerable())
    {
        cts.Token.ThrowIfCancellationRequested();
        Console.WriteLine($"-{item}\t");
        Thread.Sleep(1000);
    }
}

void RunProducer()
{
    while (true)
    {
        cts.Token.ThrowIfCancellationRequested();
        int i = random.Next(100);
        messages.Add(i);
        Console.WriteLine($"+{i}\t");
        Thread.Sleep(random.Next(100));
    }
}




