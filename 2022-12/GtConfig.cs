using Dijkstra.NET.Graph;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022_12
{
    public sealed class GtConfig
    {
        public char[,] Matrix { get; private set; }
        public int ROWS { get; private set; }
        public int COLS { get; private set; }
        public int STARTROW { get; private set; }
        public int STARTCOL { get; private set; }
        public int ENDROW { get; private set; }
        public int ENDCOL { get; private set; }
        public readonly char STARTLETTER = 'S';
        public readonly char ENDLETTER = 'E';

        public int SearchCnt { get; private set; } = 0;
        private GtConfig()
        {

            ParseFile("./data.txt");
        }
        private static readonly Lazy<GtConfig> lazy = new Lazy<GtConfig>(() => new GtConfig());
        public static GtConfig Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        public bool InRange(int row, int col)
        {
            return (row >= 0 && row < ROWS && col >= 0 && col < COLS);
        }
        public void IncreaseSearchCnt()
        {
            SearchCnt++;
        }
        void ParseFile(string filename)
        {
            string[] _filedata = File.ReadAllLines(filename);
            ROWS = _filedata.Length;
            COLS = 0;

            foreach (string line in _filedata)
            {
                COLS = Math.Max(COLS, line.Length);
            }

            Matrix = new char[ROWS, COLS];

            int _r = 0;
            foreach (string line in _filedata)
            {
                char[] line_c = line.ToCharArray();
                for (int _c = 0; _c < line_c.Length; _c++)
                {
                    Matrix[_r, _c] = line_c[_c];
                    // Console.Write(Matrix[_r, _c]);
                    if (line[_c] == 'S')
                    {
                        STARTROW = _r;
                        STARTCOL = _c;
                    }
                    else if (line[(int)_c] == 'E')
                    {
                        ENDROW = _r;
                        ENDCOL = _c;
                    }
                }
                // Console.WriteLine();
                _r++;
            }

        }
        internal int GenerateKey(int r, int c)
        {
            return 10000000 + (r * 10000) + c;
        }

        internal GtNode2 ValidNeighbour(int _r, int _c, int _nr, int _nc, char[] _search)
        {
            if (GtConfig.Instance.InRange(_nr , _nc))
            {
                if (_search.Contains(GtConfig.Instance.Matrix[_nr, _nc]))
                {
                    return new GtNode2(_r, _c, _nr, _nc, GtConfig.Instance.Matrix[_r, _c]);
                }
            }
            return null;
        }
    }
}
