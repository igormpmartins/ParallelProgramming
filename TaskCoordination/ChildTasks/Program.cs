var parent = new Task(() =>
{
    var child = new Task(() =>
    {
        Console.WriteLine("Child task starting");
        Thread.Sleep(3000);
        //throw new Exception();
        Console.WriteLine("Child task finishing.");
    }, TaskCreationOptions.AttachedToParent);

    var completionHander = child.ContinueWith(t =>
    {
        Console.WriteLine($"Oh yeah, task {t.Id}'s state is {t.Status}");
    }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnRanToCompletion);

    var failHander = child.ContinueWith(t =>
    {
        Console.WriteLine($"Oh noh, task {t.Id}'s state is {t.Status}");
    }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnFaulted);

    child.Start();

});

parent.Start();

try
{
    parent.Wait();
}
catch (AggregateException ae)
{
    ae.Handle(e => true);
}