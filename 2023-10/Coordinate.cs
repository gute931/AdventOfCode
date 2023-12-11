using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2023_10
{
    public class Coordinate
    {
        public char Symbol { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public int RowTo { get; set; }
        public int ColTo { get; set; }
        public bool StartintPoint { get; set; } = false;
        public Coordinate(char symbol, int row, int col, (int, int) to)
        {
            Symbol = symbol;
            Row = row;
            Col = col;
            RowTo = to.Item1;
            ColTo = to.Item2;
        }
        public Coordinate(char symbol, int row, int col)
        {
            StartintPoint = true;
            Symbol = symbol;
            Row = row;
            Col = col;

        }
        public moveForward()
        {
            (int, int) North = (_r - 1, _c);
            (int, int) South = (_r + 1, _c);
            (int, int) East = (_r, _c - 1);
            (int, int) West = (_r, _c + 1);

            switch (Symbol)
            {
                case '|':
                    // | is a vertical pipe connecting north and south.
                    if (GtConfig.Instance.InRangeFromTo(North, South))
                    {
                        Coordinates.Add(new Coordinate(_symbol, _r, _c, North));
                        Coordinates.Add(new Coordinate(_symbol, _r, _c, South));
                    }
                    break;
                case '-':
                    // - is a horizontal pipe connecting east and west.
                    if (GtConfig.Instance.InRangeFromTo(South, West))
                    {
                        Coordinates.Add(new Coordinate(_symbol, _r, _c, South));
                        Coordinates.Add(new Coordinate(_symbol, _r, _c, West));
                    }
                    break;
                case 'L':
                    // L is a 90 - degree bend connecting north and east.
                    if (GtConfig.Instance.InRangeFromTo(North, East))
                    {
                        Coordinates.Add(new Coordinate(_symbol, _r, _c, North));
                        Coordinates.Add(new Coordinate(_symbol, _r, _c, East));
                    }
                    break;
                case 'J':
                    // J is a 90 - degree bend connecting north and west.
                    if (GtConfig.Instance.InRangeFromTo(North, West))
                    {
                        Coordinates.Add(new Coordinate(_symbol, _r, _c, North));
                        Coordinates.Add(new Coordinate(_symbol, _r, _c, West));
                    }
                    break;
                case '7':
                    // 7 is a 90 - degree bend connecting south and west.
                    if (GtConfig.Instance.InRangeFromTo(South, West))
                    {
                        Coordinates.Add(new Coordinate(_symbol, _r, _c, South));
                        Coordinates.Add(new Coordinate(_symbol, _r, _c, West));
                    }
                    break;
                case 'F':
                    // F is a 90 - degree bend connecting south and east.
                    if (GtConfig.Instance.InRangeFromTo(South, West))
                    {
                        Coordinates.Add(new Coordinate(_symbol, _r, _c, South));
                        Coordinates.Add(new Coordinate(_symbol, _r, _c, West));
                    }
                    break;
                case 'S':
                    // S is the starting position of the animal; there is a pipe on this tile, but your sketch doesn't show what shape the pipe has.
                    _startNode = new Coordinate(_symbol, _r, _c);
                    Coordinates.Add(_startNode);
                    break;
                case '.':
                    // . is ground; there is no pipe in this tile.
                    // Console.WriteLine("No need to save thies coordinate!");
                    break;
                default:
                    // Console.WriteLine("Shouldn't stopped by here!");
                    break;
            }
        }
    }
}
