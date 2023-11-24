using System.Net.Security;

Console.WriteLine("Uppgift 2022-12-10!");

//string[] _filedata = File.ReadAllLines("./data.txt");
string[] _filedata = File.ReadAllLines("./testdata.txt");

int CYCLES_S1 = 20;
int CYCLES_S2 = 20;
int _cycleCntS1 = 1;
int _cycleCntS2 = 0;
int _step = 1;
int X = 1;
SortedList<int, int> _signal = new SortedList<int, int>();
string[,] _spriteArray = new string[6, 40];
int s2row = 0;

int _spritePos = 1;
foreach (string _file in _filedata)
{
    string[] _parts = _file.Split(' ');
    switch (_parts[0])
    {
        case "addx":
            // S2:
            if (CYCLES_S2 <= _cycleCntS2)
            {
                s2row++;
                _cycleCntS2 = 40 - _cycleCntS2;
                CYCLES_S2 += 40;
            }
            int _start = _cycleCntS2+1;
            int _end = (_cycleCntS2) + 3;
            if (X >= _cycleCntS2 && X <= (_cycleCntS2) + 3)
            {
                spritePen(_spritePos, _cycleCntS2, "#");
            }
            else
            {
                spritePen(_spritePos, _cycleCntS2, ".");
            }
            _cycleCntS2 += 2;


            _cycleCntS1 += 2;
            if (CYCLES_S1 < _cycleCntS1)
            {
                CYCLES_S1 = SaveValues(CYCLES_S1, X);
            }




            X += Convert.ToInt32(_parts[1]);
            _spritePos = X;
            break;
        default:
            _cycleCntS1++;
            _cycleCntS2++;
            spritePen(_spritePos, _cycleCntS2, ".");
            break;
    }
}
Console.WriteLine($"CYCLES: {CYCLES_S1}, _cycleCnt: {_cycleCntS1}");
Console.WriteLine($"{_signal.Where(w => w.Key <= 220).Sum(s => s.Value)}");

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

int SaveValues(int cycle, int signal)
{
    _signal.Add(cycle, (int)cycle * signal);
    //_signal.Add(cycle, signal);
    return cycle += 40;
}

void spritePen(int spritePos, int x, string symbol)
{
    if (s2row >= 6) return;

    _spriteArray[s2row, x] = symbol;
    _spriteArray[s2row, x + 1] = symbol;
}
