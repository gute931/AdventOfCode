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
        internal List<(string, GtNode)> _nodes = new List<(string, GtNode)>();
        char[] _search = new char[2];
        status NodeStatus = status.deadend;
        List<string> localPath;
        public GtNode(int currentRow, int currentCol, char currentLetter, List<string> localPath, int level)
        {
            this.Level = level + 1;
            this.localPath = new List<string>(localPath);
            this.currentLetter = currentLetter;
            MyPosition = $"{currentRow}:{currentCol}";
            if (currentRow >= 15 && currentRow <= 22 && currentCol >= 108 && currentCol <= 118) {
                Console.WriteLine($"{currentLetter}:{currentRow}:{currentCol}");
            }
            if (currentLetter=='r')
            {
               //  Console.WriteLine('r');
            }

            _search[0] = currentLetter;
            if (currentLetter == 'y')
            {
                _search[1] = 'E';
            }
            else _search[1] = (char)(((int)currentLetter) + 1);

            localPath.Add($"{currentRow}:{currentCol}");

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
                    if (!localPath.Contains($"{_row2Check}:{_col2Check}")) // Check that hasn't been searche earlier
                    {
                     
                        checkPosition(_row2Check, _col2Check);
                    }
                }
                else
                {
                    // Console.WriteLine($"s:{currentRow}:{currentCol}, t:{_row2Check}:{_col2Check}, skip");
                }
            }
        }

        void checkPosition(int currentRow, int currentCol)
        {
            if (_search.Contains(GtConfig.Instance.Matrix[currentRow, currentCol]))
            {
                NodeStatus = status.ontrack;
                // Console.WriteLine($"Pos:{MyPosition} cl:{_search[0]} -> {GtConfig.Instance.Matrix[currentRow, currentCol]}.");

                _nodes.Add(($"{currentRow}:{currentCol}", new GtNode(currentRow, currentCol, GtConfig.Instance.Matrix[currentRow, currentCol], localPath, Level)));

            }
            else if (GtConfig.Instance.Matrix[currentRow, currentCol] == GtConfig.Instance.STARTLETTER)
            {
                NodeStatus = status.start;
                Console.WriteLine($"Pos:{MyPosition} hit Start!.");
            }
            else if (GtConfig.Instance.Matrix[currentRow, currentCol] == GtConfig.Instance.ENDLETTER)
            {
                NodeStatus = status.end;
                Console.WriteLine($"Pos:{MyPosition} hit End at level : {Level}!.");
            }
        }

        internal int GetEnd(int deepth)
        {
            int shortest = int.MaxValue;
            foreach (var node in _nodes)
            {
                if (node.Item2.NodeStatus == status.end) shortest = Math.Min(shortest, deepth + 1);
                int _rc = node.Item2.GetEnd(deepth + 1);
                if (_rc < int.MaxValue) shortest = Math.Min(shortest, deepth + 1);
            }

            return shortest;
        }

    }
}