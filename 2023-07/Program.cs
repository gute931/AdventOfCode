using _2023_07;
using System.Globalization;
using System.Text;

Console.WriteLine("Uppgift 2023-12-07!");

string[] _data = File.ReadAllLines("./data.txt");
int Players = _data.Length;

List<HandS1> HandsS1 = new List<HandS1>();
List<HandS2> HandsS2 = new List<HandS2>();
for (int i = 0; i < Players; i++) HandsS1.Add(new HandS1(i + 1));
for (int i = 0; i < Players; i++) HandsS2.Add(new HandS2(i + 1));

int rundor = 0;

for (int _p = 0; _p < _data.Length; _p++)
{
    rundor++;
    HandsS1[_p].PlayRound(_data[_p]);
    HandsS2[_p].PlayRound(_data[_p]);
}

// cStep 1:

List<HandS1> _result = HandsS1.OrderByDescending(o => o.Score)
    .ThenByDescending(o => o.cV1)
    .ThenByDescending(o => o.cV2)
    .ThenByDescending(o => o.cV3)
    .ThenByDescending(o => o.cV4)
    .ThenByDescending(o => o.cV5).ToList();

uint sumGameS1 = 0;
ulong sumGameS2 = 0;
ulong sum1 = 0;
ulong sum2 = 0;

int _multplpier = Players;
foreach (var _hand in _result)
{
    // Console.WriteLine($"Spelare:{_hand.Player}, Score:{_hand.Score}, Bid:{_hand.Bid}, en:{_hand.HandResult.ToString()}, cards:{_hand.CardsToPlay}, Point:{(_hand.Bid * _multplpier)}");
    sumGameS1 += (uint)(_hand.Bid * _multplpier);
    // Console.WriteLine($"GP: {sumGame}, Player:{_hand.Player}");
    _multplpier--;

}
sum1 += (uint)sumGameS1;

// Step 2

List<HandS2> _resultS2 = HandsS2.OrderByDescending(o => o.Score)
                                .ThenByDescending(o => o.cV1)
                                .ThenByDescending(o => o.cV2)
                                .ThenByDescending(o => o.cV3)
                                .ThenByDescending(o => o.cV4)
                                .ThenByDescending(o => o.cV5).ToList();


_multplpier = Players;
StringBuilder _sb = new StringBuilder();
foreach (var _hand in _resultS2)
{
    // Console.WriteLine($"Spelare:{_hand.Player}, Score:{_hand.Score}, Bid:{_hand.Bid}, en:{_hand.HandResult.ToString()}, cards:{_hand.CardsToPlay}, Point:{(_hand.Bid * _multplpier)}, v1:{_hand.cV1}, v2:{_hand.cV2}");
    sumGameS2 += (uint)(_hand.Bid * _multplpier);
    //  Console.WriteLine($"GP: {sumGame}, Player:{_hand.Player}");
    _sb.AppendLine($"Score:{_hand.Score}, en:{_hand.HandResult.ToString()}, cards:{String.Concat(_hand.CardsToPlay.OrderBy(o => o))}, sv:{_hand.cV1} {_hand.cV2} {_hand.cV3} {_hand.cV4} {_hand.cV5}, Spelare:{_hand.Player}, Bid:{_hand.Bid}, Point:{(_hand.Bid * _multplpier)}");
    _multplpier--;

}
sum2 += sumGameS2;
File.WriteAllText("../../../GameResult.txt", _sb.ToString());

List<HandS2> _list = HandsS2.Where(w => w.Jacks > 0).OrderBy(o => o.Jacks).ThenByDescending(o => o.Score).ToList();
_sb = new StringBuilder();
foreach (var _hand in _list)
{
    _sb.AppendLine($"Jacks:{_hand.Jacks}, cards:{String.Concat(_hand.CardsToPlay.OrderBy(o => o))}, en:{_hand.HandResult.ToString()}, Score:{_hand.Score}");
}
File.WriteAllText("../../../Jacks.txt", _sb.ToString());

Console.WriteLine($"S1:{sum1} efter {rundor} rundor spelade!");
Console.WriteLine($"S2:{sum2} efter {rundor} rundor spelade!");
Console.WriteLine();