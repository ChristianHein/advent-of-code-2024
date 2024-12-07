using System.Diagnostics;
using System.Numerics;
using AdventOfCode2024.Utils;

namespace AdventOfCode2024.Day07;

public class Day7(string[] input) : Puzzle(input)
{
    public override int Number => 7;

    private static IEnumerable<(long, List<int>)> ParseInput(string[] input)
    {
        return from line in input
            let target = line.Split(": ", 2)[0]
            let equationValues = line.Split(": ", 2)[1].Split(' ')
            select (long.Parse(target), equationValues.Select(int.Parse).ToList());
    }

    private static readonly Func<long, long, long> Add = (a, b) => a + b;
    private static readonly Func<long, long, long> Multiply = (a, b) => a * b;
    private static readonly Func<long, long, long> Append = (a, b) => long.Parse(a.ToString() + b);

    public override string Part1Solution()
    {
        var equations = ParseInput(Input);
        var sumOfSolvable = equations
            .Where(equ => IsSolvable(equ, [Add, Multiply]))
            .Select(equ => equ.Item1)
            .Sum();
        return sumOfSolvable.ToString();
    }

    public override string Part2Solution()
    {
        var equations = ParseInput(Input);
        var sumOfSolvable = equations
            .Where(equ => IsSolvable(equ, [Add, Multiply, Append]))
            .Select(equ => equ.Item1)
            .Sum();
        return sumOfSolvable.ToString();
    }

    private static bool IsSolvable((long target, List<int> numbers) equation,
        List<Func<long, long, long>> operators)
    {
        Debug.Assert(equation.numbers.Count > 0);
        Debug.Assert(operators.Count > 0);

        var totalOpCombinations = BigInteger.Pow(operators.Count, equation.numbers.Count - 1);

        var initialOpCombination = Enumerable.Repeat(operators.First(), equation.numbers.Count - 1).ToList();
        var currentOpCombo = new List<Func<long, long, long>>(initialOpCombination);

        for (var comboCount = 0; comboCount < totalOpCombinations; comboCount++)
        {
            long runningValue = equation.numbers.First();

            var i = 1;
            for (; i < equation.numbers.Count; i++)
            {
                runningValue = currentOpCombo[i - 1].Invoke(runningValue, equation.numbers[i]);
                if (runningValue > equation.target)
                {
                    break;
                }
            }

            if (runningValue == equation.target)
            {
                return true;
            }

            var indexToIncrement = i == currentOpCombo.Count + 1 ? 0 : currentOpCombo.Count - i;
            CollectionUtils.IncrementCombination(currentOpCombo, operators, indexToIncrement);

            if (indexToIncrement == 0)
            {
                var loopIterationsToSkip = (int)Math.Pow(operators.Count, indexToIncrement) - 1;
                comboCount += loopIterationsToSkip;
            }
        }

        return false;
    }
}
