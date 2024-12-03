// See https://aka.ms/new-console-template for more information
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.Arm;
using System.Text.RegularExpressions;
using System.Windows.Markup;
using System.Xml;


string[] rows = File.ReadAllLines("..\\..\\..\\data.txt");
int safeCntS1 = 0;
int safeCntS2 = 0;
foreach (string row in rows)
{
    List<int> values = row.Split(' ').Select(Int32.Parse).ToList();

    if (checkSerie(values, -1)) safeCntS1++;

    int _t=0;
    for (int i = 0; i <= values.Count() - 1; i++)
    {
        if (checkSerie(values, i)) _t++;
    }
    if (_t > 0) safeCntS2++;
}


Console.WriteLine($"S!: {safeCntS1}");
Console.WriteLine($"S2: {safeCntS2}");

Console.ReadLine();

bool checkSerie(List<int> inValues, int skip) {
    List<int> values = new List<int>(inValues);
    if (skip >= 0) values.RemoveAt(skip);
 
    bool _pos = false;
    bool _neg = false;
    bool _eq  = false;

    for (int i = 0; i < values.Count() - 1; i++)
    {
        int _diff = values[i+1] - values[i];

        if (_diff > 0 && !_pos) _pos = true;
        else if (_diff < 0 && !_neg) _neg = true;
        else if (_diff == 0 && !! !_eq) _eq = true;

        if (Math.Abs(_diff) > 3 || _eq ||  _pos == _neg) {
            return false;
        }
    }   
    return true;
}