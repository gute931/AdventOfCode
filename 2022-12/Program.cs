using System.Diagnostics;

Console.WriteLine("Uppgift 2022-12-12!");

string[] _filedata = File.ReadAllLines("./data.txt");

int sRow = 20;
int sCol = 0;

int rows = 41;
int cols = 136;

char[] _lowerAZ = Enumerable.Range('a', 'z' - 'a' + 1).Select(i => (Char)i).ToArray();

int _charPos = 0;


