using System.Diagnostics;
Console.WriteLine("Uppgift 2023-12-05!");

string[] _filedata = File.ReadAllLines("./data.txt");

int[] _timeItems = _filedata[0].Substring(10).Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToArray();
int[] _distanceItems = _filedata[1].Substring(10).Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToArray();

int _times2 = Convert.ToInt32(_filedata[0].Substring(10).Replace(" ", ""));
long _ditance2 = Convert.ToInt64(_filedata[1].Substring(10).Replace(" ", ""));

int SUM1 = 1;
int SUM2 = 0;

for (int _race = 0; _race < _timeItems.Length; _race++)
{
    int _raceLength = _timeItems[_race];
    int _distanceRecord = _distanceItems[_race];
    int _s = 0;

    for (int _t = 1; _t <= _raceLength; _t++)
    {
        long _distance = CalculateDistance(_raceLength, _t);
        if (_distance > _distanceRecord) _s++;
    }

    if (_s > 0) SUM1 *= _s;
}

for (int _t = 1; _t <= _times2; _t++)
{
    long _distance = CalculateDistance(_times2, _t);
    if (_distance > _ditance2) SUM2++;
}

Console.WriteLine($"S1:{SUM1}");
Console.WriteLine($"S2:{SUM2}");
Console.WriteLine();

long CalculateDistance(long time, long loadtime)
{
    return (time-loadtime) * loadtime;
 
}

