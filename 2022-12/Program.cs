using _2022_12;

Console.WriteLine("Uppgift 2022-12-12!");

Console.WriteLine($"Rows:{GtConfig.Instance.ROWS}, Cols:{GtConfig.Instance.COLS}");
Console.WriteLine($"StartRow:{GtConfig.Instance.STARTROW}, STARTCOL:{GtConfig.Instance.STARTCOL}");


List<string> localPath = new List<string>();

GtNode  _startNode = new GtNode(GtConfig.Instance.STARTROW, GtConfig.Instance.STARTCOL, 'a', localPath, 1);

int _antal = _startNode.GetEnd(1);


Console.WriteLine("End!");
Console.ReadLine();



