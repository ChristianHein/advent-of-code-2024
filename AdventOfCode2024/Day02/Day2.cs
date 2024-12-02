namespace AdventOfCode2024.Day02;

public class Day2(string[] input) : Puzzle(input)
{
    public override int Number => 2;

    private List<List<int>> ParseInput()
    {
        var reports = new List<List<int>>(Input.Length);
        foreach (var row in Input)
        {
            var report = row.Split(null).Select(int.Parse).ToList();
            reports.Add(report);
        }

        return reports;
    }

    public static bool IsReportSafe(List<int> report)
    {
        if (report.Count == 0)
        {
            return true;
        }

        var levelsAscending = new List<int>(report);
        levelsAscending.Sort();
        var levelsDescending = new List<int>(levelsAscending);
        levelsDescending.Reverse();

        if (!report.SequenceEqual(levelsAscending) && !report.SequenceEqual(levelsDescending))
        {
            return false;
        }

        for (var i = 0; i < report.Count - 1; i++)
        {
            var difference = Math.Abs(report[i] - report[i + 1]);
            if (difference is 0 or > 3)
            {
                return false;
            }
        }

        return true;
    }

    public static bool IsReportSafeWithinTolerance(List<int> report)
    {
        if (IsReportSafe(report))
        {
            return true;
        }

        for (var i = 0; i < report.Count; i++)
        {
            var reportMissingLevel = new List<int>(report);
            reportMissingLevel.RemoveAt(i);
            if (IsReportSafe(reportMissingLevel))
            {
                return true;
            }
        }

        return false;
    }


    public override string Part1Solution()
    {
        var reports = ParseInput();
        return reports.Count(IsReportSafe).ToString();
    }

    public override string Part2Solution()
    {
        var reports = ParseInput();
        return reports.Count(IsReportSafeWithinTolerance).ToString();
    }
}
