using System.Diagnostics;

namespace AdventOfCode2024.Day02;

using Report = List<int>;

public static class ReportSafetyChecker
{
    public static bool IsReportSafe(Report report)
    {
        return (IsAscending(report) || IsDescending(report)) && HasOnlyGapsInInclusiveRange(report, 1, 3);
    }

    public static bool IsReportSafeWithinTolerance(Report report)
    {
        if (IsReportSafe(report))
            return true;

        for (var i = 0; i < report.Count; i++)
        {
            var reportMissingLevel = new Report(report);
            reportMissingLevel.RemoveAt(i);
            if (IsReportSafe(reportMissingLevel))
                return true;
        }

        return false;
    }

    private static bool IsAscending(Report report)
    {
        var reportAscending = report.Order();
        return report.SequenceEqual(reportAscending);
    }

    private static bool IsDescending(Report report)
    {
        var reportDescending = report.OrderDescending();
        return report.SequenceEqual(reportDescending);
    }

    private static bool HasOnlyGapsInInclusiveRange(Report report, int minimum, int maximum)
    {
        Debug.Assert(minimum >= 0);

        for (var i = 0; i < report.Count - 1; i++)
        {
            var difference = Math.Abs(report[i] - report[i + 1]);
            if (difference < minimum || difference > maximum)
                return false;
        }

        return true;
    }
}
