namespace AdventOfCode2024.Day07;

public class Day7(string[] input) : Puzzle(input)
{
    public override int Number => 7;

    private static List<(long, List<int>)> ParseInput(string[] input)
    {
        List<(long, List<int>)> result = [];
        foreach (var line in input)
        {
            var testValue = line.Split(": ", 2)[0];
            var equationValues = line.Split(": ", 2)[1].Split(' ');

            result.Add((long.Parse(testValue), equationValues.Select(int.Parse).ToList()));
        }

        return result;
    }

    private static bool IsSolvablePart1((long testValue, List<int> numbers) equation)
    {
        var validOperators = new List<char>(['+', '*']);

        var firstPermutation = Enumerable.Repeat(validOperators.First(), equation.numbers.Count - 1).ToList();

        var operators = new List<char>(firstPermutation);
        do
        {
            long runningVal = equation.numbers.First();
            for (var i = 1; i < equation.numbers.Count; i++)
            {
                switch (operators[i - 1])
                {
                    case '+':
                        runningVal += equation.numbers[i];
                        break;
                    case '*':
                        runningVal *= equation.numbers[i];
                        break;
                }
            }

            if (runningVal == equation.testValue)
            {
                return true;
            }

            // Next combination
            var oIndex = operators.Count - 1;
            while (oIndex >= 0)
            {
                switch (operators[oIndex])
                {
                    case '+':
                        operators[oIndex] = '*';
                        oIndex = -1;
                        break;
                    case '*':
                        operators[oIndex] = '+';
                        oIndex--;
                        break;
                }
            }
        } while (!operators.SequenceEqual(firstPermutation));

        return false;
    }

    private static bool IsSolvablePart2((long testValue, List<int> numbers) equation)
    {
        var validOperators = new List<char>(['+', '*', '|']); // TODO: actually "||"... fix later

        var firstPermutation = Enumerable.Repeat(validOperators.First(), equation.numbers.Count - 1).ToList();

        var operators = new List<char>(firstPermutation);
        do
        {
            long runningVal = equation.numbers.First();
            for (var i = 1; i < equation.numbers.Count; i++)
            {
                switch (operators[i - 1])
                {
                    case '+':
                        runningVal += equation.numbers[i];
                        break;
                    case '*':
                        runningVal *= equation.numbers[i];
                        break;
                    case '|':
                        runningVal = long.Parse(runningVal.ToString() + equation.numbers[i]);
                        break;
                }
            }

            if (runningVal == equation.testValue)
            {
                return true;
            }

            // Next combination
            var oIndex = operators.Count - 1;
            while (oIndex >= 0)
            {
                switch (operators[oIndex])
                {
                    case '+':
                        operators[oIndex] = '*';
                        oIndex = -1;
                        break;
                    case '*':
                        operators[oIndex] = '|';
                        oIndex = -1;
                        break;
                    case '|':
                        operators[oIndex] = '+';
                        oIndex--;
                        break;
                }
            }
        } while (!operators.SequenceEqual(firstPermutation));

        return false;
    }

    public override string Part1Solution()
    {
        var equations = ParseInput(Input);
        var sumOfSolvable = equations.Where(IsSolvablePart1).Select(equ => equ.Item1).Sum();
        return sumOfSolvable.ToString();
    }

    public override string Part2Solution()
    {
        var equations = ParseInput(Input);
        var sumOfSolvable = equations.Where(IsSolvablePart2).Select(equ => equ.Item1).Sum();
        return sumOfSolvable.ToString();
    }
}
