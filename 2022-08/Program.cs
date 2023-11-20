// See https://aka.ms/new-console-template for more information

using System.Net.Security;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;

Console.WriteLine("Uppgift 2022-12-08!");




string[] _filedata = File.ReadAllLines("./data.txt");
int _rowsInData = _filedata.Length;
int _colsInData = _filedata[0].Length;

byte[,] _forrest = new byte[_rowsInData, _colsInData];
string[,] _forrestResult = new string[_rowsInData, _colsInData];
string[,] _forrestDebug = new string[_rowsInData, _colsInData];

Array.Clear(_forrestResult, 0, _forrestResult.Length);
Array.Clear(_forrestDebug, 0, _forrestDebug.Length);

for (int _row = 0; _row < _filedata.Length; _row++)
{
    var _rowData = _filedata[_row].Select(ch => ch - '0').ToArray();
    for (int _col = 0; _col < _rowData.Length; _col++)
    {
        _forrest[_row, _col] = Convert.ToByte(_rowData[_col]);
    }
}

int amount = 0;

for (int _cRow = 0; _cRow < _rowsInData; _cRow++)
{
    for (int _cCol = 0; _cCol < _colsInData; _cCol++)
    {
        amount += CheckIfVisible(_forrest, _cRow, _cCol);
    }
}

StringBuilder _sb = new StringBuilder();
for (int _row = 0; _row < _rowsInData; _row++)
{
    for (int _col = 0; _col < _colsInData; _col++)
    {
        _sb.Append(_forrestDebug[_row, _col]);
    }
    _sb.AppendLine();
}

File.WriteAllText(@".\dataDebug.txt", _sb.ToString());
Console.WriteLine($"S1 : {amount}");

File.WriteAllText(@"..\..\..\dataDebug.txt", _sb.ToString());


int[,] _neighbours = new int[_rowsInData, _colsInData];
for (int _row = 0; _row < _rowsInData; _row++)
{
    for (int _col = 0; _col < _colsInData; _col++)
    {
        _neighbours[_row, _col] = checkNeighbours(_forrest, _row, _col);
    }
}
int _S2 = _neighbours.Cast<int>().ToList().Max();
Console.WriteLine($"Max view value : {_S2}");


int checkNeighbours(byte[,] forrest, int row, int col)
{
    byte _currentTree = forrest[row, col];
    int _left = 1;
    int _right = 1;
    int _down = 1;
    int _up = 1;

    // check up
    for (int _row = row-1; _row > 0; _row--)
    {
        if (forrest[_row, col] < _currentTree) _up++;
        else break;
    }

    // check left
    for (int _col = col-1; _col > 0; _col--)
    {
        if (forrest[row, _col] < _currentTree) _left++;
        else break;
    }

    // check down
    for (int _row = row + 1; _row < _rowsInData-1; _row++)
    {
        if (forrest[_row, col] < _currentTree) _down++;
        else break;
    }

    // check right
    for (int _col = col + 1; _col < _rowsInData-1; _col++)
    {
        if (forrest[row, _col] < _currentTree) _right++;
        else break;
    }

    return _up * _left * _down * _right;
}

int CheckIfVisible(byte[,] forrest, int row, int col)
{
    if (row == 0 || row == _rowsInData - 1 || col == 0 || col == _colsInData - 1)
    {
        setDebugValue(_forrestDebug, row, col, 1);
        return 1; // alla i ytterkant är synliga
    }

    // check from left
    int _cl = 1;
    for (int i = 0; i < col; i++) if (forrest[row, i] >= forrest[row, col]) { _cl = 0; break; }
    // check from right
    int _cr = 1;
    for (int i = _colsInData - 1; i > col; i--) if (forrest[row, i] >= forrest[row, col]) { _cr = 0; break; }
    // check from top
    int _ct = 1;
    for (int i = 0; i < row; i++) if (forrest[i, col] >= forrest[row, col]) { _ct = 0; break; }
    // check from bottom
    int _cb = 1;
    for (int i = _colsInData - 1; i > row; i--) if (forrest[i, col] >= forrest[row, col]) { _cb = 0; break; }
    int _r = (new[] { _cl, _cr, _ct, _cb }).Max();

    setDebugValue(_forrestDebug, row, col, _r);
    return _r;
}

void setDebugValue(string[,] _forrestDebug, int row, int col, int value)
{
    if (value > 0)
    {
        _forrestDebug[row, col] = _forrest[row, col].ToString();
    }
    else
    {
        _forrestDebug[row, col] = ".";
    }

}