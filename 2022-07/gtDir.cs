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
        public int TotalFileSize { get; set; }
        public GtDir(string directoryName)
        {
            DirectoryName = directoryName;
        }
        GtDir currentDir;
        public void CreateDirectory(string _dir)
        {
            currentDir = new GtDir(_dir);
            Directories.Add(_dir, currentDir);

        }


        public void AddFile(string _file, int _size)
        {
            if (currentDir == null)
            {
                if (Files.ContainsKey(_file)) Files[_file] = _size;
                else Files.Add(_file, _size);
            }
            else
            {
                if (currentDir.Files.ContainsKey(_file)) Files[_file] = _size;
                else currentDir.Files.Add(_file, _size);
            }

            TotalFileSize = GetTotalFilesizeR();
        }

        int GetTotalFilesize()
        {
            return Files.Values.Sum();
        }


        int GetTotalFilesizeR()
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
