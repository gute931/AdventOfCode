// See https://aka.ms/new-console-template for more information
using System.Runtime.Intrinsics.Arm;
using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");

string[] rows = File.ReadAllLines("..\\..\\..\\data_s1.txt");


List<int> lv = new List<int>();
List<int> rv = new List<int>();

foreach (string row in rows)
{
    
    lv.Add(Convert.ToInt32(row.Substring(0,5)));
    rv.Add(Convert.ToInt32(row.Substring(8,5)));
}

lv.Sort();
rv.Sort();

int diff = 0 ;

for (int i = 0; i < lv.Count(); i++)
{
    diff += Math.Abs(lv[i] - rv[i]);
}

// S2

int Score = 0;

for (int i = 0; i < lv.Count(); i++)
{
    Score += rv.Where(w=>w == lv[i]).Sum();
}

Console.WriteLine($"S1:{diff}" );
Console.WriteLine($"S2:{Score}" );

Console.ReadLine();