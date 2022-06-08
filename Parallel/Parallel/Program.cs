/*
Task.Factory.StartNew(() => MyClass.Write('.'));

var t = new Task(() => MyClass.Write('?'));
t.Start();

MyClass.Write('-');
*/

/*
Task t = new Task(MyClass.Write, "hello");
t.Start();

Task.Factory.StartNew(MyClass.Write, 123);
*/

string text1 = "Super teste", text2 = "Outro";

var task1 = new Task<int>(MyClass.TextLength, text1);
task1.Start();

Task<int> task2 = Task.Factory.StartNew(MyClass.TextLength, text2);

Console.WriteLine($"Length '{text1}' is {task1.Result}");
Console.WriteLine($"Length '{text2}' is {task2.Result}");

Console.WriteLine("Finished!");
Console.ReadKey();

public static class MyClass
{

    public static void Write(char c)
    {
        int i = 1000;
        while (i-- > 0)
        {
            Console.Write(c);
        }
    }
    public static void Write(object o)
    {
        int i = 1000;
        while (i-- > 0)
        {
            Console.Write(o);
        }
    }

    public static int TextLength(object o)
    {
        Console.WriteLine($"\nTask with id {Task.CurrentId} processing object {o}...");
        return o.ToString().Length;
    }

}