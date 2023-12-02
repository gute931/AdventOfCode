using System.Text;

Console.WriteLine("Uppgift 2023-12-02!");
string[] _filedata = File.ReadAllLines("./data.txt");

int _maxBlue = 14;
int _maxGreen = 13;
int _maxRed = 12;

int _sum1 = 0;
int _sum2 = 0;

StringBuilder _sb = new StringBuilder();

foreach (string _row in _filedata)
{
    bool _validGame = true;

    int _red =0; int _green=0; int _blue=0;
    string _game = _row.Split(':')[0];
    int _gameId = int.Parse(_game.Split(' ')[1]);
    string _transactions = _row.Split(':')[1];
    string[] _sets = _transactions.Split(";");
    foreach (var _set in _sets)
    {
        string[] _colors    = _set.Split(",");
        foreach (var _colorGrp in _colors)
        {
            string[] trans = _colorGrp.Trim().Split(" ");
            int _amount = int.Parse(trans[0]);
            string _color = trans[1];
            switch (_color  )
            {
                case "red":
                   _validGame = _amount > _maxRed ? false : _validGame;
                    _red= Math.Max( _amount, _red );
                    break;
                case "green":
                    _validGame = _amount > _maxGreen ? false : _validGame;
                    _green = Math.Max(_amount, _green);
                    break;
                case "blue":
                    _validGame = _amount > _maxBlue ? false : _validGame;
                    _blue = Math.Max(_amount, _blue);
                    break;
            }

        }

    }
    _sum2 += _red * _green * _blue;
    if (_validGame) _sum1 += _gameId;
}

Console.WriteLine($"S1:{_sum1}");
Console.WriteLine($"S2:{_sum2}");
Console.ReadLine();
