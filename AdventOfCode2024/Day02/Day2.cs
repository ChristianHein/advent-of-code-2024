namespace AdventOfCode2024.Day02;

using Report = List<int>;

public class Day2(string[] input) : Puzzle(input)
{
    public override int Number => 2;

    private List<Report> ParseInput()
    {
        var reports = new List<Report>(Input.Length);
        reports.AddRange(
            Input.Select(row => row.Split(null).Select(int.Parse).ToList())
        );

        return reports;
    }

    public override string Part1Solution()
    {
        var reports = ParseInput();
        return reports.Count(ReportSafetyChecker.IsReportSafe).ToString();
    }

    public override string Part2Solution()
    {
        var reports = ParseInput();
        return reports.Count(ReportSafetyChecker.IsReportSafeWithinTolerance).ToString();
    }
}
