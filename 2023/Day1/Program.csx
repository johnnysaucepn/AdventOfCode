Part1("input1.txt");
Part2("example2.txt");

void Part1(string selectedInput)
{
    Console.WriteLine("----Part 1----");
    var lines = File.ReadAllLines(selectedInput).ToList();
    
    var answer = lines
        .Select(l=>l.Where(c=>char.IsNumber(c)))
        .Select(n=>(first:n.First(),last:n.Last()))
        .Select(p=>((p.first-'0')*10+(p.last-'0')))
        .Sum();


    Console.WriteLine("----Part 1 Answer----");
    Console.WriteLine(answer);
}

string WordsToNumbers(string input)
{
    var output =  input.Replace("one","1")
    .Replace("two","2")
    .Replace("three","3")
    .Replace("four","4")
    .Replace("five","5")
    .Replace("six","6")
    .Replace("seven","7")
    .Replace("eight","8")
    .Replace("nine","9")
    .Replace("zero","0");

    Console.WriteLine(output);
    return output;

}

void Part2(string selectedInput)
{
    Console.WriteLine("----Part 2----");
    var lines = File.ReadAllLines(selectedInput).ToList();

    var answer = lines
        .Select(WordsToNumbers)
        .Select(l=>l.Where(c=>char.IsNumber(c)))
        .Select(n=>(first:n.First(),last:n.Last()))
        .Select(p=>((p.first-'0')*10+(p.last-'0')))
        .Sum();


    Console.WriteLine("----Part 2 Answer----");
    Console.WriteLine(answer);
}