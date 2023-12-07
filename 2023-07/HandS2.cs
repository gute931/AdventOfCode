using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace _2023_07
{
    internal class HandS2
    {
        Dictionary<char, int> CardsValueMapJacks = new Dictionary<char, int>();
        Dictionary<char, int> CardsValueMap = new Dictionary<char, int>();
        //  List<char> Cards;
        public int Player;
        public drawresult HandResult = drawresult.Nothing;
        public string CardsToPlay;
        public int Bid;
        public int Score;
        char bestCard;
        int[] CardsValue = new int[5];
        public int cV1 = 0;
        public int cV2 = 0;
        public int cV3 = 0;
        public int cV4 = 0;
        public int cV5 = 0;
        public int Jacks { get; set; } = 0;

        public HandS2(int player)
        {
            this.Player = player;
            CardsValueMapJacks.Add('A', 13);
            CardsValueMapJacks.Add('K', 12);
            CardsValueMapJacks.Add('Q', 11);
            CardsValueMapJacks.Add('T', 10);
            CardsValueMapJacks.Add('9', 9);
            CardsValueMapJacks.Add('8', 8);
            CardsValueMapJacks.Add('7', 7);
            CardsValueMapJacks.Add('6', 6);
            CardsValueMapJacks.Add('5', 5);
            CardsValueMapJacks.Add('4', 4);
            CardsValueMapJacks.Add('3', 3);
            CardsValueMapJacks.Add('2', 2);
            CardsValueMapJacks.Add('J', 1);

            CardsValueMap.Add('A', 14);
            CardsValueMap.Add('K', 13);
            CardsValueMap.Add('Q', 12);
            CardsValueMap.Add('J', 11);
            CardsValueMap.Add('T', 10);
            CardsValueMap.Add('9', 9);
            CardsValueMap.Add('8', 8);
            CardsValueMap.Add('7', 7);
            CardsValueMap.Add('6', 6);
            CardsValueMap.Add('5', 5);
            CardsValueMap.Add('4', 4);
            CardsValueMap.Add('3', 3);
            CardsValueMap.Add('2', 2);
        }
        public void PlayRound(string record)
        {
            string[] _cd = record.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            CardsToPlay = _cd[0];
            Bid = int.Parse(_cd[1]);

            char[] cardArray = CardsToPlay.ToCharArray();
            cV1 = CardsValueMapJacks[cardArray[0]];
            cV2 = CardsValueMapJacks[cardArray[1]];
            cV3 = CardsValueMapJacks[cardArray[2]];
            cV4 = CardsValueMapJacks[cardArray[3]];
            cV5 = CardsValueMapJacks[cardArray[4]];

            for (int i = 0; i < cardArray.Length; i++)
            {
                CardsValue[i] = CardsValueMap[cardArray[i]];
            }

            Jacks = CardsToPlay.Where(w => w == 'J').Count();
            if (Jacks > 0)
            {
                Score = CheckHandsJacks(Jacks);
            }
            else
            {
                Score = CheckHand(CardsToPlay);
            }
        }

        internal int CheckHand(string CardsToPlay)
        {
            int _score = 0;

            var _res = (
                from c in CardsToPlay
                group c by Convert.ToChar(c)
                ).ToDictionary(c => c.Key, c => c.Count());

            if (_res.ContainsValue(5))
            {
                HandResult = drawresult.Fiveofakind;
                _score = 7;
            }
            else if (_res.ContainsValue(4))
            {
                HandResult = drawresult.Fourofakind;
                _score = 6;
            }
            else if (_res.ContainsValue(3) && _res.ContainsValue(2))
            {
                HandResult = drawresult.Fullhouse;
                _score = 5;
            }
            else if (_res.ContainsValue(3))
            {
                HandResult = drawresult.Threeofakind;
                _score = 4;
            }
            else if (_res.ContainsValue(2))
            {
                switch (_res.Count(c => c.Value == 2))
                {
                    case 1:
                        _score = 2;
                        HandResult = drawresult.Onepair;
                        break;
                    case 2:
                        _score = 3;
                        HandResult = drawresult.Twopair;
                        break;
                }
            }
            else
            {
                // int[] _cardsSorted = Array.Sort(CardsValue);
                int[] _cardsSorted = CardsValue.OrderBy(x => x).ToArray();

                int prev = _cardsSorted[0];
                for (int i = 1; i < _cardsSorted.Length; i++)
                {
                    if (prev + 1 != _cardsSorted[i])
                    {
                        HandResult = drawresult.Nothing;
                        _score = 0;
                        return _score;
                    }
                    prev = _cardsSorted[i];
                }

                HandResult = drawresult.Highcard;
                _score = 1;

            }
            return _score;
        }



        internal int CheckHandsJacks(int _Js)
        {
            int _score = 0;

            string _CardsToPlay = CardsToPlay.Replace("J", "");

            var _res = (
                from c in _CardsToPlay
                group c by Convert.ToChar(c)
                    ).ToDictionary(c => c.Key, c => c.Count());

            if (_Js >= 4 || (_Js == 3 && _res.ContainsValue(2)) || (_Js == 2 && _res.ContainsValue(3)) || (_Js == 1 && _res.ContainsValue(4)) )
            {
                HandResult = drawresult.Fiveofakind;
                _score = 7;
            }
            else if ((_Js == 3) || (_Js == 2 && _res.ContainsValue(2)) ||  (_Js == 1 && _res.ContainsValue(3)))
            {
                HandResult = drawresult.Fourofakind;
                _score = 6;
            }
            else if (_Js == 2)
            {
                HandResult = drawresult.Threeofakind;
                _score = 4;
            }
            else if (_Js == 1 && _res.ContainsValue(2))
            {
                if (_res.Where(x => x.Value == 2).Count() == 2)
                {
                    HandResult = drawresult.Fullhouse;
                    _score = 5;
                }
                else {
                    // three of a kind
                    HandResult = drawresult.Threeofakind;
                    _score = 4;
                }
            }
            else
            {
                HandResult = drawresult.Onepair;
                _score = 2;
            }


            /*
            for (int i = 0; i < _Js; i++)
            {
                _CardsToPlay = _CardsToPlay += "J";

            }
            char[] cardArray = _CardsToPlay.ToCharArray();
            cV1 = CardsValueMap[cardArray[0]];
            cV2 = CardsValueMap[cardArray[1]];
            cV3 = CardsValueMap[cardArray[2]];
            cV4 = CardsValueMap[cardArray[3]];
            cV5 = CardsValueMap[cardArray[4]];
            */
            // Console.WriteLine($"JS:{_Js}, Cards:{_CardsToPlay}, Score:{_score}, Handresult:{HandResult}"); 
            return _score;
        }
    }

}