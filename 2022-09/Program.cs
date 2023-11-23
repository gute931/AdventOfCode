Console.WriteLine("Uppgift 2022-12-09!");

// string[] _filedata = File.ReadAllLines("./data.txt");
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
SortedList<int, (int, int)> _rope = new SortedList<int, (int, int)>();
SortedList<int, (int, int)> _ropePrev = new SortedList<int, (int, int)>();
_rope[0] = (0, 0);
ValueTuple<int, int> _ropeHeadPrev = (0, 0);
/*
for (int i = 1; i < 10; i++)
{
    _rope[i] = (null, null);
}
*/
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
        _ropePrev = new SortedList<int, (int, int)>(_rope);
        _prevHead = _curHead;
        _ropeHeadPrev = _rope[0];

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
        moveTailS2(_direction, _rowNo);

        // renderMap(_cmd, _rowNo, s + 1, _steps);
        // Thread.Sleep(50); 

    }

    // if (_rope.Count == 10) saveTailPositionS2((int)_rope[9].Item1, (int)_rope[9].Item2);
}

Console.WriteLine(_tailS1.Distinct().Count());
Console.WriteLine(_tailS2.Distinct().Count());
Console.ReadLine();



void moveTailS2(string direction, int iterations)
{
    if (_rope.Count < 10)
    {
        _rope[_rope.Count] = (0, 0); // Sätt in en bit till av repet.
    }

    for (int i = 1; i < _rope.Count; i++)
    {
        int _diffRow = (_rope[i - 1].Item1 - _rope[i].Item1);
        int _diffCol = (_rope[i - 1].Item2 - _rope[i].Item2);

        if (Math.Abs(_diffRow) == 2 && Math.Abs(_diffCol) == 2)
        {
            int _moveRow = _diffRow == -2 ? -1 : 1;
            int _moveCol = _diffCol == -2 ? -1 : 1;
            _rope[i] = (_rope[i].Item1 + _moveRow, _rope[i].Item2 + _moveCol);
        }
        else if (Math.Abs(_diffRow) == 2)
        {
            _rope[i] = (_rope[i].Item1 + (_diffRow == -2 ? -1 : 1), _rope[i].Item2);
            if (Math.Abs(_diffCol) == 1)
            {
                _rope[i] = (_rope[i].Item1, _rope[i].Item2 + (_diffCol == -1 ? -1 : 1));
            }
        }
        else if (Math.Abs(_diffCol) == 2)
        {
            _rope[i] = (_rope[i].Item1, _rope[i].Item2 + (_diffCol == -2 ? -1 : 1));
            if (Math.Abs(_diffRow) == 1)
            {
                _rope[i] = (_rope[i].Item1 + (_diffRow == -1 ? -1 : 1), _rope[i].Item2);
            }
        }

        if (i == 9) saveTailPositionS2((int)_rope[i].Item1, (int)_rope[i].Item2);
    }
}

void renderMap(string cmd, int row, int step, int _steps)
{
    Console.CursorVisible = false;
    Console.Clear();
    for (int r = -15; r < 15; r++)
    {
        for (int c = -15; c < 15; c++)
        {
            string _sign = ".";
            if (r == 0 && c == 0) _sign = "s";
            for (int i = 0; i < _rope.Count; i++)
            {
                if (_rope[i].Item1 == r && _rope[i].Item2 == c)
                {
                    _sign = (i == 0) ? "H" : i.ToString();
                    break;
                }
            }
            Console.Write($"{_sign} ");
        }
        Console.WriteLine();

    }
    Console.WriteLine("------------------------------------");
    Console.WriteLine($"Row: {row}, Oper:{cmd}, step:{step}/{_steps}");
    Console.WriteLine("------------------------------------");
    Console.WriteLine("--- Tail stats                   ---");
    Console.WriteLine($"Row: {_tailS2.Count}, Unique#:{_tailS2.Distinct().Count()}");
    Console.WriteLine("------------------------------------");
    for (int i = 0; i < _rope.Count; i++)
    {
        Console.WriteLine($"{i}: row:{_rope[i].Item1}, col:{_rope[i].Item2}");
    }
    Console.CursorVisible = true;
}

void saveHeadPositionS1()
{
    _headS1.Add($"{_curHead.Item1}:{_curHead.Item2}");
}


void saveTailPositionS2(int row, int col)
{
    _tailS2.Add($"{row}:{col}");
}




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

void saveTailPositionS1()
{
    _tailS1.Add($"{_curTail.Item1}:{_curTail.Item2}");
}
