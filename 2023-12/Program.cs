using System.Text;

Console.WriteLine("2023-12");

string[] Data = File.ReadAllLines("data.txt");
char[] patternmatches = ['.', '#'];
SortedDictionary<(long, long, long), long> History;

long combinations1 = 0;
long combinations2 = 0;
foreach (var item in Data)
{
    string[] DataParts = item.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    int[] groupSize = DataParts[1].Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToArray<int>();
    History = new SortedDictionary<(long, long, long), long>();
    long score1 = Match(DataParts[0], groupSize, 0, 0, 0);
    StringBuilder _sbPattern = new StringBuilder();
    StringBuilder _sbGroup = new StringBuilder();
    for (long i = 0; i < 5; i++) 
    {
        _sbPattern.Append(DataParts[0]);
        _sbGroup.Append(DataParts[1]);
        if (i < 4)
        {
            _sbPattern.Append("?");
            _sbGroup.Append(",");
        }
    }
    groupSize = _sbGroup.ToString().Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToArray<int>();
    History = new SortedDictionary<(long, long, long), long>();
    long score2 = Match(_sbPattern.ToString(), groupSize, 0, 0, 0);
    // Console.WriteLine($"DP:{DataParts[0]}, s:{score1}");
    combinations1 += score1;
    combinations2 += score2;
}

Console.WriteLine($"S1:{combinations1}");
Console.WriteLine($"S2:{combinations2}");
// 15464163264
Console.WriteLine("");

long Match(string codeddata, int[] groups, int dataPtr, long dots, long hashtags)
{
    (long, long, long) HistoryKey = (dataPtr, dots, hashtags);
    if (History.ContainsKey(HistoryKey)) return History[HistoryKey];

    if (dataPtr == codeddata.Length)
    {
        if (dots == groups.Length && hashtags == 0)
        {
            // Console.WriteLine($"r1: if {dots} == {groups.Length} && {hashtags} == {0},  data:{codeddata}, HK:{HistoryKey}, score:1, dots:{dots}, hashtags:{hashtags}, groups.Length:{groups.Length} ");
            return 1;
        }
        else if (dots == groups.Length - 1 && groups[dots] == hashtags)
        {
            // Console.WriteLine($"r2: if {dots} == {groups.Length - 1} && {groups[dots]} == {0},   data:{codeddata}, HK:{HistoryKey}, score:1, dots:{dots}, hashtags:{hashtags} ");
            return 1;
        }
        else
        {
            // Console.WriteLine($"r3: else. data:{codeddata}, HK:{HistoryKey}, score:0, dots:{dots}, hashtags:{hashtags} ");
            return 0;
        }
    }

    long comb = 0;
    foreach (char c in patternmatches)
    {
        //Console.WriteLine($"dataptr:{dataPtr}, data:{codeddata}, key:{HistoryKey}: cmpchar:{c}, datachar:{codeddata[dataPtr]}");
        if (codeddata[dataPtr] == c || codeddata[dataPtr] == '?')
        {
            if (c == '.' && hashtags == 0)
            {
                //Console.WriteLine($"1: d:{codeddata[dataPtr]}, c:{c}, ");
                comb += Match(codeddata, groups, dataPtr + 1, dots, 0);
                //Console.WriteLine($"1: d:{codeddata[dataPtr]}, c:{c}, r:{comb}");
            }
            else if (c == '.' && hashtags > 0 && dots < groups.Length && groups[dots] == hashtags)
            {
                //Console.WriteLine($"2: d:{codeddata[dataPtr]}, c:{c}, ");
                comb += Match(codeddata, groups, dataPtr + 1, dots + 1, 0);
                //Console.WriteLine($"2: d:{codeddata[dataPtr]}, c:{c}, ");
            }
            else if (c == '#')
            {
                //Console.WriteLine($"3: d:{codeddata[dataPtr]}, c:{c}, ");
                comb += Match(codeddata, groups, dataPtr + 1, dots, hashtags + 1);
                //Console.WriteLine($"3: d:{codeddata[dataPtr]}, c:{c}, ");
            }
        }
        //Console.WriteLine($"dataptr:{dataPtr}, data:{codeddata}, Skipped!");

    }
    //Console.WriteLine($"Return: HK:{HistoryKey}, C:{comb}");
    History[HistoryKey] = comb;
    return comb;
}