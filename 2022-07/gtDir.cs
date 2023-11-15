using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022_07
{
    internal class GtDir
    {
        SortedList<string, GtDir> Directories = new SortedList<string, GtDir>();
        public SortedList<string, GtFile> Files = new SortedList<string, GtFile>();

        public GtDir()
        {

        }

        void AddDirectory(string _dir)
        {
            Directories.Add(_dir, new GtDir());
        }


        void AddFile(string _file, int _size)
        {
            Files.Add(_file, new GtFile(_size));
        }

        int TotalFilesize()
        {
            return Files.Values.Sum(S => S.FileSize);
        }


        int TotalFilesizeR()
        {   
            int total = 0;
            foreach (var _dir in Directories)
            {
                total += _dir.Value.Files.Values.Sum(S => S.FileSize);
            }
            total += Files.Values.Sum(S => S.FileSize);

            return total
        }

    }
}
