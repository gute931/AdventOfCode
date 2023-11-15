// See https://aka.ms/new-console-template for more information
Console.WriteLine("Uppgift 2022-12-01!");

string[] _rows =  File.ReadAllLines("./data.txt");

int _elf = 1;
int _calories = 0;
SortedDictionary<string, int> _elfs = new SortedDictionary<string, int>();
List<int> _caloriesList = new List<int>();

foreach (string _row in _rows)
{
    if (string.IsNullOrEmpty(_row))
    {
        _elfs.Add($"Elf_{_elf}", _calories);
        _caloriesList.Add(_calories);
        _elf++;
        _calories = 0;
    }
    else
    {
        _calories += int.Parse(_row);
    }
}

if (_calories > 0)
{
    _elfs.Add($"Elf_{_elf}", _calories);
    _caloriesList.Add(_calories);
}
int _max = _elfs.Values.Max();
Console.WriteLine($"Svar fråga 1:{_max}");

int _top3 = _caloriesList.OrderByDescending(i => i).Take(3).Sum();
Console.WriteLine($"Svar fråga 2:{_top3}");

var _r = _elfs.Where(w => w.Value == _elfs.Values.Max());
Console.ReadLine();

