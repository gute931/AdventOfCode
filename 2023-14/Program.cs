using _2023_14;
using System.Text;

Console.WriteLine("2023-14");
List<string> Data = File.ReadAllLines("data.txt").ToList();

Console.WriteLine($"S1:{AocStep1(Data)}");
Console.WriteLine($"S2:{AocStep2(Data)}");
Console.ReadLine();


long AocStep2(List<string> data)
{
    SortedList<string, int> DataKeys = new SortedList<string, int>();

    int interval = -1;
    StringBuilder _sb = new StringBuilder();
    for (int moves = 1; moves <= 1000000000; moves++)
    {
        int _r = 0;
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

        string _dataStr = string.Join("", Data.ToArray());

        if (interval == -1)
        {
            if (DataKeys.ContainsKey(_dataStr))
            {
                int first = DataKeys[_dataStr];
                interval = moves - first;
                Console.WriteLine($"f:{first}, c:{moves}, interval:{moves - first}");

                moves = (((1000000000 - first) / interval) * interval) + first;
            }
            else
            {
                DataKeys.Add(_dataStr, moves);
            }
        }
    }

    // 95254
    return CalcPoints(Data);
}


long AocStep1(List<string> data)
{
    // S1:
    return CalcPoints(RotateList(moveItems(RotateList(Data), MoveDirection.Right)));
}

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




int CalcPoints(List<string> data)
{
    int points = 0;
    int value = Data.Count();
    for (Int32 r = 0; r < data.Count ; r++)
    {
        for (int i = 0; i < data[r].Length; i++)
        {
            points += data[r][i] == 'O' ? 1 * value : 0;
        }
        value--;
    }

    return points;
}
