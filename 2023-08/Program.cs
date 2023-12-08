// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Net.Http.Headers;
using System.Text;

Console.WriteLine("2023-12-08");
string[] _data = File.ReadAllLines("./data.txt");

char[] instructions = _data[0].Trim().ToCharArray();
string _current = "";
SortedList<string, (string, string)> map = new SortedList<string, (string, string)>();
List<string> startStringsS1 = new List<string>();
List<string> startStringsS2 = new List<string>();
int _endsZ = 0;
for (int i = 2; i < _data.Length; i++)
{
    string[] _p = _data[i].Split(" =(,)".ToCharArray(), StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    if (i == 2) _current = _p[0];
    map.Add(_p[0], (_p[1], _p[2]));
    if (_p[0] == "AAA") startStringsS1.Add(_p[0]);
    // if (_p[0].Last() == 'A' || _p[0].Last() == 'Z') Console.WriteLine(_p[0]);
    if (_p[0].Last() == 'A') startStringsS2.Add(_p[0]);
}

StringBuilder sb = new StringBuilder();

;

int SUM1 = 0;
int SUM2 = 0;
SUM1 = GetZ(startStringsS1[0], instructions, map, true);

List<int> targets = new List<int>();
for (int i = 0; i < startStringsS2.Count(); i++)
{
    targets.Add(GetZ(startStringsS2[i], instructions, map, false));
}

for (int i = 3; i < 10000; i++)
{
    bool equal = true;
    foreach (int item in targets)
    {
        if (item % i != 0)
        {
            equal = false;
            break;
        }
    }
    if (equal)
    {

        Console.WriteLine($"%={i}");
        long _v = targets[0] * i;
        if (targets.Where(w => w * i == _v).Count() == targets.Count)
        {
            Console.WriteLine($"SUM2_{_v}");
        }
        break;
    }
}

List<long> _values = new List<long>();
int _max = targets.Max();
for (int i = 3000; i < 9999999; i++)
{
    _values.Add(_max * i);
}

foreach (var item in targets)
{
    Console.WriteLine($"--- Item : {item} ---");

    for (int i = 3000; i < 9999; i++)
    {
        long _v = item * i;
        if (_values.Contains(_v))
        {
            Console.WriteLine($"{i}:{_v}");
        }
        _values.Add(_max * i);
    }
}

StringBuilder _sb = new StringBuilder();
for (int i = 3000; i < 100000; i++)
{
    foreach (var item in targets)
    {

        _sb.AppendLine(string.Format("{0}\t{1}\t{2}", i, item, item * i));
    }
}
File.WriteAllText(@"..\..\..\trace.txt", _sb.ToString());
Console.WriteLine($"SUM1:{SUM1}");
// 102758
// 127506
Console.WriteLine($"SUM2:{SUM2}");
Console.WriteLine($"");

static int GetZ(string start, char[] instructions, SortedList<string, (string, string)> map, bool EndWithZZZ)
{
    int SUM = 0;
    bool _done = false;
    string _current = start;
    while (!_done)
    {

        foreach (var instruction in instructions)
        {
            SUM++;

            switch (instruction)
            {
                case 'L':
                    _current = map[_current].Item1;
                    break;
                case 'R':
                    _current = map[_current].Item2;
                    break;
                default:
                    Console.WriteLine("Fel tecken!");
                    break;
            }

            bool possibleEnd = (EndWithZZZ && _current == "ZZZ") || (!EndWithZZZ && _current.Last() == 'Z') ? true : false;
            if (possibleEnd)
            {
                _done = true;
                break;
            }
        }
    }
    return SUM;


}