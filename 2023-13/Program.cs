using System.Text;

Console.WriteLine("2023-13");

/*
char[] _a = "#.##..##.".ToCharArray();
char[] _b = "..##..##.".ToCharArray();
var kalle = _a.Zip(_b, (x,y) => x.Equals(y)).Where(w=>w == false);
*/

string[] Data = File.ReadAllLines("data.txt");
List<string> BlockOfDataHor = new List<string>();
List<string> BlockOfDataVert = new List<string>();
long sum1 = 0;
long sum2 = 0;
int blockCnt = 0;
int _horizontalS1 = 0;
int _verticalS1 = 0;
int _horizontalS2 = 0;
int _verticalS2 = 0;
int _odd = 0;
int _even = 0;

for (int r = 0; r < Data.Length; r++)
{
    bool check = Data[r] == "" || r == Data.Length - 1;
    if (Data[r] != "") BlockOfDataHor.Add(Data[r]);
    if (check)
    {
        blockCnt++;
        // Check current block
        _horizontalS1 = CheckHorizontal_S1(BlockOfDataHor);
        _horizontalS2 = CheckHorizontal_S2(BlockOfDataHor);
        // Console.WriteLine($"_horizontal:{_horizontal}");
        // Rotate data
        //PrintArray(BlockOfDataHor, $"{blockCnt} : Rotate Before.");
        for (int c = 0; c < BlockOfDataHor[0].Length; c++)
        {
            StringBuilder _sb = new StringBuilder();
            for (int ri = 0; ri < BlockOfDataHor.Count(); ri++)
            {
                // Console.WriteLine($"r:{ri};c:{c}");
                _sb.Append(BlockOfDataHor[ri][c]);
            }
            BlockOfDataVert.Add(_sb.ToString());
        }
        //PrintArray(BlockOfDataVert, $"{blockCnt} : Rotate After.");
        _verticalS1 = CheckHorizontal_S1(BlockOfDataVert);
        _verticalS2 = CheckHorizontal_S2(BlockOfDataVert);
        // Console.WriteLine($"_vertical:{_vertical} ");
        if (_verticalS1 == 0 && _horizontalS1 == 0)
        {
            Console.WriteLine("");
        }
        // Console.WriteLine($"bc:{blockCnt}, h:{_horizontal}, v:{_vertical}");

        sum1 += (_horizontalS1) * 100 + _verticalS1;
        sum2 += (_horizontalS2) * 100 + _verticalS2;
        /*

        switch (Int32.IsOddInteger(blockCnt))
        {
            case true:
                _odd = Math.Max(_horizontal, _vertical);
                break;
            case false:
                _even = Math.Max(_horizontal, _vertical);
                sum1 += ((_even) * 100) + _odd;

                break;
        }
        */
        _horizontalS1 = 0;
        _verticalS1 = 0;
        BlockOfDataHor = new List<string>();
        BlockOfDataVert = new List<string>();
    }




}


Console.WriteLine($"Step1:{sum1} ");
Console.WriteLine($"Step2:{sum2} ");
// 20343 low
Console.WriteLine("");

int CheckHorizontal_S2(List<string> block)
{
    int _point = 0;
    for (int i = 1; i < block.Count; i++)
    {
        _point = checkOuterS2(block, i);
        if (_point > 0) return _point;
    };
    return 0;
}


int checkOuterS2(List<string> block, int row)
{
    int TotalDiffs = 0;
    int chk = Math.Min(row, block.Count - row);
    for (int j = 0; j < chk; j++)
    {
        TotalDiffs += block[row - 1 - j].Zip(block[row + j], (x, y) => x.Equals(y)).Count(w => w == false);
    }
    return TotalDiffs == 1 ? row : 0;
}


int CheckHorizontal_S1(List<string> block)
{
    long _prev = block[0].GetHashCode();
    int _point = 0;
    string prevBlock = block[0];
    for (int i = 1; i < block.Count; i++)
    {
        string currentBlock = block[i];
        long current = currentBlock.GetHashCode();
        // Console.WriteLine($"p:{_prev},c{current},pb:{prevBlock},cb:{currentBlock}");
        if (_prev == current)
        {
            // return i + 1;
            _point += checkOuterS1(block, i);
        }
        _prev = current;
        prevBlock = currentBlock;
    };
    return _point;
}



int checkOuterS1(List<string> block, int row)
{
    int chk = Math.Min(row, block.Count - row);
    for (int j = 0; j < chk; j++)
    {
        if (block[row - 1 - j].GetHashCode() != block[row + j].GetHashCode()) return 0;
    }
    return row;

}

void PrintArray(List<string> block, string label)
{
    Console.WriteLine($"<**** {label} ****>");
    foreach (var item in block)
    {
        Console.WriteLine(item);
    }
    Console.WriteLine("<******************>");
}