using System.Text.RegularExpressions;

string _data = File.ReadAllText("..\\..\\..\\data.txt");

// string patternS1 = @"mul\((\d+),(\d+)\)";
string patternS2 = @"(mul\((\d+),(\d+)\))|(do\(\))|(don't\(\))";
Regex regex = new Regex(patternS2);
MatchCollection matches = regex.Matches(_data);

int S1 = 0;
int S2 = 0;
bool calcMode = true;

foreach (Match match in matches)
{
    // Console.WriteLine(match.Value);
    switch (match.Value.Split('(')[0])
    {
        case "mul":
            int _sum = Convert.ToInt32(match.Groups[2].Value) * Convert.ToInt32(match.Groups[3].Value);
            S1 += _sum;
            if (calcMode) S2 += _sum;
            break;
        case "do":
            calcMode = true;
            break;
        case "don't":
            calcMode = false;
            break;
        default:
            Console.ReadLine();
            break;
    }

}
Console.WriteLine($"S1 : {S1}");
Console.WriteLine($"S2 : {S2}");

Console.ReadLine();
