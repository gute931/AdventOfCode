using System.Net.Security;

Console.WriteLine("Uppgift 2022-12-10!");

//string[] _filedata = File.ReadAllLines("./data.txt");
string[] _filedata = File.ReadAllLines("./testdata.txt");

int CYCLES = 20;
int _cycleCnt = 1;
int _step = 1;
int X = 1;
SortedList<int, int> _signal = new SortedList<int, int>();
SortedList<int, string> _sprite = new SortedList<int, string>();
for (int i = 1; i <= 240; i++) _sprite.Add(i, "X");
int _spritePos = 1;
foreach (string _file in _filedata)
{
    string[] _parts = _file.Split(' ');
    switch (_parts[0])
    {
        case "addx":
            _cycleCnt += 2;
            if (CYCLES < _cycleCnt)
            {
                CYCLES = SaveValues(CYCLES, X);
            }

            // S2:
            if (X == _spritePos) spritePen(_spritePos,2, "#");
            else spritePen(_spritePos, 2, ".");


            X += Convert.ToInt32(_parts[1]);
            _spritePos = X;
            break;
        default:
            _cycleCnt++;
            spritePen(_spritePos, 1, ".");
            break;
    }
}
Console.WriteLine($"CYCLES: {CYCLES}, _cycleCnt: {_cycleCnt}");
Console.WriteLine($"{_signal.Where(w=>w.Key <= 220).Sum(s => s.Value)}");

for (int i = 1; i <= 240; i++)
{
    Console.Write(_sprite[i]);
    if (i % 40 == 0) Console.WriteLine();
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
    _sprite[spritePos] = symbol;
    _sprite[spritePos+1] = symbol;
}
