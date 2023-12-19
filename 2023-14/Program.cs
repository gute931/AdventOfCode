using _2023_14;
using Microsoft.VisualBasic;
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

List<string> DataTest = new List<string>(Data);

SortedList<string, int> DataKeys = new SortedList<string, int>();
int _r = 0;
int interval = -1;
StringBuilder _sb = new StringBuilder();
for (int moves = 1; moves <= 1000000000; moves++)
{

    foreach (MoveOrientation orientation in Enum.GetValues(typeof(MoveOrientation)))
    {
        _r++;
        switch (orientation)
        {
            case MoveOrientation.North:
                Data = new List<string>(RotateList(Data));
                Data = moveItems(Data, MoveDirection.Right);
                Data = new List<string>(RotateList(Data));
                break;
            case MoveOrientation.West:
                Data = moveItems(Data, MoveDirection.Right);
                break;
            case MoveOrientation.South:
                Data = new List<string>(RotateList(Data));
                Data = moveItems(Data, MoveDirection.Left);
                Data = new List<string>(RotateList(Data));
                break;
            case MoveOrientation.East:
                Data = moveItems(Data, MoveDirection.Left);
                break;
        }

        //  File.WriteAllLines($"..\\..\\..\\00_trace_{_r}_{orientation}.txt", Data);
    }

    if (CalcPoints(Data) == 64)
    {
        Console.WriteLine("64");
    }



    string _dataStr = string.Join("", Data.ToArray());

    // _sb.AppendLine(_dataStr );
    if (moves == 100)
    {
        File.WriteAllText($"..\\..\\..\\10_records.txt", _sb.ToString());
    }
    if (interval == -1)
    {
        if (DataKeys.ContainsKey(_dataStr))
        {
            int first = DataKeys[_dataStr];
            interval = moves - first;
            Console.WriteLine($"f:{first}, c:{moves}, interval:{moves - first}");
            /*
            double miljard = 1000000000;
            double sture = miljard / Convert.ToDouble(moves);
            double _Rest = miljard - first % moves + first;
            // Data = moveItems(Data, MoveDirection.Right);
            Console.WriteLine($"Match move:{moves}");
            */
            /*
            Console.WriteLine($"S2:{CalcPoints(Data)}");
            DataTest = RotateList(Data);
            DataTest = moveItems(DataTest, MoveDirection.Right);
            DataTest = RotateList(DataTest);

            Console.WriteLine($"S2:{CalcPoints(DataTest)}");
            */
            int multi = (1000000000 - first) / interval;
            Console.WriteLine("");
            moves = multi * interval;
        }
        else
        {
            DataKeys.Add(_dataStr, moves);
        }
    }
    else
    {
        Console.WriteLine($"*.... move:{moves} ....*");
        Console.WriteLine($"S2:{CalcPoints(Data)}");
        DataTest = RotateList(Data);
        DataTest = moveItems(DataTest, MoveDirection.Right);
        DataTest = RotateList(DataTest);

        Console.WriteLine($"S2:{CalcPoints(DataTest)}");

    }
}
Console.WriteLine($"S2:{CalcPoints(Data)}");
Data = RotateList(Data);
Data = moveItems(Data, MoveDirection.Left);
Console.WriteLine($"S2:{CalcPoints(Data)}");



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
    List<string> Moved = moveItems(Pivot, MoveDirection.Right);
    int p1 = CalcPoints(Moved);
    List<string> Moved2 = RotateList(Moved);
    int p2 = CalcPoints(Moved2);

    return CalcPoints(RotateList(moveItems(RotateList(Data), MoveDirection.Right)));

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
    data.Reverse();
    for (Int32 r = 1; r <= data.Count ; r++)
    {
        for (int i = 0; i < data[r-1].Length; i++)
        {
            points += data[r - 1][i] == 'O' ? 1 * i : 0;
        }
    }

    return points;
}
