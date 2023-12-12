using _2023_10;

Console.WriteLine("2023-10");



SortedList<int, GtCoordinate> _coordinates = new SortedList<int, GtCoordinate>();

// _coordinates.Add(0, new GtCoordinate((GtConfig.Instance.STARTROW, GtConfig.Instance.STARTCOL), 'S', Direction.South));// 
// _coordinates.Add(1, new GtCoordinate((GtConfig.Instance.STARTROW, GtConfig.Instance.STARTCOL), 'S', Direction.North));
_coordinates.Add(2, new GtCoordinate((GtConfig.Instance.STARTROW, GtConfig.Instance.STARTCOL), 'S', Direction.West));
// _coordinates.Add(3, new GtCoordinate((GtConfig.Instance.STARTROW, GtConfig.Instance.STARTCOL), 'S', Direction.East));

for (int i = 0; i < 9; i++)
{
    for (int n = 0; n < _coordinates.Count(); n++)
    {
        if (_coordinates[_coordinates.Keys[i]].Status == status.ontrack) {
            _coordinates[_coordinates.Keys[i]].Step();
        }
    }
    var kalle = _coordinates.GroupBy(w => w.Value.compstr);
    Console.WriteLine($"*--- {i} ---*");
    foreach (var item in kalle)
    {
        Console.WriteLine($"n:{item.Count()}");
    }

}

// Console.WriteLine($"Rows:{ROWS}, Columns:{COLS}");
// Console.WriteLine($"StartRows:{_startCoord.Row}, StartCol:{_startCoord.Col}");
// Console.WriteLine($"# valid coorinats:{Coordinates.Count()}");

Console.WriteLine();

int FindNext(int row, int col, int level)
{
    if (level > 100000000)
    {
        Console.WriteLine("Max!");
    }
    /*
    List<Coordinate> Pointers = Coordinates.Where(d => d.Row == row && d.Col == col).ToList();
    foreach (var item in Pointers)
    {
        if (item.StartintPoint)
        {
            Console.WriteLine($"Startpoint:{level}");
            return level;
        }
        else FindNext(item.Row, item.Col, level++);
    }
    */
    return -1;
}