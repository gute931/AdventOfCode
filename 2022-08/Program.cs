// See https://aka.ms/new-console-template for more information

using System.Net.Security;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;

Console.WriteLine("Uppgift 2022-12-08!");



string[] _filedata = File.ReadAllLines("./data.txt");
byte[,] _forrest = new byte[99, 99];
string[,] _forrestResult = new string[99, 99];
string[,] _forrestDebug = new string[99, 99];

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

for (int _cRow = 0; _cRow < 99; _cRow++)
{
    for (int _cCol = 0; _cCol < 99; _cCol++)
    {
        amount += CheckIfVisible(_forrest, _cRow, _cCol);
    }
}

StringBuilder _sb = new StringBuilder();
for (int _row = 0; _row < 99; _row++)
{
    for (int _col = 0; _col < 99; _col++)
    {
        _sb.Append(_forrestDebug[_row, _col]);
    }
    _sb.AppendLine();    
}

File.WriteAllText(@".\dataDebug.txt",_sb.ToString());
Console.WriteLine($"S1 : {amount}");

File.WriteAllText(@"..\..\..\dataDebug.txt", _sb.ToString());

const int SIZE = 99;
int CheckIfVisible(byte[,] forrest, int row, int col)
{
    if (row == 0 || row == SIZE - 1 || col == 0 || col == SIZE - 1)
    {
        setDebugValue(_forrestDebug, row, col, 1);
        return 1; // alla i ytterkant är synliga
    }

    // check from left
    int _cl = 1;
    for (int i = 0; i < col; i++) if (forrest[row, i] >= forrest[row, col]) { _cl = 0; break; }
    // check from right
    int _cr = 1;
    for (int i = SIZE - 1; i > col; i--) if (forrest[row, i] >= forrest[row, col]) { _cr = 0; break; }
    // check from top
    int _ct = 1;
    for (int i = 0; i < row; i++) if (forrest[i, col] >= forrest[row, col]) { _ct = 0; break; }
    // check from bottom
    int _cb = 1;
    for (int i = SIZE - 1; i > row; i--) if (forrest[i, col] >= forrest[row, col]) { _cb = 0; break; }
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