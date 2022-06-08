const int count = 50;

var items = Enumerable.Range(1, count);
var results = new int[count];

items.AsParallel().ForAll(x =>
{
    int newValue = x * x * x;
    global::System.Console.WriteLine($"{newValue} ({Task.CurrentId})\t");
    results[x-1] = newValue;
});

Console.WriteLine();

/*foreach (var i in results)
    Console.WriteLine($"{i}\t");*/

var cubes = items.AsParallel().AsOrdered().Select(x => x * x * x);

foreach (var i in cubes)
{
    Console.WriteLine($"{i}\t");
}

Console.WriteLine();