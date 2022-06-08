

class Stuff
{
    private static int value;

    private readonly Lazy<Task<int>> AutoIncValue = new Lazy<Task<int>>(async() =>
    {
        await Task.Delay(1000).ConfigureAwait(false);
        return value++;
    });    
    
    //Runs on the Thread Pool!
    private readonly Lazy<Task<int>> AutoIncValue2 = new Lazy<Task<int>>(() 
        => Task.Run(async () =>
    {
        await Task.Delay(1000);
        return value++;
    }));

    public async void UseValue()
    {
        int value = await AutoIncValue.Value;
    }
}