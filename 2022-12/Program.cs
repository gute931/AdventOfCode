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
    for (int _c = 0; _c < GtConfig.Instance.COLS; _c++)
    {
        // _nd.Add(new GtNode2(_r, _c, _r, _c, GtConfig.Instance.Matrix[_r, _c]));
        char[] _search = new char[2];
        _search[0] = GtConfig.Instance.Matrix[_r, _c];
        if (_search[0] == 'E')
        {
            _nd.Add(new GtNode2(_r, _c, _r, _c, _search[0]));
        }
       else if (_search[0] == 'S') _search[1] = 'a';
        else if (_search[0] == 'z') _search[1] = 'E';
        else _search[1] = (char)(((int)_search[0]) + 1);

        GtNode2 nUp = GtConfig.Instance.ValidNeighbour(_r, _c, _r - 1, _c, _search);
        GtNode2 nDown = GtConfig.Instance.ValidNeighbour(_r, _c, _r + 1, _c, _search);
        GtNode2 nLeft = GtConfig.Instance.ValidNeighbour(_r, _c, _r, _c - 1, _search);
        GtNode2 nRight = GtConfig.Instance.ValidNeighbour(_r, _c, _r, _c + 1, _search);

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



