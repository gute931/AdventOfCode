using _2022_12;
using Dijkstra.NET.Graph;
using Dijkstra.NET.ShortestPath;


using System.Diagnostics;
using System.Numerics;
using System.Xml.Linq;

Console.WriteLine("Uppgift 2022-12-12!");

Console.WriteLine($"Rows:{GtConfig.Instance.ROWS}, Cols:{GtConfig.Instance.COLS}");
Console.WriteLine($"StartRow:{GtConfig.Instance.STARTROW}, STARTCOL:{GtConfig.Instance.STARTCOL}");
/*
List<(string, GtNode)> _nodes = new List<(string, GtNode)>();

List<string> localPath = new List<string>();

_nodes.Add(($"{GtConfig.Instance.STARTROW}:{GtConfig.Instance.STARTCOL}", new GtNode(GtConfig.Instance.STARTROW, GtConfig.Instance.STARTCOL, 0, localPath)));
*/

var graph = new Graph<int, string>();
Dictionary<uint, List<uint>> _neighBoudList = new Dictionary<uint, List<uint>>();

int _nodeR = 0;
for (uint _r = 0; _r < GtConfig.Instance.ROWS; _r++)
{
    int _nodeC = 0;
    for (uint _c = 0; _c < GtConfig.Instance.COLS; _c++)
    {
/*         1rr ccc; */
        int _node = 10000000 + (_r * 10000) + _c;
        char _currentLetter = GtConfig.Instance.Matrix[_r, _c];
        List<uint> neighb = new List<uint>();
        GtConfig.Instance.AddNeighrbourIfValid(neighb, _currentLetter, _r + 1, _c + 0);
        GtConfig.Instance.AddNeighrbourIfValid(neighb, _currentLetter, _r - 1, _c - 0);
        GtConfig.Instance.AddNeighrbourIfValid(neighb, _currentLetter, _r + 0, _c + 1);
        GtConfig.Instance.AddNeighrbourIfValid(neighb, _currentLetter, _r - 0, _c - 1);
        if (neighb.Count > 0) _neighBoudList.Add(_node, neighb);
    }
}
/*
for (int _r = 0; _r < GtConfig.Instance.ROWS; _r++)
{
    int _nodeC = 0;
    for (int _c = 0; _c < GtConfig.Instance.COLS; _c++)
    {
        // int _node = (_nodeR * 1000) + _nodeC;
        int _node = 10000000 + (_nodeR * 10000) + _nodeC;

        _nodeC++;
    }
    _nodeR++;
}
*/

foreach (var item in _neighBoudList)
{
    if (item.Key == 20000)
    {
        Console.WriteLine();
    }
    graph.AddNode((int)item.Key);
}

foreach (var item in _neighBoudList)
{
    if (item.Key == 20000)
    {
        Console.WriteLine();
    }
    foreach (var _v in item.Value)
    {
 
        graph.Connect((uint)item.Key, (uint)_v, 1, $"{item.Key}:{_v}");
    }
}
ShortestPathResult result2 = graph.Dijkstra(1, 10);

ShortestPathResult result = graph.Dijkstra(
    (uint)((GtConfig.Instance.STARTROW * 1000) + GtConfig.Instance.STARTCOL),
    (uint)((GtConfig.Instance.ENDROW * 1000) + GtConfig.Instance.ENDCOL)); //result contains the shortest path

for (int _r = 0; _r < GtConfig.Instance.ROWS; _r++)
{
    int _nodeC = 0;
    for (int _c = 0; _c < GtConfig.Instance.COLS; _c++)
    {
        int _node = 10000000 + (_nodeR * 10000) + _nodeC;
        // int _node = (_nodeR * 1000) + _nodeC;
        _node++;
        graph.AddNode(_node);
    }
}



Console.ReadLine();



