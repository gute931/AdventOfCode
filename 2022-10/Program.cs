Console.WriteLine("Uppgift 2022-12-10!");

string[] _filedata = File.ReadAllLines("./data.txt");

// string[] _filedata = File.ReadAllLines("./testdata.txt");

int CYCLES_S1 = 20;
int _cycleCnt_S1 = 1;
int X = 1;
SortedList<int, int> _signal_S1 = new SortedList<int, int>();

string[,] _spriteArray = new string[6, 40];

int s2_row = 1;
int s2_col = 1;
int _dataRow = 0;
int _spritePos = 1;
foreach (string _file in _filedata)
{
    _dataRow++;
    string[] _parts = _file.Split(' ');
    switch (_parts[0])
    {
        case "addx":
            // S2:
            // if (s2col == 40) spritePen(".", _file, "fill");
            for (int i = 0; i < 2; i++)
            {
                if (s2_col >= X && s2_col <= (X + 2)) spritePen_S2("#", _file, "addx");
                else spritePen_S2(".", _file, "addx");
            }

            _cycleCnt_S1 += 2;
            if (CYCLES_S1 < _cycleCnt_S1)
            {
                CYCLES_S1 = SaveValues_S1(CYCLES_S1, X);
            }

            X += Convert.ToInt32(_parts[1]);

            _spritePos = X;
            break;
        default:
            _cycleCnt_S1++;
            if (s2_col >= X && s2_col <= (X + 2)) spritePen_S2("#", _file, "noop");
            else spritePen_S2(".", _file, "noop");
            /*
            spritePen_S2(".", _file, "noop");
            */
            break;
    }

}

Console.WriteLine($"CYCLES: {CYCLES_S1}, _cycleCnt: {_cycleCnt_S1}");
Console.WriteLine($"{_signal_S1.Where(w => w.Key <= 220).Sum(s => s.Value)}");


// Render S2
for (int r = 0; r < 6; r++)
{
    for (int c = 0; c < 40; c++)
    {
        Console.Write(_spriteArray[r, c]);
    }
    Console.WriteLine();
}
Console.WriteLine();

Console.ReadLine();

int SaveValues_S1(int cycle, int signal)
{
    _signal_S1.Add(cycle, (int)cycle * signal);
    //_signal.Add(cycle, signal);
    return cycle += 40;
}

void spritePen_S2(string symbol, string trans, string transtyp)
{
    if (s2_row > 6) return;

    Console.WriteLine($"dataRow:{_dataRow}, s2row: {s2_row}, s2col=X: {s2_col}={X}:{X + 2}, trans={trans}, Symbol={symbol}, tt={transtyp}");
    _spriteArray[s2_row - 1, s2_col - 1] = symbol;

    if (s2_col == 40)
    {
        s2_row++;
        s2_col = 0;
    }

    s2_col++;

}
