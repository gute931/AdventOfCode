// string[] lines = File.ReadAllLines("testdata.txt");
string[] lines = File.ReadAllLines("data.txt");  // 4923 fel: to high, 4774 fel, 4884

List<List<string>> pageOrders = new List<List<string>>();
List<List<string>> manuals = new List<List<string>>();
List<List<string>> inValidManuals = new List<List<string>>();
moveMethod method = moveMethod.Swap;

int S1 = 0;
int S2 = 0;
// read indata
foreach (var line in lines)
{
    if (line.Contains("|"))
    {
        pageOrders.Add(line.Split('|').ToList<string>());
    }
    else if (line.Contains(","))
    {
        manuals.Add(line.Split(",").ToList<string>());
    }
}

// S1 : Check all that follows the rules
foreach (var manual in manuals)
{
    bool valid = isManualValid(pageOrders, manual);
    if (valid)
    {
        int m = int.Parse(Convert.ToString(manual.Count / 2));
        // Console.WriteLine($"status:{page[m]}");
        S1 += Convert.ToInt32(manual[m]);
    }
    else
    {
        inValidManuals.Add(manual);
    }
}



Console.WriteLine($"S1 : {S1}");

foreach (var manual in inValidManuals)
{
    bool done = false;
    int iterations = 0;
    while (!done)
    {
        iterations++;
        bool ok = FixInvalidManuals(pageOrders, method, manual);
        done = isManualValid(pageOrders, manual);
        if (done)
        {
            int m = int.Parse(Convert.ToString(manual.Count / 2));
            // Console.WriteLine("Done : middle: {0}, Iter : {2},  pageorder : {1}", manual[m], string.Join(", ", manual), iterations);
            S2 += Convert.ToInt32(manual[m]);
        }
    }
}

Console.WriteLine($"S2 : {S2}");

static bool isManualValid(List<List<string>> pageOrders, List<string> manual)
{
    foreach (var pageOrder in pageOrders)
    {
        var pagePositions = manual.Select((item, index) => new { item, index })
                                       .Where(x => pageOrder.Contains(x.item))
                                       .Select(x => new { page = x.item, pagePosition = pageOrder.IndexOf(x.item), manualPosition = x.index })
                                       .OrderBy(x => x.pagePosition)
                                       .ToList();

        if (pagePositions.Count == 2 && pagePositions[0].manualPosition > pagePositions[1].manualPosition)
        {
            return false;
        }
    }
    return true;
}


static bool FixInvalidManuals(List<List<string>> pageOrders, moveMethod method, List<string> manual)
{
    int manualItemsBegining = manual.Count;

    // Console.WriteLine("**** Begin *****");
    // Console.WriteLine("Begin       <-> {0}", string.Join(", ", manual));
    foreach (var pageOrder in pageOrders)
    {
        var pagePositions = manual.Select((item, index) => new { item, index })
                               .Where(x => pageOrder.Contains(x.item))
                               .Select(x => new { page = x.item, pagePosition = pageOrder.IndexOf(x.item), manualPosition = x.index })
                               .OrderBy(x => x.pagePosition)
                               .ToList();

        if (pagePositions.Count == 2 && pagePositions[0].manualPosition > pagePositions[1].manualPosition)
        {
            // Console.WriteLine("pageOrder : {0}", string.Join(", ", pageOrder));
            switch (method)
            {
                case moveMethod.FirstToLast:
                    // move from left to right
                    var moveOrderLR = pagePositions.OrderByDescending(p => p.pagePosition).ToList();
                    // Console.WriteLine("{0}-{1} <-> {2}-{3} , {4} -> {5}, L2R", pageOrder[0], pageOrder[1], pagePositions[0].pagePosition, pagePositions[1].pagePosition, moveOrderLR[0].manualPosition, moveOrderLR[1].manualPosition);
                    // Console.WriteLine("before : {0}", string.Join(", ", manual));
                    string item = manual[moveOrderLR[0].manualPosition];

                    manual.Insert(moveOrderLR[1].manualPosition + moveOrderLR[0].pagePosition, item);
                    manual.RemoveAt(moveOrderLR[0].manualPosition + +moveOrderLR[1].pagePosition);
                    // Console.WriteLine("after : {0}", string.Join(", ", manual));
                    // Console.WriteLine("");
                    break;
                case moveMethod.Swap:
                    var moveOrderSwap = pagePositions.OrderByDescending(p => p.manualPosition).ToList();
                    // Console.WriteLine("{0}-{1} <-> {2}-{3} , {4} -> {5}, swap", pageOrder[0], pageOrder[1], pagePositions[0].pagePosition, pagePositions[1].pagePosition, moveOrderSwap[0].manualPosition, moveOrderSwap[1].manualPosition);
                    // Console.WriteLine("before : {0}", string.Join(", ", manual));
                    string item0 = manual[moveOrderSwap[0].manualPosition];
                    string item1 = manual[moveOrderSwap[1].manualPosition];

                    manual.RemoveAt(moveOrderSwap[0].manualPosition);
                    manual.RemoveAt(moveOrderSwap[1].manualPosition);

                    manual.Insert(moveOrderSwap[1].manualPosition, item0);
                    manual.Insert(moveOrderSwap[0].manualPosition, item1);
                    // Console.WriteLine("after : {0}", string.Join(", ", manual));
                    // Console.WriteLine("");
                    break;
                default:
                    break;
            }


        }

    }
    return true;
}
enum moveMethod { FirstToLast, Swap };
