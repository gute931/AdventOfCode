using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022_12
{
    internal class GtNode2
    {
        public char Letter { get; set; }
        public char NextLetter { get; set; }
        public int CurrentRow { get; set; }
        public int CurrentColumn { get; set; }
        public int NeighbourRow { get; set; }
        public int NeighbourColumn { get; set; }
        
        public GtNode2(int cRow, int cCol, int nRow, int nCol, char cLetter)
        {
            CurrentRow = cRow;
            CurrentColumn = cCol;
            NeighbourRow = nRow;
            NeighbourColumn = nCol;

            Letter = cLetter;
            if (Letter == 'S') NextLetter = 'a';
            else if (Letter == 'z') NextLetter = 'E';
            else NextLetter = (char)(((int)Letter) + 1);


        }
    }
}
