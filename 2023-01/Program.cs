using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

Console.WriteLine("Uppgift 2023-12-01!");
string[] _filedata = File.ReadAllLines("./data.txt");


int _sum1 = 0;
int _sum2 = 0;
int _sum3 = 0;
StringBuilder _sb = new StringBuilder();
SortedList<int, string> _list = new SortedList<int, string>();




foreach (string _row in _filedata)
{


    var numbers = string.Join("", _row.ToCharArray().Where(Char.IsDigit)).ToCharArray();
    if (numbers.Length > 0)
    {
        string _num = $"{numbers[0]}{numbers[numbers.Length - 1]}";
        int _value = int.Parse(_num);
        _sum1 += _value;
    }

    int _valueS2T1 = 0;
    string _tr = _row.Replace("one", "1").Replace("two", "2").Replace("three", "3").Replace("four", "4").Replace("five", "5").Replace("six", "6").Replace("seven", "7").Replace("eight", "8").Replace("nine", "9");
    numbers = string.Join("", _tr.ToCharArray().Where(Char.IsDigit)).ToCharArray();
    if (numbers.Length > 0)
    {
        string _num = $"{numbers[0]}{numbers[numbers.Length - 1]}";
        _valueS2T1 = int.Parse(_num);
        _sum2 += _valueS2T1;
    }


    _list = new SortedList<int, string>();
    int _pos = 0;
    for (int i = 0; i < _row.Length; i++)
    {
        if (i + 1 <= _row.Length && _row.Substring(i, 1) == "0") _list.Add(_list.Count, "0");
        if (i + 1 <= _row.Length && _row.Substring(i, 1) == "1") _list.Add(_list.Count, "1");
        if (i + 1 <= _row.Length && _row.Substring(i, 1) == "2") _list.Add(_list.Count, "2");
        if (i + 1 <= _row.Length && _row.Substring(i, 1) == "3") _list.Add(_list.Count, "3");
        if (i + 1 <= _row.Length && _row.Substring(i, 1) == "4") _list.Add(_list.Count, "4");
        if (i + 1 <= _row.Length && _row.Substring(i, 1) == "5") _list.Add(_list.Count, "5");
        if (i + 1 <= _row.Length && _row.Substring(i, 1) == "6") _list.Add(_list.Count, "6");
        if (i + 1 <= _row.Length && _row.Substring(i, 1) == "7") _list.Add(_list.Count, "7");
        if (i + 1 <= _row.Length && _row.Substring(i, 1) == "8") _list.Add(_list.Count, "8");
        if (i + 1 <= _row.Length && _row.Substring(i, 1) == "9") _list.Add(_list.Count, "9");
        if (i + 4 <= _row.Length && _row.Substring(i, 4) == "zero") _list.Add(_list.Count, "0");
        if (i + 3 <= _row.Length && _row.Substring(i, 3) == "one") _list.Add(_list.Count, "1");
        if (i + 3 <= _row.Length && _row.Substring(i, 3) == "two") _list.Add(_list.Count, "2");
        if (i + 5 <= _row.Length && _row.Substring(i, 5) == "three") _list.Add(_list.Count, "3");
        if (i + 4 <= _row.Length && _row.Substring(i, 4) == "four") _list.Add(_list.Count, "4");
        if (i + 4 <= _row.Length && _row.Substring(i, 4) == "five") _list.Add(_list.Count, "5");
        if (i + 3 <= _row.Length && _row.Substring(i, 3) == "six") _list.Add(_list.Count, "6");
        if (i + 5 <= _row.Length && _row.Substring(i, 5) == "seven") _list.Add(_list.Count, "7");
        if (i + 5 <= _row.Length && _row.Substring(i, 5) == "eight") _list.Add(_list.Count, "8");
        if (i + 4 <= _row.Length && _row.Substring(i, 4) == "nine") _list.Add(_list.Count, "9");
    }

    int _valueS2T2 = 0;
    if (_list.Count > 0)
    {
        string _num = $"{_list[0]}{_list[_list.Count - 1]}";
        _valueS2T2 = int.Parse(_num);
        _sum3 += _valueS2T2;
    }
    _sb.AppendLine($"T1:{_valueS2T1}, T2:{_valueS2T2}, diff:{_valueS2T1 - _valueS2T2}, data={_row}");
}

// File.WriteAllText("../../../parsed.txt", _sb.ToString());
Console.WriteLine($"S1:{_sum1}");
/* Console.WriteLine($"S2:{_sum2}"); */
Console.WriteLine($"S2:{_sum3}");
Console.WriteLine("");
