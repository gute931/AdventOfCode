using _2023_10;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Reflection.Metadata;
using System.Text;

Console.WriteLine("2023-10");



SortedList<int, GtMaze> _coordinates = new SortedList<int, GtMaze>();

_coordinates.Add(0, new GtMaze((GtConfig.Instance.STARTROW, GtConfig.Instance.STARTCOL), 'F', gtDirection.South));
_coordinates.Add(1, new GtMaze((GtConfig.Instance.STARTROW, GtConfig.Instance.STARTCOL), 'F', gtDirection.North));
_coordinates.Add(2, new GtMaze((GtConfig.Instance.STARTROW, GtConfig.Instance.STARTCOL), 'F', gtDirection.West));
_coordinates.Add(3, new GtMaze((GtConfig.Instance.STARTROW, GtConfig.Instance.STARTCOL), 'F', gtDirection.East));

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



char[] ValidSymbols = ['|', '-', 'L', 'J', '7', 'F', 'S'];
char[] NorthStop = ['-', 'F', 'L'];
char[] SouthStop = ['-', 'L', 'J'];
char[] WestStop = ['|', 'J', '7', 'F', 'L'];
char[] EastStop = ['|', 'J', '7', 'F', 'L'];
char _insideLoopChar = '1';
string _insideLoopString = "1";
char _wasteChar = ' ';

int MaxRow = GtConfig.Instance.ROWS;
int MaxCol = GtConfig.Instance.COLS;


var _longest = _coordinates.OrderByDescending(o => o.Value.Level).First();

_longest.Value.SavePath();

string[,] _winMapChar = new string[GtConfig.Instance.ROWS, GtConfig.Instance.COLS];
char[,] _winMap = new char[GtConfig.Instance.ROWS, GtConfig.Instance.COLS];

for (int _r = 0; _r < GtConfig.Instance.ROWS; _r++) for (int _c = 0; _c < GtConfig.Instance.COLS; _c++) { _winMapChar[_r, _c] = _insideLoopString; _winMap[_r, _c] = _insideLoopChar; };

foreach (Point item in _longest.Value.Path)
{
    _winMapChar[item.Row, item.Col] = item.Boxchar;
    // _winMap[item.Row, item.Col] = '0';
    _winMap[item.Row, item.Col] = item.Symbol;
    if (item.Symbol == 'S') _winMap[item.Row, item.Col] = 'F';

}

renderMapString("00_Drawing");
renderMap("00_DrawingSymbols");


mazeOuterBoundary sideOfBorder = mazeOuterBoundary.notset;

Point StartOfBorder = _longest.Value.Path.OrderBy(o => o.Row).ThenBy(o => o.Col).First();
StartOfBorder = _longest.Value.Path[_longest.Value.Path.Count - 1];

char _startSymbol = _winMap[StartOfBorder.Row, StartOfBorder.Col];
// Start in the middle row, find first border symbol, Then follow border clockwise
sideOfBorder = mazeOuterBoundary.East;
Point _prevPoint = _longest.Value.Path[0];

gtDirection _Moved = gtDirection.None;

for (int _p = 1 ; _p < _longest.Value.Path.Count; _p++)
{
    Point _point = _longest.Value.Path[_p];
    char _symbol = _winMap[_point.Row, _point.Col];
    Console.WriteLine($"Punkt:{_point.Row}:{_point.Col}: {_symbol}");

    int _diffR = _point.Row - _prevPoint.Row;
    int _diffC = _point.Col - _prevPoint.Col;

    if (_diffR == +1 && _diffC == 0) _Moved = gtDirection.South;
    if (_diffR == -1 && _diffC == 0) _Moved = gtDirection.North;
    if (_diffR == 0 && _diffC == +1) _Moved = gtDirection.West;
    if (_diffR == 0 && _diffC == -1) _Moved = gtDirection.East;

    ClearAllInADirection(_point.Row, _point.Col, sideOfBorder);

    switch (_Moved)
    {
        case gtDirection.North:
            switch (_symbol)
            {
                case 'F':
                    sideOfBorder = sideOfBorder == mazeOuterBoundary.West ? mazeOuterBoundary.South : mazeOuterBoundary.North;
                    break;
                case '7':
                    sideOfBorder = sideOfBorder == mazeOuterBoundary.West ? mazeOuterBoundary.North : mazeOuterBoundary.South;
                    break;
            }
            break;
        case gtDirection.South:
            switch (_symbol)
            {
                case 'L':
                    sideOfBorder = sideOfBorder == mazeOuterBoundary.West ? mazeOuterBoundary.North : mazeOuterBoundary.South;
                    break;
                case 'J':
                    sideOfBorder = sideOfBorder == mazeOuterBoundary.West ? mazeOuterBoundary.South : mazeOuterBoundary.North;
                    break;
            }
            break;
        case gtDirection.East:
            switch (_symbol)
            {
                case 'F':
                    sideOfBorder = sideOfBorder == mazeOuterBoundary.North ? mazeOuterBoundary.East : mazeOuterBoundary.West;
                    break;
                case 'L':
                    sideOfBorder = sideOfBorder == mazeOuterBoundary.North ? mazeOuterBoundary.West : mazeOuterBoundary.East;
                    break;
            }
            break;
        case gtDirection.West:
            switch (_symbol)
            {
                case '7':
                    sideOfBorder = sideOfBorder == mazeOuterBoundary.North ? mazeOuterBoundary.West : mazeOuterBoundary.East;
                    break;
                case 'J':
                    sideOfBorder = sideOfBorder == mazeOuterBoundary.North ? mazeOuterBoundary.East : mazeOuterBoundary.West;
                    break;
            }
            break;
        default:
            break;
    }
    Console.WriteLine($"Call clean: symbol:{_symbol}, nextDir:{_Moved}, outsideis:{sideOfBorder}");
    ClearAllInADirection(_point.Row, _point.Col, sideOfBorder);

    _prevPoint = _point;
}

renderMap("01_Clear");
renderMapString("01_ClearString");

int S2 = 0;

for (int _r = 0; _r < GtConfig.Instance.ROWS; _r++) for (int _c = 0; _c < GtConfig.Instance.COLS; _c++) S2 += _winMap[_r, _c] == _insideLoopChar ? 1 : 0;





Console.WriteLine($"S2:{S2}");


Console.WriteLine();


return;

void ClearAllInADirection(int row, int col, mazeOuterBoundary borderSide)
{
    switch (borderSide)
    {
        case mazeOuterBoundary.East:
            for (int _c = col ; _c >= 0; _c--)
            {
                if (ClearIfNotSymbol(row, _c, borderSide)) return;
            }
            break;
        case mazeOuterBoundary.West:
            for (int _c = col + 1 ; _c < MaxCol; _c++)
            {
                if (ClearIfNotSymbol(row, _c, borderSide)) return;
            }
            break;
        case mazeOuterBoundary.North:
            for (int _r = row -1 ; _r >= 0; _r--)
            {
                if (ClearIfNotSymbol(_r, col, borderSide)) return;
            }
            break;
        case mazeOuterBoundary.South:
            for (int _r = row + 1; _r < MaxRow; _r++)
            {
                if (ClearIfNotSymbol(_r, col, borderSide)) return;
            }
            break;

    }
}

bool ClearIfNotSymbol(int row, int col, mazeOuterBoundary borderSide)
{
    bool _eod = false;
    Console.WriteLine($"Clean if data: row:{row}, col:{col}, borderSide:{borderSide}, data:{_winMap[row, col]}");
    if (!ValidSymbols.Contains(_winMap[row, col]))
    {
        _winMap[row, col] = _wasteChar;
        _winMapChar[row, col] = " ";
        Console.WriteLine("Cleared!");
    }
    else
    {
        _eod = true;
        Console.WriteLine("Nothing to Clear!");
    }
    return _eod;
}





void renderMapString(string suffix)
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
