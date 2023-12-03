using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023_03
{
    internal class gtNumber
    {
        public string NumberStr { get; set; }
        public int Number { get { return int.Parse(NumberStr); } }
        public int Row { get; set; }
        public int ColStart { get; set; }
        public int ColEnd { get; set; }
        public int Numlength { get { return NumberStr.Length; } }
        public gtNumber(int row, int colStart, int colEnd, string numberStr)
        {
            this.Row = row;
            this.ColStart = colStart;
            this.ColEnd = colEnd;
            this.NumberStr = numberStr;
        }
    }
}
