using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023_04
{
    internal class ElfSections
    {
        string[] varden;
        
        int sec1s = 0;
        int sec1e = 0;
        int sec2s = 0;
        int sec2e = 0;

        // 25-53,6-24

        public ElfSections(string _post)
        {
            varden = _post.Split(",-".ToCharArray());
            sec1s = int.Parse(varden[0]);
            sec1e = int.Parse(varden[1]);
            sec2s = int.Parse(varden[2]);
            sec2e = int.Parse(varden[3]);
        }

        public bool Overlap()
        {
            return (sec1s <= sec2e && sec1e >= sec2s) 
                || (sec2s <= sec1e && sec2e >= sec1s)
                ;
        }
    }
}
