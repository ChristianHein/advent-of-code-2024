using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day03;

public class Day3(string[] input) : Puzzle(input)
{
    public override int Number => 3;

    private List<(int, int)> ParseInputForMulInstructions()
    {
        var result = new List<(int, int)>();
        var mulRegex = new Regex(@"mul\((\d+),(\d+)\)");
        foreach (Match match in mulRegex.Matches(Input.Aggregate((a, b) => a + b)))
        {
            Debug.Assert(match.Groups.Count == 3);
            result.Add((int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)));
        }

        return result;
    }

    private List<(int, int)> ParseInputForAllInstructions()
    {
        var isMulInstructionEnabled = true;
        var result = new List<(int, int)>();
        var mulRegex = new Regex(@"(do)\(\)|(mul)\((\d+),(\d+)\)|(don't)\(\)");
        foreach (Match match in mulRegex.Matches(Input.Aggregate((a, b) => a + b)))
        {
            Debug.Assert(match.Groups.Count is 6);

            if (match.Groups[2].Value == "mul")
            {
                if (!isMulInstructionEnabled)
                    continue;
                result.Add((int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value)));
            }
            else if (match.Groups[1].Value == "do")
            {
                isMulInstructionEnabled = true;
            }
            else if (match.Groups[5].Value == "don't")
            {
                isMulInstructionEnabled = false;
            }
        }

        return result;
    }

    public override string Part1Solution()
    {
        var mulInstructions = ParseInputForMulInstructions();
        return mulInstructions.Select(instr => instr.Item1 * instr.Item2).Sum().ToString();
    }

    public override string Part2Solution()
    {
        var mulInstructions = ParseInputForAllInstructions();
        return mulInstructions.Select(instr => instr.Item1 * instr.Item2).Sum().ToString();
    }
}
