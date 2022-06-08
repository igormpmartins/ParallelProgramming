int sum = 0;

/*Parallel.For(1, 100001, x =>
{
    Console.WriteLine($"Task {Task.CurrentId} has sum {sum}");
    Interlocked.Add(ref sum, x);
});*/

Parallel.For(1, 100001,
    () => 0,
    (x, _, tls) =>
    {
        tls += x;
        Console.WriteLine($"Task {Task.CurrentId} has sum {tls}");
        return tls;
    },
    partialSum =>
    {
        Console.WriteLine($"Partial value of task {Task.CurrentId} is {partialSum}");
        Interlocked.Add(ref sum, partialSum);
    });

Console.WriteLine($"Sum of 1..100000 = {sum}");

