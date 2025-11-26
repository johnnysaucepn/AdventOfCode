var lines = File.ReadAllLines("input.txt");

var totalScore = 0;

foreach (var line in lines)
{
    var parts = line.Split(" ");
    var p1 = parts[0] switch
    {
        "A" => Move.Rock,
        "B" => Move.Paper,
        "C" => Move.Scissors
    };

    var o = parts[1] switch
    {
        "X" => Outcome.Lose,
        "Y" => Outcome.Draw,
        "Z" => Outcome.Win
    };

    var p2 = GetCorrectMove(p1, o);
    var moveScore = GetMoveScore(p1, p2);
    var winScore = GetWinScore(p1, p2);
    var score = moveScore + winScore;
    Console.WriteLine($"{parts[0]} {parts[1]} = {p1} {p2} ({o}) = {moveScore}+{winScore} = {score}");

    totalScore += score;
}
Console.WriteLine(totalScore);

Move GetCorrectMove(Move p1, Outcome o)
{
    return p1 switch
    {
        Move.Rock => o switch { Outcome.Lose => Move.Scissors, Outcome.Draw => Move.Rock, Outcome.Win => Move.Paper },
        Move.Paper => o switch { Outcome.Lose => Move.Rock, Outcome.Draw => Move.Paper, Outcome.Win => Move.Scissors },
        Move.Scissors => o switch { Outcome.Lose => Move.Paper, Outcome.Draw => Move.Scissors, Outcome.Win => Move.Rock },
    };
}

int GetMoveScore(Move p1, Move p2)
{
    return p2 switch
    {
        Move.Rock => 1,
        Move.Paper => 2,
        Move.Scissors => 3
    };
}
int GetWinScore(Move p1, Move p2)
{
    return p2 switch
    {
        Move.Rock => p1 switch { Move.Rock => 3, Move.Scissors => 6, Move.Paper => 0},
        Move.Paper => p1 switch { Move.Rock => 6, Move.Scissors => 0, Move.Paper => 3},
        Move.Scissors => p1 switch { Move.Rock => 0, Move.Scissors => 3, Move.Paper => 6}
    };
}
    
enum Move
{
    Rock, Paper, Scissors
};

enum Outcome
{
    Lose, Draw, Win
};