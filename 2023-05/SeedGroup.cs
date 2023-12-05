using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023_05
{
    internal class SeedGroup
    {
        public long ItemFrom { get; set; }
        public long ItemTo { get; set; }
        public long Offset { get; set; }
        public SeedGroup(long from, long to)
        {
            ItemFrom = from;
            ItemTo = to;
        }
    }
}
