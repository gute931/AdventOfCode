using _2023_16;
using System.Data;
using System.Text;

Console.WriteLine("2023-16");

string[] Data = File.ReadAllLines("data.txt");
char[,] Map = new char[Data[0].Length, Data.Length];
string[,] MapN = new string[Data[0].Length, Data.Length];
StringBuilder _path = new StringBuilder();
int ROWS = Data.Length;
int COLS = Data[0].Length;
char[] NavSymbols = { '|', '-', '/', '\\' };
List<(int, int, char)> startPositions = new List<(int, int, char)>();
int counter = 0;
for (int r = 0; r < ROWS; r++)
{
    for (int c = 0; c < COLS; c++)
    {
        if ((r == 0 || r == ROWS - 1 || c == 0 || c == COLS - 1) && NavSymbols.Contains(Data[r][c])) startPositions.Add((r, c, Data[r][c]));
        Map[r, c] = Data[r][c];
        MapN[r, c] = ".";
    }
}

List<(int, int, GtDirection)> History = new List<(int, int, GtDirection)>();

gtScanner(0, 0, GtDirection.East);


foreach (var item in startPositions)
{
    History = new List<(int, int, GtDirection)>();
    int steps = 0;
    GtDirection _dir = GtDirection.North;
    Array.Clear(MapN, 0, MapN.Length);
    if (item.Item1 == 0) // Top row
    {
        switch (item.Item3)
        {
            case '|':
                steps += gtScanner(item.Item1, item.Item2, GtDirection.South);
                break;
            case '-':
                steps += gtScanner(item.Item1, item.Item2, GtDirection.East);
                steps += gtScanner(item.Item1, item.Item2, GtDirection.West);
                break;
            case '/':
                steps += gtScanner(item.Item1, item.Item2, GtDirection.East);
                break;
            case '\\':
                steps += gtScanner(item.Item1, item.Item2, GtDirection.West);
                break;
        }
    }
    else if (item.Item1 == ROWS - 1)
    {
        switch (item.Item3)
        {
            case '|':
                steps += gtScanner(item.Item1, item.Item2, GtDirection.North);
                break;
            case '-':
                steps += gtScanner(item.Item1, item.Item2, GtDirection.East);
                steps += gtScanner(item.Item1, item.Item2, GtDirection.West);
                break;
            case '/':
                steps += gtScanner(item.Item1, item.Item2, GtDirection.West);
                break;
            case '\\':
                steps += gtScanner(item.Item1, item.Item2, GtDirection.East);
                break;
        }
    }
    else if (item.Item2 == 0)
    {
        switch (item.Item3)
        {
            case '|':
                steps += gtScanner(item.Item1, item.Item2, GtDirection.North);
                steps += gtScanner(item.Item1, item.Item2, GtDirection.South);
                break;
            case '-':
                steps += gtScanner(item.Item1, item.Item2, GtDirection.East);
                break;
            case '/':
                steps += gtScanner(item.Item1, item.Item2, GtDirection.North);
                break;
            case '\\':
                steps += gtScanner(item.Item1, item.Item2, GtDirection.South);
                break;
        }
    }
    else if (item.Item2 == COLS - 1)
    {
        switch (item.Item3)
        {
            case '|':
                steps += gtScanner(item.Item1, item.Item2, GtDirection.North);
                steps += gtScanner(item.Item1, item.Item2, GtDirection.South);
                break;
            case '-':
                steps += gtScanner(item.Item1, item.Item2, GtDirection.West);
                break;
            case '/':
                steps += gtScanner(item.Item1, item.Item2, GtDirection.South);
                break;
            case '\\':
                steps += gtScanner(item.Item1, item.Item2, GtDirection.North);
                break;
        }
    }
    Console.WriteLine($"s2:{steps}");
}
// 625 TL
Console.WriteLine(""  );
// File.WriteAllText(@"..\..\..\00_Path.txt", _path.ToString());

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

