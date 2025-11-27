using System.Text.RegularExpressions;
using Utilities;

namespace Year2023
{
    public class Day2
    {
        public record CubeSet
        {
            public int Red = 0;
            public int Green = 0;
            public int Blue = 0;

            public CubeSet(int r, int g, int b)
            {
                Red = r;
                Green = g;
                Blue = b;
            }
        }

        public record Game
        {
            public int GameNumber = 0;
            public List<CubeSet> Rounds = [];
            public Game(int number, List<CubeSet> rounds)
            {
                GameNumber = number;
                Rounds = rounds;
            }
        }

        public static CubeSet ParseSet(string setString)
        {
            var redMatch = Regex.Match(setString, @"(?<count>\d+) red");
            var reds = redMatch.Success ? int.Parse(redMatch.Groups["count"].Value) : 0;
            var greenMatch = Regex.Match(setString, @"(?<count>\d+) green");
            var greens = greenMatch.Success ? int.Parse(greenMatch.Groups["count"].Value) : 0;
            var blueMatch = Regex.Match(setString, @"(?<count>\d+) blue");
            var blues = blueMatch.Success ? int.Parse(blueMatch.Groups["count"].Value) : 0;

            return new CubeSet(reds, greens, blues);
        }

        public static List<CubeSet> ParseRounds(string roundsString)
        {
            var roundStrings = roundsString.Split(';').Select(x => x.Trim());

            return roundStrings.Select(ParseSet).ToList();

        }

        public static Game ParseGame(string gameString)
        {
            var match = Regex.Match(gameString, @"^Game (?<number>\d+): (?<round>.*)$");
            var gameNumber = int.Parse(match.Groups["number"].Value);
            var rounds = match.Groups["round"].Value;

            return new Game(gameNumber, ParseRounds(rounds));
        }

        public static bool IsGamePossible(CubeSet bag, Game game)
        {
            if (bag.Red < game.Rounds.Max(r => r.Red)) return false;
            if (bag.Green < game.Rounds.Max(r => r.Green)) return false;
            if (bag.Blue < game.Rounds.Max(r => r.Blue)) return false;

            return true;

        }

        public static int Part1(List<string> lines)
        {
            var cubeBag = new CubeSet(12, 13, 14);

            var games = lines
                .Select(ParseGame)
                .Where(game => IsGamePossible(cubeBag, game));

            return games.Sum(g => g.GameNumber);
        }

        public static int Part2(List<string> lines)
        {
            var games = lines
                .Select(ParseGame);

            var powers = games.Select(g =>
            {
                var maxReds = g.Rounds.Max(r => r.Red);
                var maxGreens = g.Rounds.Max(r => r.Green);
                var maxBlues = g.Rounds.Max(r => r.Blue);

                var power = maxReds * maxGreens * maxBlues;
                return power;
            });

            return powers.Sum();
        }

        [Fact]
        public void Day2_Part1_Example1()
        {
            Assert.Equal(8, Part1(Input.Strings(@"day2example.txt")));
        }

        [Fact]
        public void Day2_Part2_Example2()
        {
            Assert.Equal(2286, Part2(Input.Strings(@"day2example.txt")));
        }

        [Theory]
        [InlineData("Game 12: 18 red, 16 blue, 9 green; 10 green, 6 blue; 12 blue, 5 green, 15 red; 16 blue, 4 red, 8 green", 12, 37, 32, 50)]
        [InlineData("Game 30: 4 green, 5 blue, 1 red; 19 red, 18 blue, 3 green; 18 red, 18 blue, 1 green; 5 green, 14 blue, 4 red; 4 red, 3 green, 18 blue; 6 blue, 3 green, 17 red", 30, 63, 19, 79)]
        public void Day2_ParseGame(string line, int expectedNumber, int expectedTotalRed, int expectedTotalGreen, int expectedTotalBlue)
        {
            var game = ParseGame(line);

            Assert.Equal(expectedNumber, game.GameNumber);
            Assert.Equal(expectedTotalRed, game.Rounds.Sum(x => x.Red));
            Assert.Equal(expectedTotalGreen, game.Rounds.Sum(x => x.Green));
            Assert.Equal(expectedTotalBlue, game.Rounds.Sum(x => x.Blue));
        }

    }
}
