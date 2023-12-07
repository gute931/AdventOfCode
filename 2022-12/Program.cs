using _2022_12;

Console.WriteLine("Uppgift 2022-12-12!");

Console.WriteLine($"Rows:{GtConfig.Instance.ROWS}, Cols:{GtConfig.Instance.COLS}");
Console.WriteLine($"StartRow:{GtConfig.Instance.STARTROW}, STARTCOL:{GtConfig.Instance.STARTCOL}");

/*
List<string> localPath = new List<string>();
localPath.Add($"{GtConfig.Instance.STARTROW}:{GtConfig.Instance.STARTCOL}");
GtNode  _startNode = new GtNode(GtConfig.Instance.STARTROW, GtConfig.Instance.STARTCOL, 'a', localPath, 1);

int _antal = _startNode.GetEnd(1);
*/
List<GtNode2> _nd = new List<GtNode2>();

for (int _r = 0; _r < GtConfig.Instance.ROWS; _r++)
{
    GtNode2 _root = new GtNode2(GtConfig.Instance.STARTROW, GtConfig.Instance.STARTCOL, Level, 0, localPath);
_root.Search();

        if (nUp != null) _nd.Add(nUp);
        if (nDown != null) _nd.Add(nDown);
        if (nLeft != null) _nd.Add(nLeft);
        if (nRight != null) _nd.Add(nRight);
    }
}

GtNode2 _startNode = _nd.Where(n => n.Letter == 'S').FirstOrDefault();
GtNode2 _endNode = _nd.Where(n => n.Letter == 'E').FirstOrDefault();
bool _loop = true;
List<GtNode2> _startNodes = new List<GtNode2>() { _startNode };


int _count = 0;

while (_loop)
{
    _count++;
    List<GtNode2> _neigh = new List<GtNode2>();
    foreach (GtNode2 _Snode in _startNodes)
    {
        GtNode2[] _rn = _nd.Where(w => (w.Letter == _Snode.Letter || w.Letter == _Snode.NextLetter) && w.CurrentRow == _Snode.NeighbourRow && w.CurrentColumn == _Snode.NeighbourColumn).ToArray();
        _neigh.AddRange(_rn);
    }
    foreach (var item in _neigh)
    {
        Console.WriteLine(item.Letter);
        if (item.Letter == 'E') break;

    }
    _startNodes = new List<GtNode2>(_neigh);
}

Console.WriteLine($"SUM1:{_count}");

Console.WriteLine("End!");
Console.ReadLine();



