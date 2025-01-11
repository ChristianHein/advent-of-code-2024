using System.Diagnostics;

namespace AdventOfCode2024.Day19;

public class Puzzle(string[] input) : BasePuzzle(input)
{
    public override int Number => 19;

    private static (List<string> towelPatterns, List<string> designs) ParseInput(string[] input)
    {
        var towelPatterns = input[0].Split(", ").ToList();
        Debug.Assert(input[1] == "");
        var designs = input[2..].ToList();
        return (towelPatterns, designs);
    }

    private static Dictionary<char, List<string>> GroupStringsByFirstCharacter(List<string> strings)
    {
        var dictionary = new Dictionary<char, List<string>>();
        foreach (var str in strings.Where(str => str.Length != 0))
        {
            if (dictionary.TryGetValue(str[0], out var value))
            {
                value.Add(str);
                dictionary[str[0]] = value;
            }
            else
            {
                dictionary.Add(str[0], [str]);
            }
        }

        return dictionary;
    }

    private static long NumberOfWaysToMakeDesign(Dictionary<char, List<string>> towelPatternsByStartingLetter,
        string design)
    {
        Dictionary<int, long> possibleDesignsCharIdxToWaysCache = [];

        return NumberOfWaysToMakeDesignAcc(towelPatternsByStartingLetter, design,
            possibleDesignsCharIdxToWaysCache);
    }

    private static long NumberOfWaysToMakeDesignAcc(Dictionary<char, List<string>> towelPatternsByStartingLetter,
        string design, Dictionary<int, long> possibleDesignsCharIdxToWaysCache, int charIdx = 0)
    {
        if (possibleDesignsCharIdxToWaysCache.TryGetValue(charIdx, out var ways))
        {
            return ways;
        }

        if (charIdx >= design.Length)
        {
            possibleDesignsCharIdxToWaysCache.Add(charIdx, 1);
            return 1;
        }

        var firstCharacter = design[charIdx];
        if (!towelPatternsByStartingLetter.TryGetValue(firstCharacter, out var possiblePatterns))
        {
            possibleDesignsCharIdxToWaysCache.Add(charIdx, 1);
            return 0;
        }

        var totalPossibleWays = 0L;
        foreach (var pattern in possiblePatterns.Where(design[charIdx..].StartsWith))
        {
            var possibleWays =
                NumberOfWaysToMakeDesignAcc(towelPatternsByStartingLetter, design,
                    possibleDesignsCharIdxToWaysCache, charIdx + pattern.Length);
            if (possibleWays > 0L)
            {
                if (possibleDesignsCharIdxToWaysCache.TryGetValue(charIdx, out var runningWays))
                {
                    runningWays += possibleWays;
                    possibleDesignsCharIdxToWaysCache[charIdx] = runningWays;
                }
                else
                {
                    possibleDesignsCharIdxToWaysCache.Add(charIdx, possibleWays);
                }

                totalPossibleWays += possibleWays;
            }
        }

        possibleDesignsCharIdxToWaysCache.TryAdd(charIdx, 0);

        return totalPossibleWays;
    }

    public override string Part1Solution()
    {
        var (towelPatterns, designs) = ParseInput(Input);

        var towelPatternsByFirstStripe = GroupStringsByFirstCharacter(towelPatterns);

        return designs
            .Count(design => 0 < NumberOfWaysToMakeDesign(towelPatternsByFirstStripe, design))
            .ToString();
    }

    public override string Part2Solution()
    {
        var (towelPatterns, designs) = ParseInput(Input);

        var towelPatternsByFirstStripe = GroupStringsByFirstCharacter(towelPatterns);

        return designs
            .Select(design => NumberOfWaysToMakeDesign(towelPatternsByFirstStripe, design))
            .Where(possibleWays => possibleWays > 0).Sum()
            .ToString();
    }
}
