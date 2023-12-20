
using _2023_15;
using System.Text;
using System.Text.RegularExpressions;

Console.WriteLine("2023-14");

string Data = File.ReadAllText("data.txt");

Console.WriteLine($"S1={Step1(Data)}");
Console.WriteLine("");


SortedList<int, List<GtHashCode>> Boxes = new SortedList<int, List<GtHashCode>>();
for (int i = 0; i < 256; i++) Boxes.Add(i, new List<GtHashCode>());

string[] codedLetters = Data.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
foreach (string codedLetter in codedLetters)
{
    GtHashCode hc = new GtHashCode(codedLetter);
    switch (hc.Add)
    {
        case true: // Add
            List<GtHashCode> list = Boxes[hc.Box];
            if (list.Find(w => w.Code == hc.Code) == null)
            {
                list.Add(hc);
            }
            else
            {
                GtHashCode _hit = list.Where(w => w.Code == hc.Code).First();
                _hit.Replace(hc);
            }
            break;
        default: // Delete
            foreach (var item in Boxes)
            {
                if (item.Value.Where(w=>w.Code == hc.Code).Count() > 0)
                {
                    item.Value.RemoveAll(w => w.Code == hc.Code);
                }

            }
            break;
    }
}
int S2 = 0;
int _box = 0;
foreach (var items in Boxes)
{
    _box++;
    int _pos = 0; 
    foreach (var item in items.Value)
    {
        _pos++;
        S2 += _box * _pos * item.Index;
    }
}

// 698295 TH
Console.WriteLine($"S2:{S2}");

Console.WriteLine("");


int hash(string l)
{
    int _sum = 0;
    foreach (char b in l)
    {
        _sum += b;
        _sum *= 17;
        _sum = _sum % 256;
    }
    return _sum;
}


int Step1(string Data)
{
    int S1 = 0;
    string[] codedLetters = Data.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    foreach (string codedLetter in codedLetters)
    {
        S1 += hash(codedLetter);
    }
    return S1;
}






/*
SortedList<string, string> pattern = new SortedList<string, string>();
pattern.add(@"\brn=1\b", "30");
pattern.add(@"\bcm-\b", "253");
pattern.add(@"\bqp=3\b", "97");
pattern.add(@"\bcm=2\b", "47");
pattern.add(@"\bqp-\b", "14");
pattern.add(@"\bpc=4\b", "48");
pattern.add(@"\bot=7\b", "217");

StringBuilder sb = new StringBuilder();
    foreach (var letter in codedLetters)
    {
        switch (codedLetters)
        {
            case "rn=1": 
                break;
            case "cm- ": 
                break;
            case "qp=3": 
                break;
            case "cm=2": 
                break;
            case "qp- ": 
                break;
            case "pc=4": 
                break;
            case "ot=9": 
                break;
            case "ab=5": 
                break;
            case "pc- ": 
                break;
            case "pc=6":
                break;
             case "ot=7":
                break;
        }
    }
}
*/

