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
        public long ItemFromOrg { get; set; }
        public long ItemToOrg { get; set; }
        public long Offset { get; set; }
        public SeedGroup(long from, long to)
        {
            ItemFromOrg = from;
            ItemToOrg = to;
            ItemFrom = from;
            ItemTo = to;
        }

        public SeedGroup(long from, long to, long offset) : this(from, to)
        {
            Offset = offset;
            ItemFrom = ItemFromOrg + offset;
            ItemTo = ItemToOrg + offset;
        }
    }

}
