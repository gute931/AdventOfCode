using _2023_03;
using System.Text;
using System.Text.RegularExpressions;

Console.WriteLine("Uppgift 2023-12-03!");
string[] _filedata = File.ReadAllLines("./data.txt");

int COLS = 0;
int ROWS = _filedata.Length;

//scan for max length
foreach (string _row in _filedata) COLS = Math.Max(COLS, _row.Length);

int _value = 0;
int SUM1 = 0;
int SUM2 = 0;
int _currentPos = 0;

List<gtNumber> _numbers = new List<gtNumber>();
for (int _r = 0; _r < ROWS; _r++)
{
    _currentPos = 0;
    string[] _split = Regex.Split(_filedata[_r], @"\D+");
    // Console.WriteLine(string.Join(" ", _split));
    foreach (string _item in _split)
    {
        string _items = _item.Trim();
        if (string.IsNullOrEmpty(_items)) continue;
        int _startPos = _filedata[_r].IndexOf(_items, _currentPos);

        _currentPos = _startPos + _items.Length;
        _numbers.Add(new gtNumber(_r, _startPos, _currentPos-1, _items));
    }
}






for (int _r = 0; _r < ROWS; _r++)
{
    _currentPos = 0;
    while (_currentPos <= COLS)
    {

        int _startPos = _filedata[_r].IndexOf('*', _currentPos);
        if (_startPos == -1) break;
        _currentPos = _startPos + 1;
        List<gtNumber> _hits = new List<gtNumber>();
        _hits.AddRange(_numbers.Where(w => w.Row == _r - 1 && _startPos >= w.ColStart - 1 && _startPos <= w.ColEnd + 1).ToList<gtNumber>());
        _hits.AddRange(_numbers.Where(w => w.Row == _r + 1 && _startPos >= w.ColStart - 1 && _startPos <= w.ColEnd + 1).ToList<gtNumber>());
        _hits.AddRange(_numbers.Where(w => w.Row == _r && w.ColEnd == _startPos - 1).ToList<gtNumber>());   
        _hits.AddRange(_numbers.Where(w => w.Row == _r && w.ColStart == _startPos + 1).ToList<gtNumber>());
        if (_hits.Count() == 2)
        {
            SUM2 += _hits[0].Number * _hits[1].Number;
        }
    }

}


for (int _r = 0; _r < ROWS; _r++)
{
    _currentPos = 0;
    string[] _split = Regex.Split(_filedata[_r], @"\D+");
    foreach (string _item in _split)
    {
        bool _valid = false;
        if (int.TryParse(_item, out _value)) // Integer ?
        {
            // Console.WriteLine($"-- {_item} ---");
            int _startPos = _filedata[_r].IndexOf(_item, _currentPos);
            _currentPos = _startPos + _item.Length;
            // SortedList<(int, int), char> _map = new SortedList<(int, int), char>();
            for (int _cr = -1; _cr <= 1; _cr++)
            {
                for (int _cc = _startPos - 1; _cc < Math.Min(_startPos + _item.Length + 1, COLS); _cc++)
                {
                    if (_cr + _r >= 0 && _cr + _r < ROWS && _cc >= 0 && _cc <= COLS)
                    {
                        // Console.Write(_filedata[_cr + _r][_cc]);
                        if (_cr == 0 && _cc >= _startPos && _cc < _startPos + _item.Length) continue;

                        // Console.WriteLine($"Item:{_item}, c:{_filedata[_cr+_r][_cc]}, pos:{_cr+_r}-{_cc}");
                        if (_filedata[_cr + _r][_cc] != '.') _valid = true;

                    }
                }
                Console.WriteLine();

            }
            // Console.WriteLine($"--- {_valid} ---");
            if (_valid) SUM1 += _value;
        }
    }
}
Console.WriteLine($"SUM1:{SUM1}");
Console.WriteLine($"SUM2:{SUM2}");
Console.ReadLine();