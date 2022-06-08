using System.Collections.Concurrent;

ConcurrentDictionary<string, string> capitals = new ConcurrentDictionary<string, string>();

Task.Factory.StartNew(AddParis).Wait();
AddParis();

capitals["Russia"] = "Leningrad";
capitals.AddOrUpdate("Russia", "Moscow", (k, old) => old + " -> Moscow");
Console.WriteLine($"The capital of Russia is {capitals["Russia"]}.");

capitals["Sweden"] = "Uppsala";
var capOfSweden = capitals.GetOrAdd("Sweden", "Stockholm");
Console.WriteLine($"The capital of Sweden is {capOfSweden}");

const string toRemove = "Russia";
string removed;
var didRemove = capitals.TryRemove(toRemove, out removed);
if (didRemove)
{
    Console.WriteLine($"We just removed {removed}");
} else
{
    Console.WriteLine("Failed to remove");
}



void AddParis()
{
    bool sucess = capitals.TryAdd("France", "Paris");
    string who = Task.CurrentId.HasValue ? "Task " + Task.CurrentId : "Main thread";
    Console.WriteLine($"{who} {(sucess? "added": "did not add")} the element");
}