using _2022_09;
using System.Net.Security;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;


Console.WriteLine("Uppgift 2022-12-09!");

string[] _filedata = File.ReadAllLines("./data.txt");


int[,] _moves = new int[100, 100];

string[,] _board = new string[100, 100];

List<string> _headS1 = new List<string>();
List<string> _tailS1 = new List<string>();
List<string> _tailS2 = new List<string>();

// S1 : Declare
ValueTuple<int?, int?> _curHead = (0, 0);
ValueTuple<int?, int?> _prevHead = (0, 0);

ValueTuple<int?, int?> _curTail = (null, null);

// S1 : Declare
SortedList<int, (int?, int?)> _rope = new SortedList<int, (int?, int?)>();


_rope[0] = (0, 0);
for (int i = 1; i < 10; i++)
{
    _rope[i] = (null, null);
}

/*
for (int i = 0; i < 10; i++)
{
    _rope[i] = (null, null);
}
*/
int _rowNo = 0;

foreach (string _cmd in _filedata)
{
    _rowNo++;
    string[] _cmdParts = _cmd.Split(' ');
    string _direction = _cmdParts[0];
    int _steps = Convert.ToInt32(_cmdParts[1]);

    for (int s = 0; s < _steps; s++)
    {
        _prevHead = _curHead;

        switch (_direction)
        {
            case "U":
                _curHead.Item1--;
                _rope[0] = (_rope[0].Item1 - 1, _rope[0].Item2);
                break;
            case "D":
                _curHead.Item1++;
                _rope[0] = (_rope[0].Item1 + 1, _rope[0].Item2);
                break;
            case "L":
                _curHead.Item2--;
                _rope[0] = (_rope[0].Item1, _rope[0].Item2 - 1);
                break;
            case "R":
                _curHead.Item2++;
                _rope[0] = (_rope[0].Item1, _rope[0].Item2 + 1);
                break;
        }
        saveHeadPositionS1();
        moveTailS1(_direction);
        moveTailS2(_direction);
    }
}

Console.WriteLine(_tailS1.Distinct().Count());
// 6190 "
Console.WriteLine(_tailS2.Distinct().Count());
// 3668 Too high
Console.ReadLine();


void moveTailS1(string direction)
{
    if (_curTail.Item1 == null)
    {
        _curTail = (0, 0); // Set origin initially 
        saveTailPositionS1();
    }
    else
    {
        int _diffRow = Math.Abs((int)(_curHead.Item1 - _curTail.Item1));
        int _diffCol = Math.Abs((int)(_curHead.Item2 - _curTail.Item2));

        if (_diffRow > 1 || _diffCol > 1)
        {
            _curTail = _prevHead;
            saveTailPositionS1();
        }
    }
}

void moveTailS2(string direction)
{
    for (int i = 1; i < 10; i++)
    {
        if (_curTail.Item1 == null)
        {
            _curTail = (0, 0); // Set origin initially 
            saveTailPositionS2(0,0);
        }
        else
        {
            int _diffRow = Math.Abs((int)(_rope[i-1].Item1 - _rope[i].Item1));
            int _diffCol = Math.Abs((int)(_rope[i - 1].Item2 - _rope[i].Item2));

            if (_diffRow > 1 || _diffCol > 1)
            {
                _rope[i] = (_rope[i - 1].Item1, _rope[i - 1].Item2);
                if (i == 9) saveTailPositionS2((int)_rope[i].Item1, (int)_rope[i].Item2);
            }
        }
    }
}


void saveHeadPositionS1()
{
    _headS1.Add($"{_curHead.Item1}:{_curHead.Item2}");
}

void saveTailPositionS1()
{
    _tailS1.Add($"{_curTail.Item1}:{_curTail.Item2}");
}

void saveTailPositionS2(int row, int col)
{
    _tailS2.Add($"{row}:{col}");
}