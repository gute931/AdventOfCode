using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace _2023_10
{
    public class CoordinateGroup
    {
        public (int, int) From { get; set; }
        public (int, int) Current { get; set; }
        public (int, int) To { get; set; }
        private (int, int) North = (-1, 0);
        private (int, int) South = (+1, 0);
        private (int, int) East = (0, -1);
        private (int, int) West = (0, +1);
        public int Level { get; set; } = 0;

        public string compstr { get { return $"{Current.Item1}:{Current.Item2}"; } }

        public char NextNavigationSymbol;

        public CoordinateGroup((int, int) current)
        {
            Current = current;
        }

        public CoordinateGroup((int, int) from, (int, int) current)
        {
            From = from;
            Current = current;
        }

        public (int, int) Add((int, int) Position, gtDirection N1)
        {
            (int, int) _r = (0, 0);
            switch (N1)
            {
                case gtDirection.North:
                    _r.Item1 = Position.Item1 + North.Item1;
                    _r.Item2 = Position.Item2 + North.Item2;
                    break;
                case gtDirection.South:
                    _r.Item1 = Position.Item1 + South.Item1;
                    _r.Item2 = Position.Item2 + South.Item2;
                    break;
                case gtDirection.East:
                    _r.Item1 = Position.Item1 + East.Item1;
                    _r.Item2 = Position.Item2 + East.Item2;
                    break;
                case gtDirection.West:
                    _r.Item1 = Position.Item1 + West.Item1;
                    _r.Item2 = Position.Item2 + West.Item2;
                    break;


            }
            return _r;
        }

        public void moveForward(string[] dataArray)
        {
            Level++;
            switch (dataArray[Current.Item1][Current.Item2])
            {
                case '|':
                    if (GtConfig.Instance.InRangeFromTo(North, South))
                    {
                        // | is a vertical pipe connecting north and south.
                        if (!Add(Current, gtDirection.North).Equals(From)) Navigate(gtDirection.North);
                        else if (!Add(Current, gtDirection.South).Equals(From)) Navigate(gtDirection.South);
                    }
                    else
                    {
                        Console.WriteLine("out of bounds!");
                    }
                    break;
                case '-':
                    // - is a horizontal pipe connecting east and west.
                    if (GtConfig.Instance.InRangeFromTo(East, West))
                    {
                        if (!Add(Current, gtDirection.East).Equals(From)) Navigate(gtDirection.East);
                        else if (!Add(Current, gtDirection.West).Equals(From)) Navigate(gtDirection.West);
                    }
                    else
                    {
                        Console.WriteLine("out of bounds!");
                    }
                    break;
                case 'L':
                    // L is a 90 - degree bend connecting north and east.
                    if (GtConfig.Instance.InRangeFromTo(North, East))
                    {
                        if (!Add(Current, gtDirection.North).Equals(From)) Navigate(gtDirection.North);
                        else if (!Add(Current, gtDirection.East).Equals(From)) Navigate(gtDirection.East);
                    }
                    else
                    {
                        Console.WriteLine("out of bounds!");
                    }
                    break;
                case 'J':
                    // J is a 90 - degree bend connecting north and west.
                    if (GtConfig.Instance.InRangeFromTo(North, West))
                    {
                        if (!Add(Current, gtDirection.North).Equals(From)) Navigate(gtDirection.North);
                        else if (!Add(Current, gtDirection.West).Equals(From)) Navigate(gtDirection.West);
                    }
                    else
                    {
                        Console.WriteLine("out of bounds!");
                    }
                    break;
                case '7':
                    // 7 is a 90 - degree bend connecting south and west.
                    if (GtConfig.Instance.InRangeFromTo(South, West))
                    {
                        if (!Add(Current, gtDirection.South).Equals(From)) Navigate(gtDirection.South);
                        else if (!Add(Current, gtDirection.West).Equals(From)) Navigate(gtDirection.West);
                    }
                    else
                    {
                        Console.WriteLine("out of bounds!");
                    }
                    break;
                case 'F':
                    // F is a 90 - degree bend connecting south and east.
                    if (GtConfig.Instance.InRangeFromTo(South, West))
                    {
                        if (!Add(Current, gtDirection.South).Equals(From)) Navigate(gtDirection.South);
                        else if (!Add(Current, gtDirection.East).Equals(From)) Navigate(gtDirection.East);
                    }
                    else
                    {
                        Console.WriteLine("out of bounds!");
                    }
                    break;
                case 'S':
                    // S is the starting position of the animal; there is a pipe on this tile, but your sketch doesn't show what shape the pipe has.
                    Console.WriteLine("S........");
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

        private void Navigate(gtDirection dir)
        {
            {
                From = Current;
                Current = Add(Current, dir);
                Level++;
            }

        }
    }
}
