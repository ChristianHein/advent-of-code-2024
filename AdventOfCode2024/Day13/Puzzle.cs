using System.Text.RegularExpressions;
using static AdventOfCode2024.Utils.DirectionUtils;

namespace AdventOfCode2024.Day13;

public partial class Puzzle(string[] input) : BasePuzzle(input)
{
    public override int Number => 13;

    private struct ClawMachine
    {
        public (short x, short y) ButtonATranslation;
        public (short x, short y) ButtonBTranslation;
        public (long x, long y) PrizeTarget;

        public override string ToString()
        {
            return string.Join(Environment.NewLine,
                $"Button A: X+{ButtonATranslation.x}, Y+{ButtonATranslation.y}",
                $"Button B: X+{ButtonBTranslation.x}, Y+{ButtonBTranslation.y}",
                $"Prize: X={PrizeTarget.x}, Y={PrizeTarget.y}");
        }
    }

    private const byte ButtonATokenPrice = 3;
    private const byte ButtonBTokenPrice = 1;

    [GeneratedRegex(@"Button [AB]: X\+(\d+), Y\+(\d+)")]
    private static partial Regex ButtonTranslationRegex();

    [GeneratedRegex(@"Prize: X=(\d+), Y=(\d+)")]
    private static partial Regex PrizeTargetRegex();

    private static List<ClawMachine> ParseInput(string[] input)
    {
        var clawMachines = new List<ClawMachine>();

        for (var i = 0; i < input.Length; i += 4)
        {
            var buttonATranslationMatch = ButtonTranslationRegex().Match(input[i]);
            var buttonBTranslationMatch = ButtonTranslationRegex().Match(input[i + 1]);
            var prizeTargetMatch = PrizeTargetRegex().Match(input[i + 2]);

            clawMachines.Add(new ClawMachine
            {
                ButtonATranslation = (
                    short.Parse(buttonATranslationMatch.Groups[1].Value),
                    short.Parse(buttonATranslationMatch.Groups[2].Value)),
                ButtonBTranslation = (
                    short.Parse(buttonBTranslationMatch.Groups[1].Value),
                    short.Parse(buttonBTranslationMatch.Groups[2].Value)),
                PrizeTarget = (
                    long.Parse(prizeTargetMatch.Groups[1].Value),
                    long.Parse(prizeTargetMatch.Groups[2].Value)),
            });
        }

        return clawMachines;
    }

    private static long? MinimumTokensToSpendToWin(ClawMachine clawMachine)
    {
        const int maxButtonPresses = 100;

        var solutions = new List<(long a, long b)>();
        for (var pressesA = 0L; pressesA <= maxButtonPresses; pressesA++)
        {
            for (var pressesB = 0L; pressesB <= maxButtonPresses; pressesB++)
            {
                if (Translate(
                        Scale(clawMachine.ButtonATranslation, pressesA),
                        Scale(clawMachine.ButtonBTranslation, pressesB)) == clawMachine.PrizeTarget)
                {
                    solutions.Add((pressesA, pressesB));
                }
            }
        }

        if (solutions.Count == 0)
        {
            return null;
        }

        return solutions.Min(s => ButtonATokenPrice * s.a + ButtonBTokenPrice * s.b);
    }

    public override string Part1Solution()
    {
        var clawMachines = ParseInput(Input);

        var minimumTokensSum = clawMachines
            .Select(c => (int?)MinimumTokensToSpendToWin(c))
            .Sum(minTokens => minTokens ?? 0L);

        return minimumTokensSum.ToString();
    }

    public override string Part2Solution()
    {
        return "TODO";
    }
}
