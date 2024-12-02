using System.Collections.Generic;
using AdventOfCode2024.Day02;
using NUnit.Framework;

namespace AdventOfCode2024.Tests.Day02;

[TestFixture]
[TestOf(typeof(Day2))]
public class Day2Test
{

    [Test]
    public void ReportWithSingleElement_IsReportSafe_ReturnsTrue()
    {
        Assert.That(Day2.IsReportSafe([12]), Is.True);
        Assert.That(Day2.IsReportSafe([0]), Is.True);
    }

    [Test]
    public void ReportWithIncrementingElements_IsReportSafe_ReturnsTrue()
    {
        Assert.That(Day2.IsReportSafe([12, 13, 14, 15, 16]), Is.True);
    }

    [Test]
    public void ReportWithDecrementingElements_IsReportSafe_ReturnsTrue()
    {
        Assert.That(Day2.IsReportSafe([12, 11, 10, 9, 8]), Is.True);
    }

    [Test]
    public void ReportWithIncrementingAndDecrementingElements_IsReportSafe_ReturnsFalse()
    {
        Assert.That(Day2.IsReportSafe([12, 11, 12, 13, 12]), Is.False);
    }

    [Test]
    public void ReportWithRepeatingElements_IsReportSafe_ReturnsFalse()
    {
        Assert.That(Day2.IsReportSafe([12, 12]), Is.False);
        Assert.That(Day2.IsReportSafe([12, 11, 11, 10]), Is.False);
    }

    [Test]
    public void ReportWithSafeGaps_IsReportSafe_ReturnsTrue()
    {
        Assert.That(Day2.IsReportSafe([12, 13]), Is.True);
        Assert.That(Day2.IsReportSafe([12, 14]), Is.True);
        Assert.That(Day2.IsReportSafe([12, 15]), Is.True);

        Assert.That(Day2.IsReportSafe([15, 14]), Is.True);
        Assert.That(Day2.IsReportSafe([15, 13]), Is.True);
        Assert.That(Day2.IsReportSafe([15, 12]), Is.True);
    }

    [Test]
    public void ReportWithUnsafeGaps_IsReportSafe_ReturnsFalse()
    {
        Assert.That(Day2.IsReportSafe([12, 16]), Is.False);
        Assert.That(Day2.IsReportSafe([12, 17]), Is.False);
        Assert.That(Day2.IsReportSafe([12, 18]), Is.False);

        Assert.That(Day2.IsReportSafe([15, 11]), Is.False);
        Assert.That(Day2.IsReportSafe([15, 10]), Is.False);
        Assert.That(Day2.IsReportSafe([15, 9]), Is.False);

        Assert.That(Day2.IsReportSafe([15, 15]), Is.False);
    }
}
