// See https://aka.ms/new-console-template for more information
using _2022_07;

Console.WriteLine("Uppgift 2022-12-07!");

string[] _filedata = File.ReadAllLines("./data.txt");

GtDir _root = new GtDir("Root");
Stack<GtDir> _dirPos = new Stack<GtDir>();

foreach (var _rad in _filedata)
{
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
                            _dirPos.Clear();
                            _dirPos.Push(_root); 
                            break;
                        case "..":
                            _dirPos.Pop();
                            break;
                        default:

                        _dirPos.Push(_dirPos.Peek().Directories[_tranactionParts[2]]);
                            break;
                    }
                    break;
                default:
                    break;
            }
            break;
        case "dir":
            // Skapa directory
            _dirPos.Peek().CreateDirectory(_tranactionParts[1]);
            break;
        default:
            _dirPos.Peek().AddFile(_tranactionParts[1], int.Parse(_tranactionParts[0]));
            break;
    }
}

Console.ReadLine();

