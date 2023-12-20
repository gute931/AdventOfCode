using _2023_16;
using System.ComponentModel;
using System.Data;
using System.Linq.Expressions;
using System.Text;

Console.WriteLine("2023-16");

string[] Data = File.ReadAllLines("testdata.txt");
char[,] Map = new char[Data[0].Length, Data.Length];
string[,] MapN = new string[Data[0].Length, Data.Length];

for (int r = 0; r < Data.Length; r++)
{
    for (int c = 0; c < Data[r].Length; c++)
    {
        Map[r, c] = Data[r][c];
        MapN[r, c] = ".";
    }
}

Console.WriteLine("");

gtScanner(0, 0, GtDirection.East);

StringBuilder _sb = new StringBuilder();
for (int r = 0; r < MapN.GetLength(0); r++)
{
    for (int c = 0; c < MapN.GetLength(1); c++)
    {
        _sb.Append(MapN[r, c]);
    }
    _sb.AppendLine();
}
File.WriteAllText(@"..\..\..\00_map.txt", _sb.ToString());

int S1 = 0;
for (int r = 0; r < MapN.GetLength(0); r++)
{
    for (int c = 0; c < MapN.GetLength(1); c++)
    {
        S1 += MapN[r, c] == "#" ? 1 : 0;
    }
}
Console.WriteLine($"S1={S1}");

Console.WriteLine("");

void gtScanner(int row, int col, GtDirection currentDir)
{
    // Stopp om man naigerar utanför arrayen
    if (row < 0 || col < 0 || row > Map.GetLength(0) - 1 || col > Map.GetLength(1) - 1)
    {
        return;
    }
   
     char _symbol = Map[row, col];
    MapN[row, col] = "#";
    MapN[row, col] = _symbol.ToString();
    switch (_symbol)
    {
        case '.':
            switch (currentDir)
            {
                case GtDirection.North:
                    MapN[row, col] = "^";
                    break;
                case GtDirection.South:
                    MapN[row, col] = "v";
                    break;
                case GtDirection.East:
                    MapN[row, col] = ">";
                    break;
                case GtDirection.West:
                    MapN[row, col] = "<";
                    break;
            }

            switch (currentDir)
            {
                case GtDirection.North:
                    gtScanner(row - 1, col, currentDir);
                    break;
                case GtDirection.South:
                    gtScanner(row + 1, col, currentDir);
                    break;
                case GtDirection.East:
                    gtScanner(row, col + 1, currentDir);
                    break;
                case GtDirection.West:
                    gtScanner(row + 1, col - 1, currentDir);
                    break;
            }
            break;
        case '-':
            switch (currentDir)
            {
                case GtDirection.North:
                case GtDirection.South:
                    gtScanner(row, col - 1, GtDirection.West);
                    gtScanner(row, col + 1, GtDirection.East);
                    break;
                case GtDirection.East:
                    gtScanner(row, col + 1, GtDirection.East);
                    break;
                case GtDirection.West:
                    gtScanner(row, col - 1, GtDirection.West);
                    break;
            }
            break;
        case '|':
            switch (currentDir)
            {
                case GtDirection.North:
                    gtScanner(row - 1, col, GtDirection.North);
                    break;
                case GtDirection.South:
                    gtScanner(row + 1, col, GtDirection.South);
                    break;
                case GtDirection.East:
                case GtDirection.West:
                    gtScanner(row - 1, col, GtDirection.North);
                    gtScanner(row + 1, col, GtDirection.South);
                    break;
            }
            break;
        case '/':
            switch (currentDir)
            {
                case GtDirection.North:
                    gtScanner(row, col + 1, GtDirection.East);
                    break;
                case GtDirection.South:
                    gtScanner(row, col - 1, GtDirection.West);
                    break;
                case GtDirection.East:
                    gtScanner(row - 1, col, GtDirection.North);
                    break;
                case GtDirection.West:
                    gtScanner(row + 1, col, GtDirection.South);
                    break;
            }
            break;
        case '\\':
            switch (currentDir)
            {
                case GtDirection.North:
                    gtScanner(row, col - 1, GtDirection.West);
                    break;
                case GtDirection.South:
                    gtScanner(row, col + 1, GtDirection.East);
                    break;
                case GtDirection.East:
                    gtScanner(row + 1, col, GtDirection.South);
                    break;
                case GtDirection.West:
                    gtScanner(row - 1, col, GtDirection.North);
                    break;
            }
            break;
    }
    return;
}

