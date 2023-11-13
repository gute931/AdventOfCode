// See https://aka.ms/new-console-template for more information
Console.WriteLine("Uppgift 2022-12-02!");

string[] _rows = File.ReadAllLines("./data.txt");

// The first column is what your opponent is going to play: A for Rock, B for Paper, and C for Scissors.
// The second column, you reason, must be what you should play in response: X for Rock, Y for Paper, and Z for Scissors.
// The score for a single round is the score for the shape you selected (1 for Rock, 2 for Paper, and 3 for Scissors)
//   plus the score for the outcome of the round (0 if you lost, 3 if the round was a draw, and 6 if you won).
int _opPoints = 0;
int _myPoints1 = 0;
int _myPoints2 = 0;

int _pRock = 1;
int _pPaper = 2;
int _Scissors = 3;

int _pLost = 0;
int _pDraw = 3;
int _pWon = 6;


foreach (string _row in _rows)
{
    // Console.WriteLine(_row);
    string _opHand = _row.Substring(0, 1);
    string _myHand = _row.Substring(2, 1);
    // part 2
    switch (_row)
    {
        // Lost
        case "A X": // Rock - Rock 
            _myPoints2 += _pLost + _Scissors;
            break;
        case "B X": // Paper  - Rock
            _myPoints2 += _pLost + _pRock;
            break;
        case "C X": // Scissors - Rock
            _myPoints2 += _pLost + _pPaper;
            break;
        // Draw
        case "A Y": // Rock - Paper
            _myPoints2 += _pDraw + _pRock;
            break;
        case "B Y": // Paper - Paper
            _myPoints2 += _pDraw + _pPaper;
            break;
        case "C Y": // Scissors  - Paper
            _myPoints2 += _pDraw + _Scissors;
            break;
        // Win
        case "A Z": // Rock - Scissors
            _myPoints2 += _pWon + _pPaper;
            break;
        case "B Z": // Paper - Scissors
            _myPoints2 += _pWon + _Scissors;
            break;
        case "C Z": // Scissors - Scissors
            _myPoints2 += _pWon + _pRock;
            break;
        default:
            break;
    }
    // part 1
    switch (_row)
    {
        // Draw
        case "A X": // Rock - Rock 
            _myPoints1 += _pDraw + _pRock;    
            break;
        case "B Y": // Paper - Paper
            _myPoints1 += _pDraw + _pPaper;
            break;
        case "C Z": // Scissors - Scissors
            _myPoints1 += _pDraw + _Scissors;
            break;
        // Lost
        case "A Z": // Rock - Scissors
            _myPoints1 += _pLost + _Scissors;
            break;
        case "B X": // Paper  - Rock
            _myPoints1 += _pLost + _pRock;
            break;
        case "C Y": // Scissors  - Paper
            _myPoints1 += _pLost + _pPaper;
            break;
        // Win
        case "A Y": // Rock - Paper
            _myPoints1 += _pWon + _pPaper;
            break;
        case "B Z": // Paper - Scissors
            _myPoints1 += _pWon + _Scissors;
            break;
        case "C X": // Scissors - Rock
            _myPoints1 += _pWon + _pRock;
            break;
        default:
            break;
    }
}

Console.WriteLine($"myPoints_1 : {_myPoints1}");
Console.WriteLine($"myPoints_2 : {_myPoints2}");
Console.ReadLine();