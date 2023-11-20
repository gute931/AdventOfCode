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

for (int y = 0; y < _filedata.Length; y++)
{
    var _a = _filedata[y].Select(ch => ch - '0').ToArray();
    for (int x = 0; x < _a.Length; x++)
    {
        _forrest[x, y] = Convert.ToByte(_a[x]);
    }
}

int amount = 0;

for (int cy = 0; cy < 99; cy++)
{
    for (int cx = 0; cx < 99; cx++)
    {
        amount += CheckIfVisible(_forrest, cx, cy);
    }
}



Console.WriteLine($"S1 : {amount}");

File.WriteAllText(@".\dataDebug.txt", _forrestDebug.ToString());

const int SIZE = 99;
int CheckIfVisible(byte[,] forrest, int treeX, int treeY)
{
    if (treeX == 0 || treeX == SIZE - 1 || treeY == 0 || treeY == SIZE - 1)
    {
        setDebugValue(_forrestDebug, treeX, treeY, forrest[treeX, treeY]);
        return 1; // alla i ytterkant är synliga
    }

    // check from left
    int _cl = 1;
    for (int i = 0; i < treeX; i++) if (forrest[i, treeY] >= forrest[treeX, treeY]) { _cl = 0; break; }
    // check from right
    int _cr = 1;
    for (int i = SIZE - 1; i > treeX; i--) if (forrest[i, treeY] >= forrest[treeX, treeY]) { _cr = 0; break; }
    // check from top
    int _ct = 1;
    for (int i = 0; i < treeY; i++) if (forrest[i, treeX] >= forrest[treeX, treeY]) { _ct = 0; break; }
    // check from bottom
    int _cb = 1;
    for (int i = SIZE - 1; i > treeY; i--) if (forrest[i, treeX] >= forrest[treeX, treeY]) { _cb = 0; break; }
    int _r = (new[] { _cl, _cr, _ct, _cb }).Max();

    setDebugValue(_forrestDebug, treeX, treeY, forrest[treeX, treeY]);
    return _r;
}

void setDebugValue(string[,] _forrestDebug, int treeX, int treeY, byte value)
{
    if (value >= 0) _forrestDebug[treeX, treeY] = value.ToString();
    else _forrestDebug[treeX, treeY] = ".";

}