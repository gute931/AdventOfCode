using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;

string searchStringF = "XMAS";
string searchStringB = "SAMX";
string[] _rawData = File.ReadAllLines("data.txt");
List<string> _allCombinations = _rawData.ToList<string>();

int _fileRows = _rawData.Length;
int _fileCols = _rawData[0].Length;
int _diagonalLoops = _fileRows + _fileCols;



/* S1 */
/* Horisontala */
for (int _c = 0; _c < _fileCols; _c++)
{
    StringBuilder _sbH = new StringBuilder();
    for (int _r = 0; _r < _fileRows; _r++)
    {
        _sbH.Append(_rawData[_r][_c]);
    }
    _allCombinations.Add(_sbH.ToString());
}


/* Diagonala */
for (int i = 0; i < _diagonalLoops; i++)
{
    List<(int row, int col)> coords = Next(i, _fileRows, _fileCols, "LR");

    StringBuilder _sbLR = new StringBuilder();
    StringBuilder _sbRL = new StringBuilder();
    foreach ((int, int) coord in coords)
    {
        _sbLR.Append(_rawData[coord.Item1][coord.Item2]);

        char[] _sa = _rawData[coord.Item1].ToCharArray();
        Array.Reverse(_sa);
        _sbRL.Append(_sa[coord.Item2]);
    }
    if (_sbLR.ToString().Length >= 4) _allCombinations.Add(_sbLR.ToString());
    if (_sbRL.ToString().Length >= 4) _allCombinations.Add(_sbRL.ToString());
}

int S1 = 0;
foreach (string _line in _allCombinations)
{
    int S = Check(_line);
    S1 += S;
    // Console.WriteLine($"tot:{S1}, f:{S}, str:{_line}");
}



Console.WriteLine($"S1 : {S1}");


// Step 2:
List<(string, string, string)> _patterns = new List<(string, string, string)>
{
    (
     "(?=(S.S))",
      "A",
     "(?=(M.M))"
     ),(
     "(?=(M.S))",
      "A",
     "(?=(M.S))"
    ),(
     "(?=(M.M))",
      "A",
     "(?=(S.S))"
     ),(
     "(?=(S.M))",
      "A",
     "(?=(S.M))"
     )
};

List<int> startPositions = new List<int>();
int S2 = 0;
foreach ((string, string, string) _pattern in _patterns)
{
    for (int i = 0; i < _fileRows - 2; i++)
    {
        MatchCollection r1 = Regex.Matches(_rawData[i], _pattern.Item1);
        MatchCollection r2 = Regex.Matches(_rawData[i + 1], _pattern.Item2);
        MatchCollection r3 = Regex.Matches(_rawData[i + 2], _pattern.Item3);

        if (r1.Count > 0 && r2.Count > 0 && r3.Count > 0)
        {
            foreach (Match m in r1)
            {
                var r2hit = r2.Count(p => p.Index == m.Index + 1);
                var r3hit = r3.Count(p => p.Index == m.Index);
                if (r2hit > 0 && r3hit > 0) S2++;
            }
        }

    }
}

Console.WriteLine($"S2 : {S2}");



int Check(string line)
{
    int S = 0;
    S += Regex.Matches(line, searchStringF).Count;
    S += Regex.Matches(line, searchStringB).Count;
    return S;
}


List<(int row, int col)> Next(int col, int rows, int cols, string dir)
{
    List<(int row, int col)> _coords = new List<(int row, int col)>();

    int idx = -1;

    int currentCol = col;

    for (int _r = 0; _r < rows; _r++)
    {

        if (currentCol >= 0 && currentCol < cols)
        {
            _coords.Add((_r, currentCol));
        }
        currentCol += idx;
    }
    return _coords;
}

