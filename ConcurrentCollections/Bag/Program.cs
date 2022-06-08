﻿//stack LIFO
//queue FIFO
//bag - no ordering

using System.Collections.Concurrent;

var bag = new ConcurrentBag<int>();
var tasks = new List<Task>();

for (int i = 0; i < 10; i++)
{
    var i1 = i;
    tasks.Add(Task.Factory.StartNew(() =>
    {
        bag.Add(i1);
        global::System.Console.WriteLine($"{Task.CurrentId} has added {i1}");

        int result;

        if (bag.TryPeek(out result))
        {
            global::System.Console.WriteLine($"{Task.CurrentId} has peeked the value {result}");
        }

    }));
}

Task.WaitAll(tasks.ToArray());

int last;

if (bag.TryTake(out last))
{
    Console.WriteLine($"I got {last}");
}