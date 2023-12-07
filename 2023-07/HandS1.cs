using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _2023_07
{
    enum drawresult { Fiveofakind, Fourofakind, Fullhouse, Threeofakind, Twopair, Onepair, Highcard, Nothing }
    internal class HandS1
    {
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

        public HandS1(int player)
        {
            this.Player = player;
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



            //  List<char> Cards = new List<char>();



        }
        public void PlayRound(string record)
        {
            string[] _cd = record.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            CardsToPlay = _cd[0];
            Bid = int.Parse(_cd[1]);
            char[] cardArray = CardsToPlay.ToCharArray();

            cV1 = CardsValueMap[cardArray[0]];
            cV2 = CardsValueMap[cardArray[1]];
            cV3 = CardsValueMap[cardArray[2]];
            cV4 = CardsValueMap[cardArray[3]];
            cV5 = CardsValueMap[cardArray[4]];

            for (int i = 0; i < cardArray.Length; i++)
            {
                CardsValue[i] = CardsValueMap[cardArray[i]];
            }

            var _res = (
                from c in CardsToPlay
                group c by Convert.ToChar(c)
                ).ToDictionary(c => c.Key, c => c.Count());

            if (_res.ContainsValue(5))
            {
                HandResult = drawresult.Fiveofakind;
                Score = 10;
            }
            else if (_res.ContainsValue(4))
            {
                HandResult = drawresult.Fourofakind;
                Score = 9;
            }
            else if (_res.ContainsValue(3) && _res.ContainsValue(2))
            {
                HandResult = drawresult.Fullhouse;
                Score = 8;
            }
            else if (_res.ContainsValue(3))
            {
                HandResult = drawresult.Threeofakind;
                Score = 7;
            }
            else if (_res.ContainsValue(2))
            {
                switch (_res.Count(c => c.Value == 2))
                {
                    case 1:
                        Score = 1;
                        HandResult = drawresult.Onepair;
                        break;
                    case 2:
                        Score = 2;
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
                        Score = 0;
                        return;
                    }
                    prev = _cardsSorted[i];
                }
                HandResult = drawresult.Highcard;
                Score = 1;

            }
        }


    }
}
