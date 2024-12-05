using System.Diagnostics;

namespace AdventOfCode2024.Day05;

public class Day5(string[] input) : Puzzle(input)
{
    public override int Number => 5;

    private (Dictionary<int, List<int>>, List<List<int>>) ParseInput()
    {
        Dictionary<int, List<int>> pageOrderingRules = new();
        List<List<int>> updates = [];

        var lineIndex = 0;
        while (Input[lineIndex] != "")
        {
            var left = int.Parse(Input[lineIndex].Split("|", 2)[0]);
            var right = int.Parse(Input[lineIndex].Split("|", 2)[1]);
            if (pageOrderingRules.TryGetValue(left, out var ruleList))
            {
                ruleList.Add(right);
            }
            else
            {
                pageOrderingRules.Add(left, [right]);
            }

            lineIndex++;
        }

        while (lineIndex < Input.Length && Input[lineIndex] == "")
        {
            lineIndex++;
        }

        while (lineIndex < Input.Length)
        {
            updates.Add(Input[lineIndex].Split(",").Select(int.Parse).ToList());
            lineIndex++;
        }

        return (pageOrderingRules, updates);
    }

    public override string Part1Solution()
    {
        var (pageOrderingRules, updates) = ParseInput();
        return updates.Sum(manual => CorrectlyOrdered(manual, pageOrderingRules) ? GetMiddlePage(manual) : 0)
            .ToString();
    }

    public override string Part2Solution()
    {
        var (pageOrderingRules, updates) = ParseInput();
        var incorrectlyOrderedUpdates =
            updates.Where(update => !CorrectlyOrdered(update, pageOrderingRules)).ToList();

        var correctedUpdates =
            incorrectlyOrderedUpdates.Select(update => SetCorrectOrder(update, pageOrderingRules))
                .ToList();

        Debug.Assert(incorrectlyOrderedUpdates.Count == correctedUpdates.Count);
        Debug.Assert(correctedUpdates.All(update => CorrectlyOrdered(update, pageOrderingRules)));

        return correctedUpdates.Sum(GetMiddlePage).ToString();
    }

    public static bool CorrectlyOrdered(List<int> updates, Dictionary<int, List<int>> rules)
    {
        for (var i = 0; i < updates.Count; i++)
        {
            var leftPages = updates[..i];

            if (rules.TryGetValue(updates[i], out var validSuccessors)
                && validSuccessors.Any(page => leftPages.Contains(page)))
            {
                return false;
            }
        }

        return true;
    }

    private static int GetMiddlePage(List<int> update)
    {
        Debug.Assert(update.Count % 2 == 1);
        return update[update.Count / 2];
    }

    private static List<int> SetCorrectOrder(List<int> update, Dictionary<int, List<int>> pageOrderingRules)
    {
        update.Sort(PageOrderComparison);
        return update;

        int PageOrderComparison(int a, int b)
        {
            if (a == b)
            {
                return 0;
            }

            return pageOrderingRules.TryGetValue(a, out var validSuccessors)
                   && validSuccessors.Contains(b)
                ? -1
                : 1;
        }
    }
}
