int taskCount = 5;
CountdownEvent cte = new CountdownEvent(taskCount);
Random random = new Random();

for (int i = 0; i < taskCount; i++)
{
    Task.Factory.StartNew(() =>
    {
        Console.WriteLine($"Entering task {Task.CurrentId}");
        Thread.Sleep(random.Next(3000));
        cte.Signal();
        global::System.Console.WriteLine($"Exiting {Task.CurrentId}");
    });
}

var finalTask = Task.Factory.StartNew(() =>
{
    Console.WriteLine($"Waiting for other task do complete in {Task.CurrentId}");
    cte.Wait();
    //Thread.Sleep(5000);
    global::System.Console.WriteLine("All tasks completed");
});

finalTask.Wait();


