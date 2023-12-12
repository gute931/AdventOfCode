using _2023_10;
using System.Net.Security;
using System.Reflection.Metadata;
using System.Text;

Console.WriteLine("2023-10");



SortedList<int, GtCoordinate> _coordinates = new SortedList<int, GtCoordinate>();

_coordinates.Add(0, new GtCoordinate((GtConfig.Instance.STARTROW, GtConfig.Instance.STARTCOL), 'S', Direction.South));
_coordinates.Add(1, new GtCoordinate((GtConfig.Instance.STARTROW, GtConfig.Instance.STARTCOL), 'S', Direction.North));
_coordinates.Add(2, new GtCoordinate((GtConfig.Instance.STARTROW, GtConfig.Instance.STARTCOL), 'S', Direction.West));
_coordinates.Add(3, new GtCoordinate((GtConfig.Instance.STARTROW, GtConfig.Instance.STARTCOL), 'S', Direction.East));

int max = 0;
for (int n = 0; n < _coordinates.Count(); n++)
{
    for (int i = 0; i < 99999; i++)
    {
        if (_coordinates[_coordinates.Keys[n]].Status == status.ontrack)
        {
            _coordinates[_coordinates.Keys[n]].Step();
        }
    }

    if (_coordinates[_coordinates.Keys[n]].Status == status.Start)
    {
        max = Math.Max(max, _coordinates[_coordinates.Keys[n]].Level);
        Console.WriteLine($"Max:{max}");
    }
}
Console.WriteLine($"S1:{max / 2}");

var _longest = _coordinates.OrderByDescending(o => o.Value.Level).First();

string[,] _winMapChar = new string[GtConfig.Instance.ROWS, GtConfig.Instance.COLS];
char[,] _winMap = new char[GtConfig.Instance.ROWS, GtConfig.Instance.COLS];

for (int _r = 0; _r < GtConfig.Instance.ROWS; _r++) for (int _c = 0; _c < GtConfig.Instance.COLS; _c++) { _winMapChar[_r, _c] = "1"; _winMap[_r, _c] = '1'; };

foreach (Point item in _longest.Value.Path)
{
    _winMapChar[item.Row, item.Col] = item.Boxchar;
    _winMap[item.Row, item.Col] = '0';
}

renderMapChar("Drawing");

for (int _r = 0; _r < GtConfig.Instance.ROWS; _r++)
{
    for (int _c = 0; _c < GtConfig.Instance.COLS; _c++)
    {
        if (_winMap[_r, _c] == '0') break;
        else _winMap[_r, _c] = '.';
    }

    for (int _c = GtConfig.Instance.COLS - 1; _c >= 0; _c--)
    {
        if (_winMap[_r, _c] == '0') break;
        else _winMap[_r, _c] = '.';
    }
}

for (int _c = 0; _c < GtConfig.Instance.COLS; _c++)
{
    for (int _r = 0; _r < GtConfig.Instance.ROWS; _r++)
    {
        if (_winMap[_r, _c] == '0') break;
        else _winMap[_r, _c] = '.';
    }
    for (int _r = GtConfig.Instance.ROWS - 1; _r >= 0; _r--)
    {
        if (_winMap[_r, _c] == '0') break;
        else _winMap[_r, _c] = '.';
    }
}

renderMap("HorizontalDone");


for (int x = 0; x < 10; x++)
{
    for (int _r = 1; _r < GtConfig.Instance.ROWS - 1; _r++)
    {
        for (int _c = 1; _c < GtConfig.Instance.COLS - 1; _c++)
        {
            if (_winMap[_r, _c] == '.')
            {
                int Cleaned = 0;
                Cleaned += clean(_winMap, _r - 1, _c - 1);
                Cleaned += clean(_winMap, _r - 1, _c);
                Cleaned += clean(_winMap, _r - 1, _c + 1);
                Cleaned += clean(_winMap, _r, _c - 1);
                Cleaned += clean(_winMap, _r, _c + 1);
                Cleaned += clean(_winMap, _r + 1, _c - 1);
                Cleaned += clean(_winMap, _r + 1, _c);
                Cleaned += clean(_winMap, _r + 1, _c + 1);
          
            }
        }
    }
}


renderMap("FinalWash");


for (int _r = 1; _r < GtConfig.Instance.ROWS - 1; _r++)
{
    int _borders = 0;
    for (int _c = 1; _c < GtConfig.Instance.COLS - 1; _c++)
    {
        switch (_winMap[_r, _c])
        {
            case '0':
                {
                    _borders++;
                    break;
                }
            case '1':
                {
                    if (int.IsEvenInteger(_borders)) _winMap[_r, _c] = 'X';
                }
                break;
        }
    }
}

renderMap("ODD");

int clean(char[,] map, int row, int col)
{
    int clean = _winMap[row, col] == '1' ? 1 : 0;
    _winMap[row, col] = clean == 1 ? '.' : _winMap[row, col];
    return clean;
}


int S2 = 0;

for (int _r = 0; _r < GtConfig.Instance.ROWS; _r++) for (int _c = 0; _c < GtConfig.Instance.COLS; _c++) S2 += _winMap[_r, _c] == '1' ? 1 : 0;




Console.WriteLine($"S2:{S2}");


Console.WriteLine();

void renderMapChar(string suffix)
{
    StringBuilder _sb = new StringBuilder();
    for (int _r = 0; _r < GtConfig.Instance.ROWS; _r++)
    {
        for (int _c = 0; _c < GtConfig.Instance.COLS; _c++) _sb.Append($"{_winMapChar[_r, _c]}");
        _sb.AppendLine();
    }

    File.WriteAllText($"..\\..\\..\\Map_{suffix}.txt", _sb.ToString());
}

void renderMap(string suffix)
{
    StringBuilder _sb = new StringBuilder();
    for (int _r = 0; _r < GtConfig.Instance.ROWS; _r++)
    {
        for (int _c = 0; _c < GtConfig.Instance.COLS; _c++) _sb.Append($"{_winMap[_r, _c]}");
        _sb.AppendLine();
    }

    File.WriteAllText($"..\\..\\..\\Map_{suffix}.txt", _sb.ToString());
}
