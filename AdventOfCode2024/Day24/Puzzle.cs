using System.Diagnostics;

namespace AdventOfCode2024.Day24;

public class Puzzle(string[] input) : BasePuzzle(input)
{
    public override int Number => 24;

    private record Formula(Op Op, string Arg1, string Arg2, string Dest);

    private enum Op
    {
        And,
        Or,
        Xor
    }

    private static (Dictionary<string, bool> symbolsToValue, List<Formula> formulas) ParseInput(string[] input)
    {
        var symbolValues = new Dictionary<string, bool>();
        var i = 0;
        while (i < input.Length && input[i] != string.Empty)
        {
            var symbol = input[i].Split(": ", 2)[0];
            var value = byte.Parse(input[i].Split(": ", 2)[1]) != 0;
            symbolValues.Add(symbol, value);
            i++;
        }

        i++;

        var formulas = new List<Formula>();
        while (i < input.Length)
        {
            var arg1 = input[i].Split(' ')[0];
            var op = (Op)Enum.Parse(typeof(Op), input[i].Split(' ')[1], ignoreCase: true);
            var arg2 = input[i].Split(' ')[2];
            Debug.Assert(input[i].Split(' ')[3].Equals("->"));
            var dest = input[i].Split(' ')[4];
            formulas.Add(new Formula(op, arg1, arg2, dest));
            i++;
        }

        return (symbolValues, formulas);
    }

    public override string Part1Solution()
    {
        var (symbolsToValue, formulas) = ParseInput(Input);

        var unevaluatedFormulas = new List<Formula>(formulas);
        while (unevaluatedFormulas.Count > 0)
        {
            var currentFormulas = new List<Formula>(unevaluatedFormulas);
            unevaluatedFormulas.Clear();

            foreach (var formula in currentFormulas)
            {
                if (symbolsToValue.TryGetValue(formula.Arg1, out var arg1Value) &&
                    symbolsToValue.TryGetValue(formula.Arg2, out var arg2Value))
                {
                    switch (formula.Op)
                    {
                        case Op.And:
                            symbolsToValue.Add(formula.Dest, arg1Value && arg2Value);
                            break;
                        case Op.Or:
                            symbolsToValue.Add(formula.Dest, arg1Value || arg2Value);
                            break;
                        case Op.Xor:
                            symbolsToValue.Add(formula.Dest, arg1Value ^ arg2Value);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else
                {
                    unevaluatedFormulas.Add(formula);
                }
            }
        }

        var zCount = symbolsToValue.Keys.Count(symbol => symbol.StartsWith('z'));
        var z = 0UL;
        for (var zIdx = 0; zIdx < zCount; zIdx++)
        {
            z |= Convert.ToUInt64(symbolsToValue[$"z{zIdx:00}"]) << zIdx;
        }

        return z.ToString();
    }

    public override string Part2Solution()
    {
        return "TODO";
    }
}
