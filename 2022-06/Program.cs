// See https://aka.ms/new-console-template for more information
Console.WriteLine("Uppgift 2022-12-06!");

string _message = File.ReadAllText("./data.txt");

int _S1 = 0;
int _S2 = 0;
for (int i = 0; i < _message.Length; i++)
{
    if (_message.Substring(i, 4).Distinct().LongCount() == 4 && _S1 == 0) _S1 = i + 4;
    if (_message.Substring(i, 14).Distinct().LongCount() == 14 && _S2 == 0) _S2 = i + 14;
    if (_S1 > 0 && _S2 > 0) break;
}
Console.WriteLine($"First data S1 : {_S1}");
Console.WriteLine($"First data S2 : {_S2}");

Console.ReadLine();