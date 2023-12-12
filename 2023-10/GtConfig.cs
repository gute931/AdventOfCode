namespace _2023_10
{
    public enum status { ontrack, loop, deadend, Start };
    public sealed class GtConfig
    {

        // public List<Coordinate> Coordinates  { get; }

        public string[] Data { get; set; }
        public int ROWS { get; private set; }
        public int COLS { get; private set; }
        public int STARTROW { get; set; }
        public int STARTCOL { get; set; }
        public int SearchCnt { get; private set; } = 0;
        private GtConfig()
        {

            // ParseFile("./data.txt");
            ParseFile("./testdata1.txt");
            //  Coordinates = new List<Coordinate>();
        }
        private static readonly Lazy<GtConfig> _singleton = new Lazy<GtConfig>(() => new GtConfig());
        public static GtConfig Instance
        {
            get
            {
                return _singleton.Value;
            }
        }

        void ParseFile(string filename)
        {
            Data = File.ReadAllLines(filename);
            ROWS = Data.Length;
            COLS = 0;
            for (int _r = 0; _r < Data.Length; _r++)
            {
                COLS = Math.Max(COLS, Data[_r].Length);
                for (int _c = 0; _c < Data[_r].Length; _c++)
                {

                    if (Data[_r][_c] == 'S')
                    {
                        STARTROW = _r;
                        STARTCOL = _c;
                    }
                }
            }

        }

        public bool InRange(int row, int col)
        {
            return (row >= 0 && row < ROWS && col >= 0 && col < COLS);
        }

        internal bool InRangeFromTo((int, int) c1, (int, int) c2)
        {
            return InRange(c1.Item1, c1.Item2) && InRange(c2.Item1, c2.Item2);
        }

        public void IncreaseSearchCnt()
        {
            SearchCnt++;
        }



    }
}
