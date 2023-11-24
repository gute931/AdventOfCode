using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _2022_11
{
    internal class Monkey
    {
        public int monkeyId { get; set; }
        public List<int> items { get; set; }
        public string operation { get; set; }
        public string opItem1 { get; set; }
        public string opItem2 { get; set; }
        public int divisibleBy { get; set; }
        public int throwToTrue { get; set; }
        public int throwToFalse { get; set; }
        public bool opitem2Num { get; set; }
        public int opitem2Value { get; set; }

        public int inspections { get; set; } = 0;
        public int inspectionTotal { get; set; } = 0;


        public Monkey(int MonkeyId)
        {
            monkeyId = MonkeyId;
            items = new List<int>();
        }
        internal void addLevel(int level)
        {
            items.Add(level);
        }

        internal void ParseRec(string rec)
        {
            string[] _parts = rec.TrimStart().Split(" ,".ToCharArray());
            // Monkey 0:
            // Starting items: 79, 98
            // Operation: new = old * 19
            // Test: divisible by 23
            // If true: throw to monkey 2
            // If false: throw to monkey 3
            switch (_parts[0])
            {
                case "Starting":
                    for (int i = 2; i < _parts.Length; i+=2)
                    {
                        items.Add(int.Parse(_parts[i]));
                    }
                    break;
                case "Operation:":
                    opItem1 = _parts[3];
                    operation = _parts[4];
                    opItem2 = _parts[5];
                    opitem2Num = opItem2.All(Char.IsDigit);
                    if (opitem2Num) opitem2Value = Convert.ToInt32(opItem2);
                    break;
                case "Test:":
                    divisibleBy = int.Parse(_parts[3]);
                    break;
                case "If":
                    switch (_parts[1])
                    {
                        case "true:":
                            throwToTrue = int.Parse(_parts[5]);
                            break;
                        case "false:":
                            throwToFalse = int.Parse(_parts[5]);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }

        }

        internal List<(int, int)> Process()
        {
            List<(int, int)> _rl = new List<(int, int)>();

            int value = 0;
            foreach (int i in items)
            {
                inspections++;
                switch (operation)
                {
                    case "+":
                        if (opitem2Num)
                        {
                            value = i + opitem2Value;
                        }
                        break;
                    case "-":
                        if (opitem2Num)
                        {
                            value = i - opitem2Value;
                        }
                        break;
                    case "*":
                        if (opitem2Num)
                        {
                            value = i * opitem2Value;
                        }
                        else if (opItem2 == "old")
                        {
                            value = i * i;
                        }
                        break;
                    case "/":
                        if (opitem2Num)
                        {
                            value = i / opitem2Value;
                        }
                        break;
                    default:
                        break;
                }
                int worryLevel = value / 3;
                
                int _targetMonkey = worryLevel % divisibleBy == 0 ? throwToTrue : throwToFalse;
                _rl.Add((_targetMonkey, worryLevel));
                inspectionTotal += worryLevel;
            }

            items.Clear();
            return _rl;
        }
    }
}
