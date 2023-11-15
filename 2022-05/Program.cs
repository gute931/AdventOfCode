// See https://aka.ms/new-console-template for more information
using System.Text;

Console.WriteLine("Hello, World!");


string[] _rows = File.ReadAllLines("./data.txt");
// find end of config
// start of transactions
int _configRows = 0;
int _stacks = 9;
int _transationStartRow = 0;
foreach (var x in _rows.Select((value, index) => new { value, index }))
{
    if (_configRows == 0 && x.value.Trim().StartsWith("1"))
    {
        _configRows = x.index - 1;
        // _stacks = x.value.Split(' ').Length;
    }
    if (x.value.StartsWith("m"))
    {
        _transationStartRow = x.index;
        break;
    }
}



Stack<string>[] stackPositionS1 = new Stack<string>[9];
Stack<string>[] stackPositionS2 = new Stack<string>[9];
for (int i = 0; i < stackPositionS1.Length; i++)
{
    stackPositionS1[i] = new Stack<string>();
    stackPositionS2[i] = new Stack<string>();
}

// load crates
for (int _r = _configRows; _r >= 0; _r--)
{

    for (int _i = 0; _i < _stacks; _i++)
    {
        string _box = _rows[_r].Substring(_i * 4, 3).Trim();
        if (!String.IsNullOrEmpty(_box))
        {
            stackPositionS1[_i].Push(_box);
            stackPositionS2[_i].Push(_box);
        }
    }
}


Console.WriteLine($"ConfigRows : {_configRows}");
Console.WriteLine($"Stacks : {_stacks}");
Console.WriteLine($"TransationStartRow : {_transationStartRow}");

Stack<string> _tempS2 = new Stack<string>();
// move crates
for (int _r = _transationStartRow; _r < _rows.Length; _r++)
{
    string[] _transaction = _rows[_r].Split(' ');
    int _amount = int.Parse(_transaction[1]);
    int _from = int.Parse(_transaction[3]) - 1;
    int _to = int.Parse(_transaction[5]) - 1;

    // Svar 1.
    for (int m = 0; m < _amount; m++)
    {
        string _box = stackPositionS1[_from].Pop();
        stackPositionS1[_to].Push(_box);
    }

    // Svar 2.
    for (int m = 0; m < _amount; m++) _tempS2.Push(stackPositionS2[_from].Pop());
    for (int m = 0; m < _amount; m++) stackPositionS2[_to].Push(_tempS2.Pop());

}

StringBuilder _svar_1 = new StringBuilder();
StringBuilder _svar_2 = new StringBuilder();

for (int i = 0; i < 9; i++)
{
    _svar_1.Append(stackPositionS1[i].Pop().Substring(1, 1));
    _svar_2.Append(stackPositionS2[i].Pop().Substring(1, 1));
}

Console.WriteLine($"Svar : {_svar_1.ToString()}");
Console.WriteLine($"Svar : {_svar_2.ToString()}");
// S1 -> SPFMVDTZT
// S2 -> ZFSJBPRFP
Console.ReadLine();



