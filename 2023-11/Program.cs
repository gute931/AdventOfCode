Console.WriteLine("2023-11");
string[] rawData = File.ReadAllLines("data.txt");
int Rows = rawData.Length;
int Cols = rawData[0].Length;
int[] RowValuesS1 = new int[Rows];
int[] ColValuesS1 = new int[Cols];
int[] RowValuesS2 = new int[Rows];
int[] ColValuesS2 = new int[Cols];
SortedList<int, (int, int)> galaxies = new System.Collections.Generic.SortedList<int, (int, int)>();

// Rows to expand
for (int r = 0; r < Rows; r++)
{
    RowValuesS1[r] = rawData[r].ToArray<char>().Count(s => s == '.') == Cols ? 2 : 1;
    RowValuesS2[r] = rawData[r].ToArray<char>().Count(s => s == '.') == Cols ? 1000000 : 1;
}

// Cols to expand
for (int c = 0; c < Cols; c++)
{
    int _dots = 0;
    for (int r = 0; r < Rows; r++)
    {
        _dots += rawData[r][c] == '.' ? 1 : 0;
    }
    ColValuesS1[c] = _dots == Rows ? 2 : 1;
    ColValuesS2[c] = _dots == Rows ? 1000000 : 1;
}

// Find Galaxies
for (int r = 0; r < Rows; r++) for (int c = 0; c < Cols; c++) if (rawData[r][c] == '#') galaxies.Add(galaxies.Count, (r, c));


// Sum distances between all galaxies
long SUM1 = 0;
long SUM2 = 0;
for (int og = 0; og < galaxies.Count(); og++)
{
    for (int ig = og; ig < galaxies.Count(); ig++)
    {
        (int, int) _from = galaxies[og];
        (int, int) _to = galaxies[ig];
        int _rSum1 = 0;
        int _cSum1 = 0;
        int _rSum2 = 0;
        int _cSum2 = 0;
        for (int s = Math.Min(_from.Item1, _to.Item1); s < Math.Max(_from.Item1, _to.Item1); s++) _rSum1 += RowValuesS1[s];
        for (int s = Math.Min(_from.Item1, _to.Item1); s < Math.Max(_from.Item1, _to.Item1); s++) _rSum2 += RowValuesS2[s];
        for (int s = Math.Min(_from.Item2, _to.Item2); s < Math.Max(_from.Item2, _to.Item2); s++) _cSum1 += ColValuesS1[s];
        for (int s = Math.Min(_from.Item2, _to.Item2); s < Math.Max(_from.Item2, _to.Item2); s++) _cSum2 += ColValuesS2[s];
        // Console.WriteLine($":og:{og}:ig:{ig} - F:{_from.Item1}:{_from.Item2}, T:{_to.Item1}:{_to.Item2} ==> {_rSum + _cSum}");
        SUM1 += _rSum1 + _cSum1;
        SUM2 += _rSum2 + _cSum2;
    }
}

Console.WriteLine($"S1:{SUM1}");
Console.WriteLine($"S2:{SUM2}");
Console.WriteLine("");



