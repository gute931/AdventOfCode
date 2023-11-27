using _2022_11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022_11
{
    internal class MonkeyProcess
    {
        public static int execute(string filename, int SolutionPart, int rounds, double worryLevelNo)
        {
            string[] _filedata = File.ReadAllLines(filename);

            // string[] _filedata = File.ReadAllLines("./testdata.txt");

            SortedDictionary<int, Monkey> _monkeys = new SortedDictionary<int, Monkey>();
            Monkey _currentMonkey = null;

            // Ladda från datafilen
            foreach (string _record in _filedata)
            {
                string[] _parts = _record.Split(" :".ToCharArray());
                if (_parts.Length > 0 && _parts[0] == "Monkey")
                {
                    _currentMonkey = new Monkey(Convert.ToInt32(_parts[1]), SolutionPart);
                    _monkeys.Add(_currentMonkey.monkeyId, _currentMonkey);
                }
                else if (_parts.Length > 0 && _currentMonkey != null)
                {
                    _currentMonkey.ParseRec(_record);
                }
            }

            // Loopa igenom alla object så många ggr. som parameter sounds sätts till
            for (int i = 0; i < rounds; i++)
            {
                foreach (var _monkey in _monkeys)
                {
                    List<(int, int)> _results = _monkey.Value.Process(worryLevelNo);
                    foreach (var _result in _results)
                    {
                        Monkey _receiver = _monkeys[_result.Item1];
                        _receiver.addLevel(_result.Item2);
                    }
                }

            }
            Console.WriteLine($"==== After {rounds} rouonds ===== worrylevel : {worryLevelNo}. =====");
            foreach (var _monkey in _monkeys)
            {
                String _dlm = "";
                Console.Write($"Monkey {_monkey.Value.monkeyId}");
                foreach (var item in _monkey.Value.items)
                {
                    Console.Write($"{_dlm} {item}");
                    _dlm = ",";
                }

                Console.WriteLine();
            }

            foreach (var _monkey in _monkeys)
            {
                Console.WriteLine($"Monkey {_monkey.Value.monkeyId}, Inspections: {_monkey.Value.inspections}");
            }


            Console.WriteLine();

            int[] resultObs = _monkeys.OrderByDescending(S => S.Value.inspections).Take(2).Select(s => s.Value.inspections).ToArray();
            int _sum = resultObs[0] * resultObs[1];
            Console.WriteLine($"Solution:{SolutionPart}, result :{_sum}");
            return _sum;

            // int _total = _monkeys.OrderByDescending(S => S.Value.inspections).Take(2).Aggregate(s => s.Value.inspections);


        }
    }
}
