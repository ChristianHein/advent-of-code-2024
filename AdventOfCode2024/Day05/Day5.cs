using System.Diagnostics;
using System.Formats.Asn1;

namespace AdventOfCode2024.Day05;

public class Day5(string[] input) : Puzzle(input)
{
    public override int Number => 5;

    private readonly Dictionary<int, List<int>> _pageOrderingRules = new();
    private readonly List<List<int>> _manuals = [];

    private void ParseInput()
    {
        _pageOrderingRules.Clear();
        _manuals.Clear();

        var lineIndex = 0;
        while (Input[lineIndex] != "")
        {
            var left = int.Parse(Input[lineIndex].Split("|", 2)[0]);
            var right = int.Parse(Input[lineIndex].Split("|", 2)[1]);
            if (_pageOrderingRules.TryGetValue(left, out var ruleList))
            {
                ruleList.Add(right);
            }
            else
            {
                _pageOrderingRules.Add(left, [right]);
            }

            lineIndex++;
        }

        while (lineIndex < Input.Length && Input[lineIndex] == "")
        {
            lineIndex++;
        }

        while (lineIndex < Input.Length)
        {
            _manuals.Add(Input[lineIndex].Split(",").Select(int.Parse).ToList());
            lineIndex++;
        }
    }

    public static bool CorrectlyOrdered(List<int> manual, Dictionary<int, List<int>> rules)
    {
        for (var i = 0; i < manual.Count; i++)
        {
            var leftPages = manual[..i];

            if (rules.TryGetValue(manual[i], out var ruleList) && ruleList.Any(page => leftPages.Contains(page)))
            {
                return false;
            }
        }

        return true;
    }

    private static int GetMiddlePage(List<int> manual)
    {
        Debug.Assert(manual.Count % 2 == 1);
        return manual[manual.Count / 2];
    }

    private static List<int> SetCorrectOrder(List<int> manual, Dictionary<int, List<int>> pageOrderingRules)
    {
        //Console.WriteLine($"ENTERED: Set correct order: {string.Join(", ", manual)}");
        //Console.WriteLine(
        //    $"pageOrderingRules: {string.Join("\n", pageOrderingRules.Select(res => "Key " + res.Key + ": Values = [" + string.Join(", ", res.Value) + "]").ToList())}"
        //);
        do
        {
            for (var i = 0; i < manual.Count; i++)
            {
                var leftPages = manual[..i];

                if (pageOrderingRules.TryGetValue(manual[i], out var ruleList) &&
                    ruleList.Any(page => leftPages.Contains(page)))
                {
                    //Console.WriteLine($"RULELIST: {string.Join(", ", ruleList)}");
                    var temp = manual[i];
                    manual.RemoveAt(i);
                    manual.Insert(0, temp);
                    break;
                }
            }
        } while (!CorrectlyOrdered(manual, pageOrderingRules));

        //Console.WriteLine($"EXITED: Set correct order: {string.Join(", ", manual)}");
        return manual;
    }

    public override string Part1Solution()
    {
        ParseInput();
        return _manuals.Sum(manual => CorrectlyOrdered(manual, _pageOrderingRules) ? GetMiddlePage(manual) : 0)
            .ToString();
    }

    public override string Part2Solution()
    {
        ParseInput();
        var incorrectlyOrderedManuals =
            _manuals.Where(manual => !CorrectlyOrdered(manual, _pageOrderingRules)).ToList();

        var correctedManuals =
            incorrectlyOrderedManuals.Select(manual => SetCorrectOrder(new List<int>(manual), _pageOrderingRules))
                .ToList();

        Debug.Assert(incorrectlyOrderedManuals.Count == correctedManuals.Count);
        Debug.Assert(correctedManuals.All(manual => CorrectlyOrdered(manual, _pageOrderingRules)));

        return correctedManuals.Sum(GetMiddlePage).ToString();
    }
}
