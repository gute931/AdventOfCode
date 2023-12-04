using System.Text;

Console.WriteLine("Uppgift 2023-12-04!");
string[] _filedata = File.ReadAllLines("./testdata.txt");
int[] _s2counter = new int[_filedata.Length];
Array.Fill(_s2counter, 0);
int SUM1 = 0;
int SUM2 = 0;
foreach (string _rec in _filedata)
{
    string[] _parts = _rec.Split(":|".ToCharArray(), StringSplitOptions.TrimEntries);
    int _card = int.Parse(_parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]);
    string[] _myNumbers = _parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    string[] _winningNumbers = _parts[2].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var _m = _myNumbers.Intersect(_winningNumbers).Count();
    // Step 1
    int _p = 0;
    for (int i = 0; i < _m; i++)
    {
        if (i == 0) _p++;
        else _p *= 2;
    }
    SUM1 += _p;
    // Step 2
    _s2counter[_card - 1] += 1; // for current card
    for (int i = 0; i < _m; i++)
    {
        _s2counter[_card + i] += 1; // for current card
    }
    SUM2 = _s2counter.Sum();
Console.WriteLine($"S1:{SUM1}");
Console.WriteLine($"S2:{SUM2}");
Console.WriteLine();