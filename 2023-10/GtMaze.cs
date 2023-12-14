using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace _2023_10
{
    public class GtMaze
    {
        public (int, int) Current { get; set; }
        private (int, int) North = (-1, 0);
        private (int, int) South = (+1, 0);
        private (int, int) East = (0, -1);
        private (int, int) West = (0, +1);
        public int Level { get; set; } = 0;
        public string compstr { get { return $"{Current.Item1}:{Current.Item2}"; } }
        public char CurrentSymbol;
        public status Status { get; set; } = status.ontrack;
        List<string> Log = new List<string>();
        public gtDirection StartDirection = gtDirection.None;
        public gtDirection EndDirection { get; private set; }
        public List<Point> Path = new List<Point>();



        public GtMaze((int, int) current, char symbol, gtDirection direction)
        {
            StartDirection = direction;
            CurrentSymbol = symbol;
            Current = current;
            PathAdd(Current, CurrentSymbol);
            EndDirection = direction;
            Log.Add($"{symbol}:{current.Item1}:{current.Item2}");
        }

        public void SavePath()
        {
            StringBuilder _sb = new StringBuilder();
            foreach (Point p in Path)
            {
                _sb.AppendLine($" [{p.Sequence}]:{p.Row}-{p.Col} {p.Symbol} : {p.Boxchar}");
            }
            File.WriteAllText(@"..\..\..\99_Path.txt", _sb.ToString());
        }


        internal void CloseLoop()
        {
            Point _s = Path.First();
            Point _e = Path.Last();

            int _diffRow = _e.Row - _s.Row;
            int _diffCol = _e.Col - _s.Col;
            char _symbol = ' ';

            if (_diffRow == -1 && _diffCol == -1) _symbol = ' ';
            else if (_diffRow == 0 && _diffCol == -1) _symbol = '-';
            else if (_diffRow == +1 && _diffCol == -1) _symbol = ' ';
            else if (_diffRow == -1 && _diffCol == 0) _symbol = '|';
            else if (_diffRow == +1 && _diffCol == 0) _symbol = ' ';
            else if (_diffRow == -1 && _diffCol == +1) _symbol = ' ';
            else if (_diffRow == 0 && _diffCol == +1) _symbol = '-';
            else if (_diffRow == +1 && _diffCol == +1) _symbol = ' ';
        }

        internal void SetFindOutside()
        {

        }


        private void PathAdd((int, int) current, char currentSymbol)
        {
            Path.Add(new Point(current.Item1, current.Item2, currentSymbol, Path.Count()));
        }

        public void Step()
        {
            Navigate(EndDirection);
            if (Status == status.ontrack) PathAdd(Current, CurrentSymbol);
        }




        public void Navigate(gtDirection direction)
        {
            Level++;
            // Console.WriteLine($"Lvl:{Level}, Direction:{direction}: CP:{Current.Item1}:{Current.Item2}, CurrentSymbol:{CurrentSymbol}");
            // Move to ordered location
            (int, int) _newPos = (-9, -9);
            switch (direction)
            {
                case gtDirection.North:
                    _newPos = (Current.Item1 + North.Item1, Current.Item2 + North.Item2);
                    break;
                case gtDirection.South:
                    _newPos = (Current.Item1 + South.Item1, Current.Item2 + South.Item2);
                    break;
                case gtDirection.East:
                    _newPos = (Current.Item1 + East.Item1, Current.Item2 + East.Item2);
                    break;
                case gtDirection.West:
                    _newPos = (Current.Item1 + West.Item1, Current.Item2 + West.Item2);
                    break;

            }

            // 
            // Console.WriteLine($"Lvl:{Level}, Direction:{direction}: NP:{_newPos.Item1}:{_newPos.Item2}");
            if (!GtConfig.Instance.InRange(_newPos.Item1, _newPos.Item2))
            {
                Status = status.deadend;
                // Console.WriteLine("Dead End!!!");
                return;
            }
            gtDirection _nextDirection = gtDirection.None;
            char _symbol = GtConfig.Instance.Data[_newPos.Item1][_newPos.Item2];
            // Console.WriteLine($"Lvl:{Level}, Direction:{direction}: NP:{_newPos.Item1}:{_newPos.Item2}, NextSymbol:{_symbol}");
            switch (_symbol)
            {
                case '|':
                    if (direction == gtDirection.South) _nextDirection = gtDirection.South;
                    else if (direction == gtDirection.North) _nextDirection = gtDirection.North;
                    break;
                case '-':
                    // - is a horizontal pipe connecting east and west.
                    if (direction == gtDirection.West) _nextDirection = gtDirection.West;
                    else if (direction == gtDirection.East) _nextDirection = gtDirection.East;
                    break;
                case 'L':
                    // L is a 90 - degree bend connecting north and east.
                    if (direction == gtDirection.South) _nextDirection = gtDirection.West;
                    else if (direction == gtDirection.East) _nextDirection = gtDirection.North;
                    break;
                case 'J':
                    // J is a 90 - degree bend connecting north and west.
                    if (direction == gtDirection.South) _nextDirection = gtDirection.East;
                    else if (direction == gtDirection.West) _nextDirection = gtDirection.North;
                    break;
                case '7':
                    // 7 is a 90 - degree bend connecting south and west.
                    if (direction == gtDirection.North) _nextDirection = gtDirection.East;
                    else if (direction == gtDirection.West) _nextDirection = gtDirection.South;
                    break;
                case 'F':
                    // F is a 90 - degree bend connecting south and east.
                    if (direction == gtDirection.North) _nextDirection = gtDirection.West;
                    else if (direction == gtDirection.East) _nextDirection = gtDirection.South;
                    break;
                case 'S':
                    // S is the starting position of the animal; there is a pipe on this tile, but your sketch doesn't show what shape the pipe has.
                    // Console.WriteLine("S........");
                    Status = status.Start;
                    _symbol = 'F';
                    break;

                case '.':
                    // . is ground; there is no pipe in this tile.
                    // Console.WriteLine("No need to save thies coordinate!");
                    Status = status.deadend;
                    break;
                default:
                    // Console.WriteLine("Shouldn't stopped by here!");
                    break;
            }
            Current = _newPos;
            CurrentSymbol = _symbol;
            EndDirection = _nextDirection;
        }

    }
}
