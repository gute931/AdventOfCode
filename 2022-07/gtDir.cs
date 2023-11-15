using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022_07
{
    internal class GtDir
    {
        public SortedList<string, GtDir> Directories = new SortedList<string, GtDir>();
        public SortedList<string, int> Files = new SortedList<string, int>();
        public string DirectoryName { get; set; }
        public GtDir(string directoryName)
        {
            DirectoryName = directoryName;
        }

        public GtDir CreateDirectory(string _dir)
        {
            GtDir gtDir = new GtDir(_dir);
            Directories.Add(_dir, gtDir);
            return gtDir;
        }


        public void AddFile(string _file, int _size)
        {
            Files.Add(_file, _size);
        }

        int TotalFilesize()
        {
            return Files.Values.Sum();
        }


        int TotalFilesizeR()
        {
            int total = 0;
            foreach (var _dir in Directories)
            {
                total += _dir.Value.Files.Values.Sum();
            }
            total += Files.Values.Sum();

            return total;
        }

    }
}
