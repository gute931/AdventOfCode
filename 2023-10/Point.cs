using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023_10
{
    public class Point
    {
        public int Row;
        public int Col;
        public char Symbol;
        public string Boxchar;


        public Point(int item1, int item2, char symbol)
        {
            Row = item1;
            Col = item2;
            Symbol = symbol;
            switch (Symbol)
            {
                case 'S': Boxchar = "\u254B"; break;
                case '-': Boxchar = "\u2501"; break;
                case '|': Boxchar = "\u2503"; break;
                case 'F': Boxchar = "\u250F"; break;
                case 'J': Boxchar = "\u251B"; break;
                case '7': Boxchar = "\u2513"; break;
                case 'L': Boxchar = "\u2517"; break;
                default:
                    break;
            }
        }
    }
}
