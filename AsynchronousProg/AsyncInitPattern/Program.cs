var myClass = new MyClass();

//if (myClass is IAsyncInit ai)
//await ai.InitTask;

var oc = new MyOtherClass(myClass);
await oc.InitTask;

interface IAsyncInit
{
    Task InitTask { get; }
}

class MyClass: IAsyncInit
{
    public Task InitTask { get; }

    public MyClass() 
    {
        InitTask = InitAsync();
    }

    private async Task InitAsync()
    {
        await Task.Delay(1000);
    }

}

class MyOtherClass : IAsyncInit
{
    private readonly MyClass myClass;

    public Task InitTask { get; }

    public MyOtherClass(MyClass myClass)
    {
        InitTask = InitAsync();
        this.myClass = myClass;
    }

    private async Task InitAsync()
    {
        if (myClass is IAsyncInit ai)
            await ai.InitTask;

        await Task.Delay(1000);
    }

}