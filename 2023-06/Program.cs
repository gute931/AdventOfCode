using System.Diagnostics;

Console.WriteLine("Uppgift 2023-12-05!");

string[] _filedata = File.ReadAllLines("./data.txt");

int[] _timeItems = _filedata[0].Substring(10).Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToArray();
int[] _distanceItems = _filedata[0].Substring(10).Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToArray();
/*
SortedList<int, (int, int)> _raceLimits = new SortedList<int, (int, int)>();
_raceLimits.Add(0, (0, 99));
_raceLimits.Add(1, (4, 11));
_raceLimits.Add(2, (11, 19));
_raceLimits.Add(2, (11, 19));
*/

int SUM1 = 1;
int _ltFrom = 0;
int _ltTo = 99;


for (int _race = 0; _race < _timeItems.Length; _race++)
{
    int _raceLength = _timeItems[_race];
    int _distanceRecord = _distanceItems[_race];
    int _s = 0;

    Console.WriteLine($"R:{_race}, _ltFrom:{_ltFrom}, _ltTo:{_ltTo} ");
    for (int _t = _ltFrom; _t <= _ltTo; _t++)
    {
        int _distance = CalculateDistance(_raceLength, _t);
        if (_distance > _distanceRecord) _s++;
        Console.WriteLine($"R:{_race}, RL:{_raceLength}, LT:{_t}, D:{_distance}, DR:{_distanceRecord}");
    }
    Console.WriteLine($"R:{_race}, W:{_s}");
    _ltFrom += _s;
    _ltTo = _ltTo == 99 ? 11 : _ltTo + _s;
    if (_s > 0) SUM1 *= _s;
}



Console.WriteLine($"S1:{SUM1}");
Console.WriteLine("");


int CalculateDistance(int time, int loadtime)
{
    int _currentSpeed = 0;
    int _currentDistance = 0;
    for (int _t = loadtime + 1; _t <= time; _t++)
    {
        if (_currentSpeed < loadtime) _currentSpeed += loadtime;
        _currentDistance += _currentSpeed;
    }
    return _currentDistance;
}

