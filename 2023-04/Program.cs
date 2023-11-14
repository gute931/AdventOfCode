// See https://aka.ms/new-console-template for more information
Console.WriteLine("Uppgift 2022-12-04!");

string[] _rows = File.ReadAllLines("./data.txt");

int _overlaps_1 = 0;
int _overlaps_2 = 0;

foreach (var _row in _rows)
{
    string[] varden = _row.Split(",-".ToCharArray());
    int sec1s = int.Parse(varden[0]);
    int sec1e = int.Parse(varden[1]);
    int sec2s = int.Parse(varden[2]);
    int sec2e = int.Parse(varden[3]);

    _overlaps_1 += ((sec1s >= sec2s && sec1e <= sec2e) || (sec2s >= sec1s && sec2e <= sec1e)) ? 1 : 0;
    _overlaps_2 += ((sec1s <= sec2e && sec1e >= sec2s) || (sec2s <= sec1e && sec2e >= sec1s)) ? 1 : 0;

}

Console.WriteLine($"Overlaps S1 : {_overlaps_1}");
Console.WriteLine($"Overlaps S2 : {_overlaps_2}");

Console.ReadLine();
