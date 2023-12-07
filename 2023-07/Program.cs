using _2023_07;
using System.Globalization;

Console.WriteLine("Uppgift 2023-12-07!");


int Players = 5;
List<Hand> Hands = new List<Hand>();
for (int i = 0; i < Players; i++) Hands.Add(new Hand(i+1));


string[] _data = File.ReadAllLines("./data.txt");
int sum1 = 0;
int rundor = 0;
for (int _p = 0; _p < _data.Length; _p += 5)
{
    rundor++;
    Hands[0].PlayRound(_data[_p + 0]);
    Hands[1].PlayRound(_data[_p + 1]);
    Hands[2].PlayRound(_data[_p + 2]);
    Hands[3].PlayRound(_data[_p + 3]);
    Hands[4].PlayRound(_data[_p + 4]);

    // check order 
    Console.WriteLine($"--- runda : {rundor} ---- ");
    int sumGame = 0;
    int _multplpier = 1;
    foreach (var _hand in Hands.OrderBy(o => o.Score)
        .ThenBy(o => o.cV1)
        .ThenBy(o => o.cV2)
        .ThenBy(o => o.cV3)
        .ThenBy(o => o.cV4)
        .ThenBy(o => o.cV5))
    {
        Console.WriteLine($"Spelare:{_hand.Player}, Score:{_hand.Score}, Bid:{_hand.Bid}, en:{_hand.HandResult.ToString()}, cards:{_hand.CardsToPlay}, Point:{(_hand.Bid * _multplpier)}");
        sumGame += (_hand.Bid * _multplpier);
        _multplpier++;

    }
    sum1 += sumGame;
    Console.WriteLine($"GP: {sumGame}, Sum1:{sum1}");

}
// 1496525
Console.WriteLine($"S1:{sum1} efter {rundor} rundor spelade!");
Console.WriteLine();