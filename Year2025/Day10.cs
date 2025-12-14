using Utilities;
using Xunit;

namespace Year2025;

public class Day10
{
    public class Machine
    {
        public List<bool> InitialState = [];
        public List<bool> TargetState = [];
        public List<List<int>> Buttons = [];
        public List<int> Joltages = [];

        public int LightCount => TargetState.Count;
        public int ButtonsPressed = 0;

        public static Machine Parse(string line)
        {
            (var lights, var buttons, var joltages) = SplitLine(line);
            return new Machine(lights, buttons, joltages);
        }

        public Machine(string lightsRaw, List<string> buttonsRaw, string joltagesRaw)
        {
            var lightsTrimmed = lightsRaw[1..^1];
            var lightCount = lightsTrimmed.Length;

            for (var i = 0; i < lightCount; i++)
            {
                TargetState.Add(lightsTrimmed[i] == '#');
            }
            Reset();

            foreach (var buttonStr in buttonsRaw)
            {
                var buttonStrTrimmed = buttonStr[1..^1];
                var buttons = buttonStrTrimmed.Split(',').Select(int.Parse).ToList();
                Buttons.Add(buttons);
            }

            var joltagesTrimmed = joltagesRaw[1..^1];
            Joltages = joltagesTrimmed.Split(',').Select(int.Parse).ToList();
        }

        public static (string lightsString, List<string> buttonsString, string joltagesString) SplitLine(string line)
        {
            var parts = line.Split(' ');
            var lightsRaw = parts[0];
            var buttonsRaw = parts[1..^1].ToList();
            var joltagesRaw = parts[^1];
            return (lightsRaw, buttonsRaw, joltagesRaw);
        }

        public void Reset()
        {
            InitialState = Enumerable.Repeat(false, LightCount).ToList();
            ButtonsPressed = 0;
        }

        public bool IsInTargetState()
        {
            for (var i = 0; i < LightCount; i++)
            {
                if (TargetState[i] != InitialState[i]) return false;
            }
            return true;
        }

        public void PressButton(int index)
        {
            var button = Buttons[index];
            foreach (var flipIndex in button)
            {
                InitialState[flipIndex] = !InitialState[flipIndex];
            }
        }

        public List<List<int>> AllPossibleButtonPresses()
        {
            return Enumerable.Range(1, (int)Math.Pow(2, Buttons.Count) - 1)
                .Select(i =>
                {
                    return EnumerateButtonPresses(i).ToList();
                })
                .ToList();
        }

        private IEnumerable<int> EnumerateButtonPresses(int i)
        {
            for (var j = 0; j < Buttons.Count; j++)
            {
                var bit = 1 << j;
                if ((i & bit) != 0)
                {
                    yield return j;
                }
            }
        }

        public List<List<int>> FindValidButtonPresses()
        {
            var validPatterns = AllPossibleButtonPresses()
                .Where(p =>
                {
                    Reset();
                    p.ForEach(PressButton);
                    return IsInTargetState();
                })
                .ToList();
            return validPatterns;
        }

    }

    public long Part1(List<string> lines)
    {
        var machines = lines
            .Select(Machine.Parse)
            .ToList();

        var totalPresses = 0;
        foreach (var machine in machines)
        {
            List<List<int>> validPatterns = machine.FindValidButtonPresses();

            var best = validPatterns
                .OrderBy(v => v.Count) // shortest first
                .First();

            Console.WriteLine(Format.DumpList(machine.TargetState));
            Console.WriteLine(Format.DumpList(best));

            totalPresses += best.Count;
        }
        return totalPresses;
    }

    public long Part2(List<string> lines)
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void Day10_Part1_Example()
    {
        Assert.Equal(7, Part1(Input.Strings(@"day10example.txt")));
    }

    [Fact]
    public void Day10_Part2_Example()
    {
        Assert.Equal(0, Part2(Input.Strings(@"day10example.txt")));
    }

    [Fact]
    public void Machine_SplitLine()
    {
        var line = "[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}";

        (var lights, var buttons, var joltages) = Machine.SplitLine(line);
        Assert.Equal("[.##.]", lights);
        Assert.Equal(["(3)", "(1,3)", "(2)", "(2,3)", "(0,2)", "(0,1)"], buttons);
        Assert.Equal("{3,5,4,7}", joltages);
    }

    [Fact]
    public void Machine_Constructor()
    {
        var line = "[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}";
        var machine = Machine.Parse(line);

        Assert.Equal([false, true, true, false], machine.TargetState);
        Assert.Equal([[3], [1, 3], [2], [2, 3], [0, 2], [0, 1]], machine.Buttons);
        Assert.Equal([3, 5, 4, 7], machine.Joltages);
    }

    [Fact]
    public void Machine_AllPossibleButtonPresses()
    {
        var machine = new Machine("[.]", ["(3)", "(1,3)", "(2)", "(2,3)"], "{3}");

        List<List<int>> expectedButtonPresses = [[0], [1], [0, 1], [2], [0, 2], [1, 2], [0, 1, 2], [3], [0, 3], [1, 3], [0, 1, 3], [2, 3], [0, 2, 3], [1, 2, 3], [0, 1, 2, 3]];
        Assert.Equal(expectedButtonPresses, machine.AllPossibleButtonPresses().ToList());
    }

}