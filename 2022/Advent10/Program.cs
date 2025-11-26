using System.Text;
//Part1("input.txt");


Part2("input.txt");

void Part1(string selectedInput)
{
    Console.WriteLine("----Part 1----");
    var lines = File.ReadAllLines(selectedInput).ToList();

    var crt = new Crt();
    var totalSignal = crt.Run(lines);
    Console.WriteLine("----Part 1 Answer----");

    Console.WriteLine(totalSignal);
}

void Part2(string selectedInput)
{
    Console.WriteLine("----Part 2----");
    var lines = File.ReadAllLines(selectedInput).ToList();

    var crt = new Crt();
    var totalSignal = crt.Run(lines);

    Console.WriteLine("----Part 2 Answer----");
}

public class Crt
{

   int X = 1;
    int Cycle = 1;
    int TotalSignal = 0;
    int[] SampleCycles = new [] { 20, 60, 100, 140, 180, 220 };

    StringBuilder sb = new StringBuilder();
    

    public int Run(IEnumerable<string> lines)
    {
        foreach (var instruction in lines)
    {
        

        if (instruction.StartsWith("addx"))
        {
            DoCycle();
            var amount = Convert.ToInt32(instruction.Split(" ")[1]);
            DoCycle();
            X += amount;
        }
        else
        {
            DoCycle();
        }

    }
    return TotalSignal;
    }


    void DoCycle()
    {
        //Console.WriteLine($"Before cycle {Cycle}: register {X}");

        var position = (Cycle -1) % 40;
        var pixel1 = X-1;
        var pixel2 = X;
        var pixel3 = X+1;

        var lit = (position == pixel1 || position == pixel2 || position == pixel3);

        sb.Append(lit ? "#" : ".");

            if (sb.Length >=40)
            {
            Console.WriteLine(sb.ToString());
            sb.Clear();
            }


        if (SampleCycles.Contains(Cycle))
        {
            var signal = (Cycle * X);
            //Console.WriteLine($"Cycle {Cycle}: register {X}, signal {signal}");
            TotalSignal += signal;
        }
        Cycle++;

    }
}

enum InstructionState
{
    None,
    Noop,
    AddXStart,
    AddXEnd
}

