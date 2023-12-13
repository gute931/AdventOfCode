using _2023_10;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Net.Security;
using System.Reflection.Metadata;
using System.Text;

Console.WriteLine("2023-10");



SortedList<int, GtCoordinate> _coordinates = new SortedList<int, GtCoordinate>();

_coordinates.Add(0, new GtCoordinate((GtConfig.Instance.STARTROW, GtConfig.Instance.STARTCOL), 'S', gtDirection.South));
_coordinates.Add(1, new GtCoordinate((GtConfig.Instance.STARTROW, GtConfig.Instance.STARTCOL), 'S', gtDirection.North));
_coordinates.Add(2, new GtCoordinate((GtConfig.Instance.STARTROW, GtConfig.Instance.STARTCOL), 'S', gtDirection.West));
_coordinates.Add(3, new GtCoordinate((GtConfig.Instance.STARTROW, GtConfig.Instance.STARTCOL), 'S', gtDirection.East));

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

char[] ValidSymbols = { '|', '-', 'L', 'J', '7', 'F', 'S' };
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

GtCoordinate _longest.Value.Path.OrderBy(o => o.Row).ThenBy(o => o.Col).First();


// Start in the middle row, find first border symbol, Then follow border clockwise
(int, int) StartOfBorder = (0, 0);


mazeOuterBoundary sideOfBorder = mazeOuterBoundary.notset;
Direction _nextMove = Direction.None;
for (int _c = 0; _c < MaxCol; _c++)
{
    if (ValidSymbols.Contains(_winMap[Math.Abs(MaxRow), _c]))
    {
        StartOfBorder = (Math.Abs(MaxRow), _c);
        switch (_winMap[Math.Abs(MaxRow), _c])
        {
            case '|':
                sideOfBorder = mazeOuterBoundary.East;
                _nextMove = Direction.North;
                break;
            case 'L':
            case 'F':
                sideOfBorder = mazeOuterBoundary.East;
                _nextMove = Direction.West;
                break;
            default:
                Console.WriteLine("Should not end up here!");
                break;
        }
    }

}

int _bRow = StartOfBorder.Item1;
int _bCol = StartOfBorder.Item2;
bool _end = false;

while (_end)
{
    switch (_nextMove)
    {
        case Direction.North:
            _bRow--;
            break;
        case Direction.South:
            _bRow++;
            break;
        case Direction.East:
            _bCol--;
            break;
        case Direction.West:
            _bCol++;
            break;
    }




}

void ClearAllInADirection(int row, int col, mazeOuterBoundary borderSide)
{
    switch (borderSide)
    {
        case mazeOuterBoundary.East:
            for (int _c = col; _c >= 0; _c--)
            {
                if (!ValidSymbols.Contains(_winMap[row, _c])) _winMap[row, _c] = _wasteChar;
                else return;
            }
            break;
        case mazeOuterBoundary.West:
            for (int _c = col; _c < MaxCol; _c++)
            {
                if (!ValidSymbols.Contains(_winMap[row, _c])) _winMap[row, _c] = _wasteChar;
                else return;
            }
            break;
        case mazeOuterBoundary.North:
            for (int _r = row; _r >= 0; _r--)
            {
                if (!ValidSymbols.Contains(_winMap[_r, col])) _winMap[_r, col] = _wasteChar;
                else return;
            }
            break;
        case mazeOuterBoundary.South:
            for (int _r = row; _r < MaxCol; _r++)
            {
                if (!ValidSymbols.Contains(_winMap[_r, col])) _winMap[_r, col] = _wasteChar;
                else return;
            }
            break;

    }
}

renderMapChar("00_Drawing");
renderMap("00_DrawingSymbols");



int[,] _checked = new int[MaxRow, MaxCol];
for (int _r = 0; _r < GtConfig.Instance.ROWS; _r++)
{
    for (int _c = 0; _c < GtConfig.Instance.COLS; _c++)
    {
        _checked[_r, _c] = -1;
    }
}
int _checkNo = 0;

char[] ValidSymbols = { '|', '-', 'L', 'J', '7', 'F', 'S' };
char[] NorthStop = ['-', 'F', 'L'];
char[] SouthStop = ['-', 'L', 'J'];
char[] WestStop = ['|', 'J', '7', 'F', 'L'];
char[] EastStop = ['|', 'J', '7', 'F', 'L'];

checkNclear(0, -1, gtDirection.West);
checkNclear(-1, 0, gtDirection.South);
for (int _r = 0; _r < MaxRow; _r++)
{
    checkNclear(_r, 0, gtDirection.West);
    checkNclear(_r, MaxCol, gtDirection.East);
}
for (int _c = 0; _c < MaxCol; _c++)
{
    checkNclear(0, _c, gtDirection.South);
    checkNclear(MaxRow, _c, gtDirection.North);
}



renderMap("05_search");

int S2 = 0;

for (int _r = 0; _r < GtConfig.Instance.ROWS; _r++) for (int _c = 0; _c < GtConfig.Instance.COLS; _c++) S2 += _winMap[_r, _c] == _insideLoopChar ? 1 : 0;



Console.WriteLine($"S2:{S2}");



for (int _r = 0; _r < GtConfig.Instance.ROWS; _r++)
{
    for (int _c = 0; _c < GtConfig.Instance.COLS; _c++)
    {
        if (ValidSymbols.Contains(_winMap[_r, _c])) break;
        else _winMap[_r, _c] = _wasteChar;
    }

    for (int _c = GtConfig.Instance.COLS - 1; _c >= 0; _c--)
    {
        if (ValidSymbols.Contains(_winMap[_r, _c])) break;
        else _winMap[_r, _c] = _wasteChar;
    }
}

renderMap("10_OuterEdgeLoop1");

for (int _c = 0; _c < GtConfig.Instance.COLS; _c++)
{
    for (int _r = 0; _r < GtConfig.Instance.ROWS; _r++)
    {
        if (ValidSymbols.Contains(_winMap[_r, _c])) break;
        else _winMap[_r, _c] = _wasteChar;
    }
    for (int _r = GtConfig.Instance.ROWS - 1; _r >= 0; _r--)
    {
        if (ValidSymbols.Contains(_winMap[_r, _c])) break;
        else _winMap[_r, _c] = _wasteChar;
    }
}


renderMap("11_OuterEdgeLoop2");

for (int x = 0; x < 15; x++)
{
    for (int _r = 1; _r < GtConfig.Instance.ROWS - 1; _r++)
    {
        for (int _c = 1; _c < GtConfig.Instance.COLS - 1; _c++)
        {
            if (_winMap[_r, _c] == _wasteChar)
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



renderMap("20_CleanNeightbourAroundASpace");


for (int _r = 0; _r < GtConfig.Instance.ROWS; _r++)
{
    for (int _c = 0; _c < GtConfig.Instance.COLS; _c++)
    {
        if (_winMap[_r, _c] == _insideLoopChar)
        {
            // InsideLoop(_r, _c);
        }
    }
}



bool inVertical = false;

for (int _r = 0; _r < GtConfig.Instance.ROWS; _r++)
{
    bool inHorizontal = false;
    for (int _c = 0; _c < GtConfig.Instance.COLS; _c++)
    {
        char _symbol = _winMap[_r, _c];
        switch (_symbol)
        {
            case '|':
                inHorizontal = !inHorizontal;
                break;
            case '-':
                // inVertical = !inVertical;
                break;
            case '7':
            case 'J':
            case 'F':
            case 'L':
            case 'S':
                inHorizontal = !inHorizontal;
                inVertical = !inVertical;
                break;
            case '1':
                if (!inHorizontal || !inVertical) _winMap[_r, _c] = _wasteChar;
                break;
            default:
                break;
        }
    }
}

renderMap("60_InsideOfSymbol");

renderChecked("06_CheckOrder");





/*
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
*/

renderMap("Done");
/*
int S2 = 0;

for (int _r = 0; _r < GtConfig.Instance.ROWS; _r++) for (int _c = 0; _c < GtConfig.Instance.COLS; _c++) S2 += _winMap[_r, _c] == _insideLoopChar ? 1 : 0;
*/


Console.WriteLine($"S2:{S2}");


Console.WriteLine();



void checkNclear(int row, int col, gtDirection dir)
{
    switch (dir)
    {
        case gtDirection.North:
            row -= 1;
            break;
        case gtDirection.South:
            row += 1;
            break;
        case gtDirection.East:
            col -= 1;
            break;
        case gtDirection.West:
            col += 1;
            break;
    }

    if (row < 0 || col < 0 || row >= MaxRow || col >= MaxCol) return;

    if (_checked[row, col] >= 0) return;
    _checkNo++;
    _checked[row, col] = _checkNo;

    char _symbol = _winMap[row, col];
    switch (dir)
    {
        case gtDirection.North:
            if (NorthStop.Contains(_symbol)) return;
            if (!ValidSymbols.Contains(_symbol)) _winMap[row, col] = _wasteChar;
            checkNclear(row, col, gtDirection.North);
            checkNclear(row, col, gtDirection.West);
            checkNclear(row, col, gtDirection.East);
            break;
        case gtDirection.South:
            if (SouthStop.Contains(_symbol)) return;
            if (!ValidSymbols.Contains(_symbol)) _winMap[row, col] = _wasteChar;
            checkNclear(row, col, gtDirection.South);
            checkNclear(row, col, gtDirection.West);
            checkNclear(row, col, gtDirection.East);
            break;
        case gtDirection.East:
            if (EastStop.Contains(_symbol)) return;
            if (!ValidSymbols.Contains(_symbol)) _winMap[row, col] = _wasteChar;
            checkNclear(row, col, gtDirection.East);
            checkNclear(row, col, gtDirection.South);
            checkNclear(row, col, gtDirection.North);
            break;
        case gtDirection.West:
            if (WestStop.Contains(_symbol)) return;
            if (!ValidSymbols.Contains(_symbol)) _winMap[row, col] = _wasteChar;
            checkNclear(row, col, gtDirection.West);
            checkNclear(row, col, gtDirection.South);
            checkNclear(row, col, gtDirection.North);
            break;
        default:
            break;
    }
}


(bool, bool) borderItem(int y, int x)
{
    bool _x = false;
    bool _y = false;
    char _symbol = _winMap[y, x];
    switch (_symbol)
    {
        case '|':
            _x = true;
            break;
        case '-':
            _y = true;
            break;
        case '7':
        case 'J':
        case 'F':
        case 'L':
        case 'S':
            _x = true;
            _y = true;
            break;
    }
    return (_x, _y);
}

int clean(char[,] map, int row, int col)
{
    int clean = !ValidSymbols.Contains(_winMap[row, col]) ? 1 : 0;
    _winMap[row, col] = clean == 1 ? _wasteChar : _winMap[row, col];
    return clean;
}


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

void renderChecked(string suffix)
{
    StringBuilder _sb = new StringBuilder();
    for (int _r = 0; _r < GtConfig.Instance.ROWS; _r++)
    {
        _sb.AppendLine();
        for (int _c = 0; _c < GtConfig.Instance.COLS; _c++) _sb.AppendFormat("{0,4:D4} ", _checked[_r, _c]);
        _sb.AppendLine();
        _sb.AppendLine();
    }

    File.WriteAllText($"..\\..\\..\\Map_{suffix}.txt", _sb.ToString());
}
