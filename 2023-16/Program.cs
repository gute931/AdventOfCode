using _2023_16;
using System.Text;

Console.WriteLine("2023-16");

string[] Data = File.ReadAllLines("data.txt");
char[,] Map = new char[Data[0].Length, Data.Length];
string[,] MapN = new string[Data[0].Length, Data.Length];
StringBuilder _path = new StringBuilder();

char[] NavSymbols = { '|', '-', '/', '\\' };
List<(int,int)> startPositions = new List<(int,int)> ();
int counter = 0;
for (int r = 0; r < Data.Length; r++)
{
    for (int c = 0; c < Data[r].Length; c++)
    {
        if ((r == 0 || r == Data.Length - 1 || c==0 || c == Data[0].Length) && ) continue;
        Map[r, c] = Data[r][c];
        MapN[r, c] = ".";
    }
}

List<(int, int, GtDirection)> History = new List<(int, int, GtDirection)>();

try
{
    gtScanner(0, 0, GtDirection.East);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
File.WriteAllText(@"..\..\..\00_Path.txt", _path.ToString());

void WriteMap()
{
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
}
int S1 = 0;
for (int r = 0; r < MapN.GetLength(0); r++)
{
    for (int c = 0; c < MapN.GetLength(1); c++)
    {
        S1 += MapN[r, c] == "#" ? 1 : 0;
    }
}

Console.WriteLine($"S1={S1}");
// 1638
// 7939
Console.WriteLine("");

int gtScanner(int row, int col, GtDirection currentDir)
{
    int steps = 0;
    // Stopp om man naigerar utanför arrayen
    if (row < 0 || col < 0 || row > Map.GetLength(0) - 1 || col > Map.GetLength(1) - 1)
    {
        _path.AppendLine($"C:{counter}, {row},{col}, D:{currentDir} -> End.");
        return steps;
    }


    if (History.Contains((row, col, currentDir))) return steps;

    steps++;
    counter++;
    char _symbol = Map[row, col];
    if (NavSymbols.Contains(_symbol)) History.Add((row, col, currentDir));


    MapN[row, col] = "#";

    // _path.AppendLine($"C:{counter}, {row},{col}, D:{currentDir}, S:{_symbol}");

    /*
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
    */
    switch (_symbol)
    {
        case '.':


            switch (currentDir)
            {
                case GtDirection.North:
                    steps += gtScanner(row - 1, col, currentDir);
                    break;
                case GtDirection.South:
                    steps += gtScanner(row + 1, col, currentDir);
                    break;
                case GtDirection.East:
                    steps += gtScanner(row, col + 1, currentDir);
                    break;
                case GtDirection.West:
                    steps += gtScanner(row, col - 1, currentDir);
                    break;
            }
            break;
        case '-':
            switch (currentDir)
            {
                case GtDirection.North:
                    steps += gtScanner(row, col - 1, GtDirection.West);
                    steps += gtScanner(row, col + 1, GtDirection.East);
                    break;
                case GtDirection.South:
                    steps += gtScanner(row, col - 1, GtDirection.West);
                    steps += gtScanner(row, col + 1, GtDirection.East);
                    break;
                case GtDirection.East:
                    steps += gtScanner(row, col + 1, GtDirection.East);
                    break;
                case GtDirection.West:
                    steps += gtScanner(row, col - 1, GtDirection.West);
                    break;
            }
            break;
        case '|':
            switch (currentDir)
            {
                case GtDirection.North:

                    steps += gtScanner(row - 1, col, GtDirection.North);
                    break;
                case GtDirection.South:
                    steps += gtScanner(row + 1, col, GtDirection.South);
                    break;
                case GtDirection.East:
                    steps += gtScanner(row - 1, col, GtDirection.North);
                    steps += gtScanner(row + 1, col, GtDirection.South);
                    break;
                case GtDirection.West:
                    steps += gtScanner(row - 1, col, GtDirection.North);
                    steps += gtScanner(row + 1, col, GtDirection.South);
                    break;
            }
            break;
        case '/':
            switch (currentDir)
            {
                case GtDirection.North:
                    steps += gtScanner(row, col + 1, GtDirection.East);
                    break;
                case GtDirection.South:
                    steps += gtScanner(row, col - 1, GtDirection.West);
                    break;
                case GtDirection.East:
                    steps += gtScanner(row - 1, col, GtDirection.North);
                    break;
                case GtDirection.West:
                    steps += gtScanner(row + 1, col, GtDirection.South);
                    break;
            }
            break;
        case '\\':
            switch (currentDir)
            {
                case GtDirection.North:
                    steps += gtScanner(row, col - 1, GtDirection.West);
                    break;
                case GtDirection.South:
                    steps += gtScanner(row, col + 1, GtDirection.East);

                    break;
                case GtDirection.East:
                    steps += gtScanner(row + 1, col, GtDirection.South);
                    break;
                case GtDirection.West:
                    steps += gtScanner(row - 1, col, GtDirection.North);
                    break;
            }
            break;
    }
    return steps;
}

