// See https://aka.ms/new-console-template for more information
using _2022_07;

Console.WriteLine("Uppgift 2022-12-07!");

string[] _filedata = File.ReadAllLines("./data.txt");

GtDir _root = new GtDir();
Stack<GtDir> _dirPos = new Stack<GtDir>();

foreach (var _rad in _filedata)
{
    if (_rad.StartsWith("$"))
    {

    }
    else if (_rad.EndsWith("dir"))
    {
        _root = new GtDir();
    }
    else
    {

    }

}

Console.WriteLine(_filedata);

