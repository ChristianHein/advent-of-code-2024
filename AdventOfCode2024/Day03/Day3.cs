﻿using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day03;

public partial class Day3(string[] input) : Puzzle(input)
{
    public override int Number => 3;

    [GeneratedRegex(@"mul\((?<arg1>\d+),(?<arg2>\d+)\)")]
    private static partial Regex MulExprRegex();

    [GeneratedRegex(@"(?<token_do>do)\(\)|(?<token_dont>don't)\(\)|(?<token_mul>mul)\((?<arg1>\d+),(?<arg2>\d+)\)")]
    private static partial Regex AllExprRegex();

    private List<(int, int)> ParseAllMulInstructions()
    {
        var mulValuePairs = new List<(int, int)>();
        var mulRegex = MulExprRegex();
        foreach (Match match in mulRegex.Matches(string.Join("", Input)))
        {
            Debug.Assert(match.Groups.Count == 3);

            var arg1 = match.Groups["arg1"].Value;
            var arg2 = match.Groups["arg2"].Value;
            var mulPair = (int.Parse(arg1), int.Parse(arg2));
            mulValuePairs.Add(mulPair);
        }

        return mulValuePairs;
    }

    private List<(int, int)> ParseMulInstructionsConditionally()
    {
        var regex = AllExprRegex();

        var mulValuePairs = new List<(int, int)>();
        var isMulInstructionEnabled = true;

        foreach (Match match in regex.Matches(string.Join("", Input)))
        {
            Debug.Assert(match.Groups.Count == 6);

            if (match.Groups["token_do"].Value != string.Empty)
            {
                isMulInstructionEnabled = true;
            }
            else if (match.Groups["token_dont"].Value != string.Empty)
            {
                isMulInstructionEnabled = false;
            }
            else if (match.Groups["token_mul"].Value != string.Empty)
            {
                if (!isMulInstructionEnabled)
                    continue;

                var arg1 = match.Groups["arg1"].Value;
                var arg2 = match.Groups["arg2"].Value;
                var mulPair = (int.Parse(arg1), int.Parse(arg2));
                mulValuePairs.Add(mulPair);
            }
            else
            {
                throw new ArgumentException($"Token '{match.Groups["token_do"].Value}' is not a valid token.");
            }
        }

        return mulValuePairs;
    }

    public override string Part1Solution()
    {
        var mulValuePairs = ParseAllMulInstructions();
        return mulValuePairs
            .Select(val => val.Item1 * val.Item2)
            .Sum()
            .ToString();
    }

    public override string Part2Solution()
    {
        var mulValuePairs = ParseMulInstructionsConditionally();
        return mulValuePairs
            .Select(val => val.Item1 * val.Item2)
            .Sum()
            .ToString();
    }
}
