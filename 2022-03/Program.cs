// See https://aka.ms/new-console-template for more information
Console.WriteLine("Uppgift 2022-12-03!");


string[] _rows = File.ReadAllLines("./data.txt");
string[,] _rs = new string[_rows.Length, 2];
for (int i = 0; i < _rows.Length; i++)
{
    int _rLength = _rows[i].Length / 2;
    _rs[i, 0] = _rows[i].Substring(0, _rLength).Trim();
    _rs[i, 1] = _rows[i].Substring(_rLength, _rLength).Trim();
}

SortedDictionary<char, int> _charValues = new SortedDictionary<char, int>();

char[] _lowerAZ = Enumerable.Range('a', 'z' - 'a' + 1).Select(i => (Char)i).ToArray();
char[] _upperAZ = Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (Char)i).ToArray();
for (int i = 0; i < _lowerAZ.Length; i++)
{
    _charValues.Add(_lowerAZ[i], i + 1);
}
for (int i = 0; i < _upperAZ.Length; i++)
{
    _charValues.Add(_upperAZ[i], i + 27);
}

int _pointsS1 = 0;
for (int i = 0; i < _rs.Length / 2; i++)
{
    char[] _distinct = _rs[i, 0].Distinct().ToArray();
    // foreach (char _item in _rs[i, 0])
    foreach (char _item in _distinct)
    {
        if (_rs[i, 1].Contains(_item))
        {
            int _point = _charValues[_item];
            _pointsS1 += _point;
            Console.WriteLine($"Rucksack : {i + 1}, item : {_item}, prio : {_point} ");
        }
    }
}

int _pointsS2 = 0;
for (int i = 0; i < _rows.Length; i+=3)
{
    Console.WriteLine("Rucksack # : {i}");
    char[] _distinct = _rows[i].Distinct().ToArray();
    // foreach (char _item in _rs[i, 0])
    foreach (char _item in _distinct)
    {
        if (_rows[i+1].Contains(_item) && _rows[i+2].Contains(_item))
        {
            int _point = _charValues[_item];
            _pointsS2 += _point;
            Console.WriteLine($"Rucksack : {i + 1}, item : {_item}, prio : {_point} ");
        }
    }
}

Console.WriteLine($"Points S1:{_pointsS1}");

Console.WriteLine($"Points S2:{_pointsS2}");




Console.ReadLine();