using System.Text;

Console.WriteLine("2023-12-08");
string[] _data = File.ReadAllLines("./data.txt");

char[] instructions = _data[0].Trim().ToCharArray();
SortedList<string, (string, string)> map = new SortedList<string, (string, string)>();
List<string> startStringsS1 = new List<string>();
List<string> startStringsS2 = new List<string>();
for (int i = 2; i < _data.Length; i++)
{
    string[] _p = _data[i].Split(" =(,)".ToCharArray(), StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    map.Add(_p[0], (_p[1], _p[2]));
    if (_p[0] == "AAA") startStringsS1.Add(_p[0]);
    if (_p[0].Last() == 'A') startStringsS2.Add(_p[0]);
}

StringBuilder sb = new StringBuilder();

int SUM1 = 0;
SUM1 = GetZ(startStringsS1[0], instructions, map, true);


List<int> targets = new List<int>();
for (int i = 0; i < startStringsS2.Count(); i++)
{
    targets.Add(GetZ(startStringsS2[i], instructions, map, false));
}


long SUM2 = lcm_of_array_elements(targets.ToArray());


Console.WriteLine($"SUM1:{SUM1}");
Console.WriteLine($"SUM2:{SUM2}");
Console.ReadLine();

static int GetZ(string start, char[] instructions, SortedList<string, (string, string)> map, bool EndWithZZZ)
{
    int SUM = 0;
    bool _done = false;
    string _current = start;
    while (!_done)
    {

        foreach (var instruction in instructions)
        {
            SUM++;

            switch (instruction)
            {
                case 'L':
                    _current = map[_current].Item1;
                    break;
                case 'R':
                    _current = map[_current].Item2;
                    break;
                default:
                    Console.WriteLine("Fel tecken!");
                    break;
            }

            bool possibleEnd = (EndWithZZZ && _current == "ZZZ") || (!EndWithZZZ && _current.Last() == 'Z') ? true : false;
            if (possibleEnd)
            {
                _done = true;
                break;
            }
        }
    }
    return SUM;
}


long lcm_of_array_elements(int[] element_array)
{
    long lcm_of_array_elements = 1;
    int divisor = 2;

    while (true)
    {

        int counter = 0;
        bool divisible = false;
        for (int i = 0; i < element_array.Length; i++)
        {

            // lcm_of_array_elements (n1, n2, ... 0) = 0.
            // For negative number we convert into
            // positive and calculate lcm_of_array_elements.
            if (element_array[i] == 0)
            {
                return 0;
            }
            else if (element_array[i] < 0)
            {
                element_array[i] = element_array[i] * (-1);
            }
            if (element_array[i] == 1)
            {
                counter++;
            }

            // Divide element_array by devisor if complete
            // division i.e. without remainder then replace
            // number with quotient; used for find next factor
            if (element_array[i] % divisor == 0)
            {
                divisible = true;
                element_array[i] = element_array[i] / divisor;
            }
        }

        // If divisor able to completely divide any number
        // from array multiply with lcm_of_array_elements
        // and store into lcm_of_array_elements and continue
        // to same divisor for next factor finding.
        // else increment divisor
        if (divisible)
        {
            lcm_of_array_elements = lcm_of_array_elements * divisor;
        }
        else
        {
            divisor++;
        }

        // Check if all element_array is 1 indicate 
        // we found all factors and terminate while loop.
        if (counter == element_array.Length)
        {
            return lcm_of_array_elements;
        }
    }
}
