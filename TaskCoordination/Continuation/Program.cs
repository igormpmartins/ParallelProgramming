var task = Task.Factory.StartNew(() => "Task 1");
var task2 = Task.Factory.StartNew(() => "Task 2");

var task3 = Task.Factory.ContinueWhenAll(new[] { task, task2 },
    tasks =>
    {
        global::System.Console.WriteLine("Task completed");
        foreach (var t in tasks)
            Console.WriteLine(" - " + t.Result);
        global::System.Console.WriteLine("All tasks done");
        //return true;
    });

/*var task3 = Task.Factory.ContinueWhenAny(new[] {task, task2},
    t =>
    {
        global::System.Console.WriteLine("Task completed");
        Console.WriteLine(" - " + t.Result);
        global::System.Console.WriteLine("All tasks done");
    });

task3.Wait();*/

var taskCont = Task.Factory.StartNew(() =>
{
    Console.WriteLine("Boiling Water");
    return "boiled!";
});

var taskCont2 = taskCont.ContinueWith(t =>
{
    Console.WriteLine($"Completed task {t.Id}, pour water, etc.");
    global::System.Console.WriteLine($"Result: {t.Result}");
});

taskCont2.Wait();
