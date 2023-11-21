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

List<string> _head = new List<string>();
List<string> _tail = new List<string>();

ValueTuple<int?, int?> _curHead = (0, 0);
ValueTuple<int?, int?> _prevHead = (null, null);

ValueTuple<int?, int?> _curTail = (0, 0);


foreach (string _cmd in _filedata)
{
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
                break;
            case "D":
                _curHead.Item1++;
                break;
            case "L":
                _curHead.Item2--;
                break;
            case "R":
                _curHead.Item2++;
                break;
        }
        saveHeadPosition();
        moveTail(_direction);
        saveTailPosition();
    }
}

Console.WriteLine(_tail.Distinct().Count());

Console.ReadLine();

void moveTail(string direction)
{
    if (_curTail.Item1 == null)
    {
        _curTail = (0, 0); // Set origin
    }
    else
    {
        int _diffRow = Math.Abs((int)(_curHead.Item1 - _curTail.Item1));
        int _diffCol = Math.Abs((int)(_curHead.Item2 - _curTail.Item2));

        if ((_diffRow != 0 && _diffCol == 0) || (_diffRow == 0 && _diffCol != 0))
        {
            _curTail = _prevHead;
        }
        else if (_diffRow > 1 || _diffCol > 1)
        {
            _curTail = _prevHead;
        }
    }
}


void saveHeadPosition()
{
    _head.Add($"{_curHead.Item1}:{_curHead.Item2}");
}


void saveTailPosition()
{
    _tail.Add($"{_curTail.Item1}:{_curTail.Item2}");
}
