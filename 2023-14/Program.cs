using _2023_14;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

Console.WriteLine("2023-14");
List<string> Data = File.ReadAllLines("testdata.txt").ToList();

Console.WriteLine($"S1:{AocStep1(Data)}");
//Data = File.ReadAllLines("testdata.txt").ToList();

/*
List<string> DataTest = new List<string>(Data);
File.WriteAllLines($"..\\..\\..\\00_trace_{0}.txt", DataTest);
DataTest = RotateList(DataTest);
DataTest = moveItems(DataTest, MoveDirection.Right);
DataTest = RotateList(DataTest);
File.WriteAllLines($"..\\..\\..\\00_trace_1_north.txt", DataTest);
DataTest = moveItems(DataTest, MoveDirection.Left);
File.WriteAllLines($"..\\..\\..\\00_trace_1_west.txt", DataTest);

DataTest = RotateList(DataTest);
DataTest = moveItems(DataTest, MoveDirection.Left);
DataTest = RotateList(DataTest);
File.WriteAllLines($"..\\..\\..\\00_trace_1_south.txt", DataTest);
DataTest = moveItems(DataTest, MoveDirection.Right);
File.WriteAllLines($"..\\..\\..\\00_trace_1_east.txt", DataTest);

*/


SortedList<string, int> DataKeys = new SortedList<string, int>();

for (int moves = 1; moves <= 1000000000; moves++)
{

    foreach (MoveOrientation orientation in Enum.GetValues(typeof(MoveOrientation)))
    {
        Data = RotateList(Data);
        switch (orientation)
        {
            case MoveOrientation.North:
            case MoveOrientation.East:
                Data = moveItems(Data, MoveDirection.Right);
                break;
            case MoveOrientation.South:
            case MoveOrientation.West:
                Data = moveItems(Data, MoveDirection.Left);
                break;
        }
    }

    if (CalcPoints(Data) == 64)
    {
        Console.WriteLine("");
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
        DataKeys.Add(_dataStr, moves);
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
