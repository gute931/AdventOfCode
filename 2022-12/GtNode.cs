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
        public int currentLetter { get; set; }
        char start;
        char slut;
        int Level;
        internal List<(string, GtNode)> _nodes = new List<(string, GtNode)>();
        char[] _search = new char[2];
        status NodeStatus = status.deadend;
        List<string> localPath;
        public GtNode(int currentRow, int currentCol, int currentLetter, List<string> localPath, int level)
        {
            this.Level = level + 1;
            this.localPath = localPath;
            localPath.Add($"{currentRow}:{currentCol}");
            this.currentLetter = currentLetter;
            if (GtConfig.Instance.InRange(currentRow - 1, currentCol)) checkPosition(currentRow - 1, currentCol, localPath);
            if (GtConfig.Instance.InRange(currentRow + 1, currentCol)) checkPosition(currentRow + 1, currentCol, localPath);
            if (GtConfig.Instance.InRange(currentRow, currentCol - 1)) checkPosition(currentRow, currentCol - 1, localPath);
            if (GtConfig.Instance.InRange(currentRow, currentCol + 1)) checkPosition(currentRow, currentCol + 1, localPath);

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
        void checkPosition(int currentRow, int currentCol, List<string> localPath)
        {
            string _pos = $"{currentRow}:{currentCol}";
            GtConfig.Instance.IncreaseSearchCnt();

            if (GtConfig.Instance.Matrix[currentRow, currentCol] == GtConfig.Instance.STARTLETTER)
            {
                NodeStatus = status.start;
               // Console.WriteLine($"Pos:{_pos} hit Start!.");
            }
            else if (GtConfig.Instance.Matrix[currentRow, currentCol] == GtConfig.Instance.ENDLETTER)
            {
                NodeStatus = status.end;
                Console.WriteLine($"Pos:{_pos} hit End at level : {Level}!.");
            }
            else if (localPath.Contains(_pos))
            {
                NodeStatus = status.deadend;
                return;
            }
            else if (GtConfig.Instance.Matrix[currentRow, currentCol] == GtConfig.Instance.PATH[currentLetter])
            {
                NodeStatus = status.ontrack;
                _nodes.Add(($"{currentRow}:{currentCol}", new GtNode(currentRow, currentCol, currentLetter, localPath, Level)));

            }
            else if (currentLetter + 1 < GtConfig.Instance.PATH.Length && GtConfig.Instance.Matrix[currentRow, currentCol] == GtConfig.Instance.PATH[currentLetter + 1])
            {
                NodeStatus = status.ontrack;
                _nodes.Add(($"{currentRow}:{currentCol}", new GtNode(currentRow, currentCol, currentLetter + 1, localPath, Level)));

            }
         //   Console.WriteLine($"Status of search:{NodeStatus.ToString()}");
        }
    }
}