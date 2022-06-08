// See https://aka.ms/new-console-template for more information

var test1 = false;
var test2 = true;
var test3 = false;
var total = new bool[] { test1, test2, test3 };

var result = !(test1 && test2 && test3) && (test1 ^ test2 ^ test3);

//Console.WriteLine(result);

//Console.WriteLine(total.Count(b => b));
