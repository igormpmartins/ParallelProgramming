using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Concurrent;

var summary = BenchmarkRunner.Run<Outro>();
Console.WriteLine(summary);

public class Outro
{

    [Benchmark]
    public void SquareEachValue()
    {
        const int count = 100000;
        var values = Enumerable.Range(0, count);
        var results = new int[count];
        Parallel.ForEach(values, i => { results[i] = (int)Math.Pow(i, 2); });
    }       
    
    [Benchmark]
    public void SquareEachBad()
    {
        const int count = 100000;
        var values = Enumerable.Range(0, count);
        var results = new int[count];
        //Parallel.ForEach(values, i => { results[i] = (int)Math.Pow(i, 2); });
        for (int i = 0; i < count; i++)
        {
            results[i] = (int)Math.Pow(i, 2);
        }
    }    
    
    [Benchmark]
    public void SquareEachValueChunked()
    {
        const int count = 100000;
        var values = Enumerable.Range(0, count);
        var results = new int[count];

        var part = Partitioner.Create(0, count, 10000);
        Parallel.ForEach(part, range =>
        {
            for (int i = range.Item1; i < range.Item2; i++)
            {
                results[i] = (int)Math.Pow(i, 2);
            }
        });

    }

}