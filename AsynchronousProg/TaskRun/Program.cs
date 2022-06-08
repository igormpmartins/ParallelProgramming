/*var t = Task.Factory.StartNew(async() =>
{
    //var inner = Task.Factory.StartNew(() => 123);
    //return inner;
    await Task.Delay(5000);
    return 123;
}).Unwrap();
*/

/*
int result = await Task.Run(async() =>
{
    await Task.Delay(1000);
    return 42;
});
*/

var result = await Task.Factory.StartNew(async () =>
    {
        Console.WriteLine("Iniciando");
        await Task.Delay(2000);
        Console.WriteLine("Indo");
        return 42;
    },
    CancellationToken.None,
    TaskCreationOptions.DenyChildAttach,
    TaskScheduler.Default);

await Task.WhenAny(result);

Console.WriteLine("foi!");
Console.ReadLine();