using _2023_14;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

Console.WriteLine("2023-14");
List<string> Data = File.ReadAllLines("testdata.txt").ToList();

Console.WriteLine($"S1:{AocStep1(Data)}");
//Data = File.ReadAllLines("testdata.txt").ToList();

SortedList<string, int> DataKeys = new SortedList<string, int>();

for (int moves = 1; moves <= 1000000000; moves++)
{
    foreach (MoveOrientation orientation in Enum.GetValues(typeof(MoveOrientation)))
    {
        List<string> pivot = RotateList(Data);
        List<string> result = new List<string>();
        MoveDirection _dir = MoveDirection.Right;
        switch (orientation)
        {
            case MoveOrientation.North:
            case MoveOrientation.East:
                _dir = MoveDirection.Right;
                result = moveItems(pivot, _dir);
                if (moves == 1 && orientation==MoveOrientation.North) Console.WriteLine($"S1:_{orientation}_{AocStep1(Data)}");

                break;
            case MoveOrientation.South:
            case MoveOrientation.West:
                _dir = MoveDirection.Left;
                result = moveItems(pivot, _dir);
                break;
        }
        Data = new List<string>(result);
        if (moves <= 3)
        {
            File.WriteAllLines($"..\\..\\..\\00_traceR{moves}_{orientation}.txt", Data);
        }
    }


    string _dataStr = string.Join("", Data.ToArray());
    if (DataKeys.ContainsKey(_dataStr))
    {
        int first = DataKeys[_dataStr];
        Console.WriteLine($"{first}");
        double miljard = 1000000000;
        double sture = miljard / Convert.ToDouble(moves);
        int multi = 1000000000 / moves;
        double _Rest = miljard - first % moves + first;
        // Data = moveItems(Data, MoveDirection.Right);
        Console.WriteLine($"Match move:{moves}");
        Console.WriteLine($"S2:{CalcPoints(Data)}");

        Console.WriteLine("");
        moves = multi * moves;
    }
    else
    {
        DataKeys.Add( _dataStr, moves);
    }
}
Console.WriteLine($"S2:{CalcPoints(Data)}");
Data = RotateList(Data);
Data = moveItems(Data, MoveDirection.Left);
Console.WriteLine($"S2:{CalcPoints(Data)}");
long _hashCode = Data.GetHashCode();



List<string> moveItems(List<string> inList, MoveDirection moveDir)
{
    List<string> outList = new List<string>();
    string _to = "";
    foreach (var Row in inList)
    {
        bool _done = false;
        string _from = Row;
        while (!_done)
        {
            _to = moveDir == MoveDirection.Right ? _from.Replace(".O", "O.") : _from.Replace("O.", ".O");
            _done = _from == _to ? true : false;
            _from = _to;
        }
        outList.Add(_to);
    }
    return outList;
}


List<string> RotateList(List<string> list)
{
    List<string> col2row = new List<string>();
    for (int c = 0; c < list[0].Length; c++)
    {
        StringBuilder _sb = new StringBuilder();
        for (int ri = 0; ri < list.Count(); ri++)
        {
            // Console.WriteLine($"r:{ri};c:{c}");
            _sb.Append(list[ri][c]);
        }
        col2row.Add(_sb.ToString());
    }
    return col2row;
}




long AocStep1(List<string> data)
{
    // S1:
    List<string> Pivot = RotateList(Data);
    // CalcPoints(moveItems(Pivot, MoveDirection.Right));
    return CalcPoints(moveItems(Pivot, MoveDirection.Right));

    List<string> _result = new List<string>();
    string _to = "";
    long points = 0;

    foreach (var Row in Pivot)
    {
        bool _done = false;
        string _from = Row;
        while (!_done)
        {
            _to = _from.Replace(".O", "O.");
            _done = _from == _to ? true : false;
            _from = _to;
        }
        _result.Add(_to);

        Console.WriteLine($"{_to}");
    }
    return CalcPoints(_result);
    /*
    foreach (var row in _result)
    {
        string _reversed = new string(row.Reverse().ToArray());

        for (int i = 1; i <= _reversed.Length; i++)
        {
            points += _reversed[i - 1] == 'O' ? 1 * i : 0;
        }
    }

    return points;
    */
}

int CalcPoints(List<string> data)
{
    int points = 0;
    foreach (var row in data)
    {
        string _reversed = new string(row.Reverse().ToArray());

        for (int i = 1; i <= _reversed.Length; i++)
        {
            points += _reversed[i - 1] == 'O' ? 1 * i : 0;
        }
    }

    return points;
}
