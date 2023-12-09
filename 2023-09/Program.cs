Console.WriteLine("2023-09");
string[] _data = File.ReadAllLines("./data.txt");

int SUM1 = interatedata(_data,false);
int SUM2 = interatedata(_data,true);

Console.WriteLine($"SUM1:{SUM1}");
Console.WriteLine($"SUM2:{SUM2}");
Console.WriteLine();



int interatedata(string[] _data, bool backwards) {
    int _totalt = 0;
    // List<int> _row = new List<int>();
    for (int _record = 0; _record < _data.Length; _record++)
    {
        List<int> _row = _data[_record].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(Int32.Parse).ToList();

        if (backwards) _row.Reverse();

        SortedList<int, List<int>> _handlerS1 = new SortedList<int, List<int>>();
        _handlerS1.Add(_handlerS1.Count() + 1, _row);
        bool _done = false;
        while (!_done)
        {
            List<int> _newrowS1 = new List<int>();
            for (int _y = 1; _y < _row.Count(); _y++)
            {
                _newrowS1.Add(_row[_y] - _row[_y - 1]);
            }

            if (_newrowS1.Where(x => x == 0).Count() == _newrowS1.Count())
            {
                _done = true;
            }

            _handlerS1.Add(_handlerS1.Count() + 1, new List<int>(_newrowS1));
            _row = _newrowS1;
        }

        // Then go backwards, count right side
        int _sum = 0;
        _handlerS1[_handlerS1.Count].Add(0);
        for (int _r = _handlerS1.Count(); _r > 1; _r--)
        {
            List<int> _oRec = _handlerS1[_r - 1];
            List<int> _uRec = _handlerS1[_r];

            _sum = _oRec[_oRec.Count - 1] + _uRec[_oRec.Count - 1];
            _oRec.Add(_sum);
        }
        _totalt += _sum;
    }

    return _totalt;
}