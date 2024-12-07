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
        if (operators.Count == 0)
        {
            return equation.numbers.First() == equation.target;
        }

        var ops = Enumerable.Repeat(operators.First(), equation.numbers.Count - 1).ToList();
        var totalOpCombinations = BigInteger.Pow(operators.Count, equation.numbers.Count - 1);
        while (totalOpCombinations-- > 0)
        {
            long runningVal = equation.numbers.First();
            for (var i = 1; i < equation.numbers.Count; i++)
            {
                runningVal = ops[i - 1].Invoke(runningVal, equation.numbers[i]);
            }

            if (runningVal == equation.target)
            {
                return true;
            }

            CollectionUtils.SetNextCombination(ops, operators);
        }

        return false;
    }
}
