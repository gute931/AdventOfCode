using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace _2022_12
{
    internal class GtNode2
    {
        public status SearchStatus { get; set; } = status.deadend;
        public int Letter { get; set; }
        public int Level { get; }
        public int CurrentRow { get; set; }
        public int CurrentColumn { get; set; }
        public int NeighbourRow { get; set; }
        public int NeighbourColumn { get; set; }
        List<GtNode2> Nodes = new List<GtNode2>();
        List<string> SearchPathHistory = new List<string>();


        public GtNode2(int cRow, int cCol, int level, int currentChar, List<string> searchpathhistory)
        {
            this.SearchPathHistory = new List<string>(searchpathhistory);
            this.SearchPathHistory.Add($"{cRow}:{cCol}");
            this.Level = level + 1;
            this.Letter = currentChar;

            if (GtConfig.Instance.PATH.Length - 1 == 'E')
            {
                Console.WriteLine($"Found E, Level:{Level}");
                SearchStatus = status.end;
            }
            this.CurrentRow = cRow;
            this.CurrentColumn = cCol;
        }

        public int Search()
        {
           
                // Check North
                int _letterN = CheckNeighbour(this.CurrentRow - 1, this.CurrentColumn);
                // Check South
                int _letterS = CheckNeighbour(this.CurrentRow + 1, this.CurrentColumn);
                // Check West
                int _letterW = CheckNeighbour(this.CurrentRow, this.CurrentColumn - 1);
                // Check East
                int _letterE = CheckNeighbour(this.CurrentRow, this.CurrentColumn + 1);

                foreach (var _node in Nodes)
                {
                    if (!(_node.SearchStatus == status.end))
                    {
                        _node.Search();
                    }
                }

                return Nodes.Count;

      
        }

        int CheckNeighbour(int row, int col)
        {
            if (SearchPathHistory.Contains($"{row}:{col}")) return -1;

            if (!GtConfig.Instance.InRange(row, col)) return -1;
            char _cChar = GtConfig.Instance.Matrix[row, col];


            // Is char equal or next in queue

            if (GtConfig.Instance.PATH[Letter] == _cChar || GtConfig.Instance.PATH[Letter + 1] == _cChar)
            {
                Console.WriteLine($"{_cChar} at {row},{col},L{Level}");
                int fChar = Array.IndexOf(GtConfig.Instance.PATH, _cChar);
                GtNode2 _subNode = new GtNode2(row, col, Level, fChar, SearchPathHistory);
                Nodes.Add(_subNode);
                return fChar;
            }

            return -2;
        }
    }
}
