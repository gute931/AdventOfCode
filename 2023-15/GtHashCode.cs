using System.Text;

namespace _2023_15
{
    internal class GtHashCode
    {
        public string FullCode { get; private set; }
        public string Code { get; private set; }
        public int Index { get; private set; }
        public bool Add { get; set; }
        public int Box { get; set; }

        public GtHashCode(string code)
        {
            FullCode = code;
            Box = hash(code);
            Add = code.Contains("=");
            string[] _parts = code.Split("=-".ToCharArray());
            Code = _parts[0];
            if (Add) Index = Convert.ToInt32(_parts[1]);
        }

        public void Replace (GtHashCode code)
        {
            Index = code.Index;
        }



        int hash(string hCode)
        {
            int _sum = 0;
            foreach (char b in hCode)
            {
                _sum += b;
                _sum *= 17;
                _sum = _sum % 256;
            }
            return _sum;
        }

    }
}
