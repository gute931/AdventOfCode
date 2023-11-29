using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022_12
{
    enum status { deadend, ontrack, start, end };
    internal class GtNode
    {
        public int currentLetter { get; set; }
        char start;
        char slut;
        internal List<(string, GtNode)> _nodes = new List<(string, GtNode)>();
        char[] _search = new char[2];
        status NodeStatus = status.deadend;
        List<string> localPath;
        public GtNode(int currentRow, int currentCol, int currentLetter, List<string> localPath)
        {
            this.localPath = localPath;
            localPath.Add($"{currentRow}:{currentCol}");
            _search[0] = GtConfig.Instance.PATH[currentLetter];
            _search[1] = GtConfig.Instance.PATH[currentLetter + 1];
            if (GtConfig.Instance.InRange(currentRow - 1, currentCol)) checkPosition(currentRow - 1, currentCol, localPath);
            if (GtConfig.Instance.InRange(currentRow + 1, currentCol)) checkPosition(currentRow + 1, currentCol, localPath);
            if (GtConfig.Instance.InRange(currentRow, currentCol - 1)) checkPosition(currentRow, currentCol - 1, localPath);
            if (GtConfig.Instance.InRange(currentRow, currentCol + 1)) checkPosition(currentRow, currentCol + 1, localPath);

        }
        /*
                public status check(int currentRow, int currentCol)
                {

                }
        */
        void checkPosition(int currentRow, int currentCol, List<string> localPath)
        {
            string _pos = $"{currentRow}:{currentCol}";
            GtConfig.Instance.IncreaseSearchCnt();

            Console.WriteLine($"Pos:{_pos}: Letter:{currentLetter} : LetterP : {GtConfig.Instance.Matrix[currentRow, currentCol]}");
            if (localPath.Contains(_pos))
            {
                NodeStatus = status.deadend;
                return;
            }
            if (GtConfig.Instance.Matrix[currentRow, currentCol] == GtConfig.Instance.PATH[currentLetter])
            {
                NodeStatus = status.ontrack;
                _nodes.Add(($"{currentRow}:{currentCol}", new GtNode(currentRow, currentCol, currentLetter, localPath)));

            }
            if (GtConfig.Instance.Matrix[currentRow, currentCol] == GtConfig.Instance.PATH[currentLetter + 1])
            {
                NodeStatus = status.ontrack;
                _nodes.Add(($"{currentRow}:{currentCol}", new GtNode(currentRow, currentCol, currentLetter + 1, localPath)));

            }
            else if (GtConfig.Instance.Matrix[currentRow, currentCol] == GtConfig.Instance.STARTLETTER)
            {
                NodeStatus = status.start;
                Console.WriteLine($"Pos:{_pos} hit Start!.");
            }
            else if (GtConfig.Instance.Matrix[currentRow, currentCol] == GtConfig.Instance.ENDLETTER)
            {
                NodeStatus = status.end;
                Console.WriteLine($"Pos:{_pos} hit End!.");
            }
            Console.WriteLine($"Status of search:{NodeStatus.ToString()}");
        }
    }
}