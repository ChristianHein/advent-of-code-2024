using System.Diagnostics;
using System.Text.RegularExpressions;
using static AdventOfCode2024.Utils.DirectionUtils;

namespace AdventOfCode2024.Day13;

public partial class Puzzle(string[] input) : BasePuzzle(input)
{
    public override int Number => 13;

    private const byte TokenPriceButtonA = 3;
    private const byte TokenPriceButtonB = 1;

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
        var x1 = clawMachine.ButtonATranslation.x;
        var y1 = clawMachine.ButtonATranslation.y;

        var x2 = clawMachine.ButtonBTranslation.x;
        var y2 = clawMachine.ButtonBTranslation.y;

        var x3 = clawMachine.PrizeTarget.x;
        var y3 = clawMachine.PrizeTarget.y;

        var aNumerator = x2 * y3 - x3 * y2;
        var bNumerator = x3 * y1 - x1 * y3;
        var denominator = x2 * y1 - x1 * y2;

        // Assumption: Non-zero denominator never occurs in input. (A proper implementation would handle this case.)
        Debug.Assert(denominator != 0);

        if (aNumerator % denominator != 0 ||
            bNumerator % denominator != 0)
        {
            // A solution exists, but it is not an integer solution
            return null;
        }

        var a = aNumerator / denominator;
        var b = bNumerator / denominator;

        return TokenPriceButtonA * a + TokenPriceButtonB * b;
    }

    public override string Part1Solution()
    {
        var clawMachines = ParseInput(Input);

        var minimumTokensSum = clawMachines
            .Sum(clawMachine => MinimumTokensToSpendToWin(clawMachine) ?? 0L);

        return minimumTokensSum.ToString();
    }

    public override string Part2Solution()
    {
        var clawMachines = ParseInput(Input);

        for (var i = 0; i < clawMachines.Count; i++)
        {
            clawMachines[i] = new ClawMachine
            {
                ButtonATranslation = clawMachines[i].ButtonATranslation,
                ButtonBTranslation = clawMachines[i].ButtonBTranslation,
                PrizeTarget = Translate(clawMachines[i].PrizeTarget, (10_000_000_000_000, 10_000_000_000_000))
            };
        }

        var minimumTokensSum = clawMachines
            .Sum(clawMachine => MinimumTokensToSpendToWin(clawMachine) ?? 0L);

        return minimumTokensSum.ToString();
    }
}
