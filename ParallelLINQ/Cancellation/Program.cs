var items = ParallelEnumerable.Range(1, 20);
var cts = new CancellationTokenSource();

var results = items.WithCancellation(cts.Token).Select(i =>
{
    double result = Math.Log10((double)i);

    //if (result > 1) throw new InvalidOperationException();
    global::System.Console.WriteLine($"i = {i}, tid = {Task.CurrentId}");
    return result;
});

try
{
    foreach (var c in results)
    {
        if (c > 1) cts.Cancel();

        Console.WriteLine($"result = {c}");
    }
}
catch (AggregateException ae)
{
    ae.Handle(e =>
    {
        global::System.Console.WriteLine($"{e.GetType().Name}: {e.Message}");
        return true;
    });
}
catch (OperationCanceledException e)
{
    Console.WriteLine("Cancelled!" + e.Message);
}