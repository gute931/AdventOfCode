using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;


namespace _2022_12
{
    enum status { deadend, ontrack, start, end };
    internal class GtNode
    {
        public char currentLetter { get; set; }
        public string MyPosition { get; set; }
        char start;
        char slut;
        int Level;
        internal List<(string, GtNode)> Nodes = new List<(string, GtNode)>();
        char[] _search = new char[2];
        status NodeStatus = status.deadend;
        List<string> LocalPath;
        public GtNode(int currentRow, int currentCol, char currentLetter, List<string> inLocalPath, int inLevel)
        {
            try
            {
                Level = inLevel + 1;
                LocalPath = new List<string>(inLocalPath);
                LocalPath.Add($"{currentRow}:{currentCol}");
                this.currentLetter = currentLetter;
                MyPosition = $"{currentRow}:{currentCol}";

                _search[0] = currentLetter;
                if (currentLetter == 'z')
                {
                    _search[1] = 'E';
                }
                else _search[1] = (char)(((int)currentLetter) + 1);


                GtConfig.Instance.IncreaseSearchCnt();
                List<(int, int)> _check = new List<(int, int)>();
                _check.Add((currentRow - 1, currentCol));
                _check.Add((currentRow + 1, currentCol));
                _check.Add((currentRow, currentCol - 1));
                _check.Add((currentRow, currentCol + 1));
                foreach (var item in _check)
                {
                    int _row2Check = item.Item1;
                    int _col2Check = item.Item2;
                    if (GtConfig.Instance.InRange(_row2Check, _col2Check)) // Exclude it's own pos
                    {
                       // Console.WriteLine($"s:{currentRow}:{currentCol}, t:{_row2Check}:{_col2Check}, {currentLetter}, check");
                        if (!this.LocalPath.Contains($"{_row2Check}:{_col2Check}")) // Check that hasn't been searche earlier
                        {

                            checkPosition(_row2Check, _col2Check);
                        }
                        else
                        {
                             Console.WriteLine($"s:{currentRow}:{currentCol}, t:{_row2Check}:{_col2Check}, skip");

                        }
                    }
                    else
                    {
                        Console.WriteLine($"s:{currentRow}:{currentCol}, t:{_row2Check}:{_col2Check}, skip");
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
        }

        void checkPosition(int inCurrentRow, int inCurrentCol)
        {
            string _checkPos = $"{inCurrentRow}:{inCurrentCol}";
            char _currentLetter = GtConfig.Instance.Matrix[inCurrentRow, inCurrentCol];
            // Console.WriteLine($"Pos: {MyPosition} -> {_checkPos} -> sf:{_search[0]}<->{_search[1]} data -> {_currentLetter}.");

            if (_search.Contains(_currentLetter))
            {
                NodeStatus = status.ontrack;
                Console.WriteLine($"Jump To :{_checkPos}");
                Nodes.Add(($"{inCurrentRow}:{inCurrentCol}", new GtNode(inCurrentRow, inCurrentCol, _currentLetter, this.LocalPath, Level)));

            }
            else if (_currentLetter == GtConfig.Instance.STARTLETTER)
            {
                NodeStatus = status.start;
                Console.WriteLine($"Pos:{_checkPos} hit Start!.");
            }
            else if (_currentLetter == GtConfig.Instance.ENDLETTER)
            {
                NodeStatus = status.end;
                Console.WriteLine($"Pos:{_checkPos} Hit end at level : {Level}!.");
                Console.WriteLine($"<... Path ...>");
                int _r = 0;
                foreach (var item in LocalPath)
                {
                    _r++;
                    string[] _p = item.Split(':');
                    Console.WriteLine($"{_r} . {item} : L:{GtConfig.Instance.Matrix[int.Parse(_p[0]), int.Parse(_p[1])]}");
                }
                Console.WriteLine($"<... End ...>");

            }
            else Console.WriteLine($"Dead end.");
        }

        internal int GetEnd(int deepth)
        {
            int shortest = int.MaxValue;
            foreach (var node in Nodes)
            {
                if (node.Item2.NodeStatus == status.end) shortest = Math.Min(shortest, deepth + 1);
                int _rc = node.Item2.GetEnd(deepth + 1);
                if (_rc < int.MaxValue) shortest = Math.Min(shortest, deepth + 1);
            }

            return shortest;
        }

    }
}