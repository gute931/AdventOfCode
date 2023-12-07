using _2022_12;
using System.Reflection.Emit;
using Dijkstra.NET.Graph;
using Dijkstra.NET.ShortestPath;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Metrics;
using System.Xml.Linq;
using System;


Console.WriteLine("Uppgift 2022-12-12!");

char[] PATH = ['S', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'E'];

var graph = new Graph<uint, string>();
int startKey = 0;
int endKey = 0;

string[] FILEDATA = File.ReadAllLines("./testdata.txt");

int ROWS = FILEDATA.Length;
int COLS = 0;

for (int _r = 0; _r < FILEDATA.Length; _r++)
{
    COLS = Math.Max(COLS, FILEDATA[_r].Length);

    for (int _c = 0; _c < FILEDATA[_r].Length; _c++)
    {
        Console.WriteLine($"{_r}, {_c}, {GetKeyN(_r, _c)}");
        if (FILEDATA[_r][_c] == 'S') startKey = GetKeyN(_r, _c);
        if (FILEDATA[_r][_c] == 'E') endKey = GetKeyN(_r, _c);
        graph.AddNode((uint)GetKeyN(_r, _c));
        // Console.WriteLine($"KA: {GetKeyN(_r, _c)}");

        if (validNeighbour(_r + 1, _c, FILEDATA[_r][_c]))
        {
            Console.WriteLine($"{GetKey(_r, _c)}-{GetKey(_r + 1, _c)}");
            graph.Connect((uint)GetKeyN(_r, _c), (uint)GetKeyN(_r + 1, _c), 1, $"{GetKey(_r, _c)}-{GetKey(_r + 1, _c)}");
        }
        if (validNeighbour(_r - 1, _c, FILEDATA[_r][_c]))
        {
            Console.WriteLine($"{GetKey(_r, _c)}-{GetKey(_r - 1, _c)}");
            graph.Connect((uint)GetKeyN(_r, _c), (uint)GetKeyN(_r - 1, _c), 1, $"{GetKey(_r, _c)}-{GetKey(_r - 1, _c)}");
        }
        if (validNeighbour(_r, _c + 1, FILEDATA[_r][_c]))
        {
            Console.WriteLine($"{GetKey(_r, _c)}-{GetKey(_r, _c + 1)}");
            graph.Connect((uint)GetKeyN(_r, _c), (uint)GetKeyN(_r, _c + 1), 1, $"{GetKey(_r, _c)}-{GetKey(_r, _c + 1)}");
        }
        if (validNeighbour(_r, _c - 1, FILEDATA[_r][_c]))
        {
            Console.WriteLine($"{GetKey(_r, _c)}-{GetKey(_r, _c - 1)}");
            graph.Connect((uint)GetKeyN(_r, _c), (uint)GetKeyN(_r, _c - 1), 1, $"{GetKey(_r, _c)}-{GetKey(_r, _c - 1)}");
        }
    }
}

ShortestPathResult result = graph.Dijkstra((uint)startKey, (uint)endKey);
Console.WriteLine(result.ToString());

bool validNeighbour(int row, int col, char Letter)
{
    if (Letter == 'E') return false;
    if (row > 0 && col > 0 && row < ROWS && col < COLS)
    {
        int LetterNo = Array.IndexOf(PATH, Letter);
        char _cChar = FILEDATA[row][col];
        if (PATH[LetterNo] == _cChar || PATH[LetterNo + 1] == _cChar)
        {

            return true;
        }
    }
    return false;
};



string GetKey(int row, int col) { return $"{row}:{col}"; }
int GetKeyN(int row, int col) { return ((COLS * row) + col); }





Console.WriteLine($"Rows:{GtConfig.Instance.ROWS}, Cols:{GtConfig.Instance.COLS}");
Console.WriteLine($"StartRow:{GtConfig.Instance.STARTROW}, STARTCOL:{GtConfig.Instance.STARTCOL}");


/*
List<string> localPath = new List<string>();
localPath.Add($"{GtConfig.Instance.STARTROW}:{GtConfig.Instance.STARTCOL}");
GtNode  _startNode = new GtNode(GtConfig.Instance.STARTROW, GtConfig.Instance.STARTCOL, 'a', localPath, 1);

int _antal = _startNode.GetEnd(1);
*/

int Level = 0;
List<string> localPath = new List<string>();
localPath.Add($"{GtConfig.Instance.STARTROW}:{GtConfig.Instance.STARTCOL}");

try
{
    GtNode2 _root = new GtNode2(GtConfig.Instance.STARTROW, GtConfig.Instance.STARTCOL, Level, 0, localPath);
    _root.Search();

}
catch (Exception E)
{

    throw;
}
// Console.WriteLine($"SUM1:{_count}");

Console.WriteLine("End!");
Console.ReadLine();



