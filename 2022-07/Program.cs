// See https://aka.ms/new-console-template for more information

using System.Text;

Console.WriteLine("Uppgift 2022-12-07!");

string[] _filedata = File.ReadAllLines("./data.txt");




SortedDictionary<string, int> dirData = new SortedDictionary<string, int>();
SortedDictionary<string, int> dirDataSum = new SortedDictionary<string, int>();

Stack<string> _dirs = new Stack<string>();


_dirs.Push("root");

string _currentPath = String.Join("/", _dirs);
dirData.Add(_currentPath, 0);
int _row = 0;
foreach (var _rad in _filedata)
{
    _row++;

    string[] _tranactionParts = _rad.Split(' ');
    switch (_tranactionParts[0])
    {
        case "$":
            switch (_tranactionParts[1])
            {
                case "cd":
                    switch (_tranactionParts[2])
                    {
                        case "/":
                            _dirs.Clear();
                            _dirs.Push("root");
                            _currentPath = String.Join("/", _dirs.Reverse());
                            break;
                        case "..":
                            _dirs.Pop();
                            _currentPath = String.Join("/", _dirs.Reverse());
                            break;
                        default:
                            _dirs.Push(_tranactionParts[2]);
                            _currentPath = String.Join("/", _dirs.Reverse());
                            break;
                    }
                    break;
                default:
                    break;
            }
            break;
        case "dir":
            // Skapa directory
            _currentPath = String.Join("/", _dirs.Reverse());

            dirData.Add($"{_currentPath}/{_tranactionParts[1]}", 0);
            break;
        default:
            dirData[_currentPath] += int.Parse(_tranactionParts[0]);
            break;
    }
}


foreach (var item in dirData)
{
    int _summa = dirData.Where(d => d.Key.Contains(item.Key)).Sum(s => s.Value);
    dirDataSum.Add(item.Key, _summa);
}

int s1 = dirDataSum.Where(d => d.Value <= 100000).Sum(s => s.Value);

int free = 70000000 - dirDataSum["root"];
int toFreeUp = 30000000 - free;

int s2 = dirDataSum.Where(d => d.Value >= toFreeUp).OrderBy(d => d.Value).Take(1).Sum(s => s.Value);



