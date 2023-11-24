using _2022_11;
using System.Globalization;

Console.WriteLine("Uppgift 2022-12-11!");

string[] _filedata = File.ReadAllLines("./data.txt");
//string[] _filedata = File.ReadAllLines("./testdata.txt");


// string[] _filedata = File.ReadAllLines("./testdata.txt");

SortedDictionary<int, Monkey> _monkeys = new SortedDictionary<int, Monkey>();

Monkey _currentMonkey = null;
foreach (string _record in _filedata)
{
    string[] _parts = _record.Split(" :".ToCharArray());
    if (_parts.Length > 0 && _parts[0] == "Monkey")
    {
        _currentMonkey = new Monkey(Convert.ToInt32(_parts[1]));
        _monkeys.Add(_currentMonkey.monkeyId, _currentMonkey);
    }
    else if (_parts.Length > 0 && _currentMonkey != null)
    {
        _currentMonkey.ParseRec(_record);
    }
}

for (int i = 0; i < 1000; i++)
{
    foreach (var _monkey in _monkeys)
    {
        List<(int, int)> _results = _monkey.Value.Process();
        foreach (var _result in _results)
        {
            Monkey _receiver = _monkeys[_result.Item1];
            _receiver.addLevel(_result.Item2);
        }
    }
}

foreach (var _monkey in _monkeys)
{
    String _dlm = "";
    Console.Write($"Monkey {_monkey.Value.monkeyId}");
    foreach (var item in _monkey.Value.items)
    {
        Console.Write($"{_dlm} {item}");
        _dlm = ",";
    }
    Console.Write($", Inspections: {_monkey.Value.inspections}, InspectioTotal: {_monkey.Value.inspectionTotal}");

    Console.WriteLine();
}

int[] resultObs = _monkeys.OrderByDescending(S => S.Value.inspections).Take(2).Select(s => s.Value.inspections).ToArray();

Console.WriteLine($"S1:{resultObs[0] * resultObs[1]}");
// int _total = _monkeys.OrderByDescending(S => S.Value.inspections).Take(2).Aggregate(s => s.Value.inspections);

Console.ReadLine();
