using AdventOfCode2024.Day02;
using NUnit.Framework;

namespace AdventOfCode2024.Tests.Day02;

[TestFixture]
[TestOf(typeof(Day2))]
public class Day2Test
{

    [Test]
    public void ReportWithSingleElement_IsReportSafe_IsTrue()
    {
        Assert.That(ReportSafetyChecker.IsReportSafe([12]), Is.True);
        Assert.That(ReportSafetyChecker.IsReportSafe([0]), Is.True);
    }

    [Test]
    public void ReportWithIncrementingElements_IsReportSafe_IsTrue()
    {
        Assert.That(ReportSafetyChecker.IsReportSafe([12, 13, 14, 15, 16]), Is.True);
    }

    [Test]
    public void ReportWithDecrementingElements_IsReportSafe_IsTrue()
    {
        Assert.That(ReportSafetyChecker.IsReportSafe([12, 11, 10, 9, 8]), Is.True);
    }

    [Test]
    public void ReportWithIncrementingAndDecrementingElements_IsReportSafe_IsFalse()
    {
        Assert.That(ReportSafetyChecker.IsReportSafe([12, 11, 12, 13, 12]), Is.False);
    }

    [Test]
    public void ReportWithRepeatingElements_IsReportSafe_IsFalse()
    {
        Assert.That(ReportSafetyChecker.IsReportSafe([12, 12]), Is.False);
        Assert.That(ReportSafetyChecker.IsReportSafe([12, 11, 11, 10]), Is.False);
    }

    [Test]
    public void ReportWithSafeGaps_IsReportSafe_IsTrue()
    {
        Assert.That(ReportSafetyChecker.IsReportSafe([12, 13]), Is.True);
        Assert.That(ReportSafetyChecker.IsReportSafe([12, 14]), Is.True);
        Assert.That(ReportSafetyChecker.IsReportSafe([12, 15]), Is.True);

        Assert.That(ReportSafetyChecker.IsReportSafe([15, 14]), Is.True);
        Assert.That(ReportSafetyChecker.IsReportSafe([15, 13]), Is.True);
        Assert.That(ReportSafetyChecker.IsReportSafe([15, 12]), Is.True);
    }

    [Test]
    public void ReportWithUnsafeGaps_IsReportSafe_IsFalse()
    {
        Assert.That(ReportSafetyChecker.IsReportSafe([12, 16]), Is.False);
        Assert.That(ReportSafetyChecker.IsReportSafe([12, 17]), Is.False);
        Assert.That(ReportSafetyChecker.IsReportSafe([12, 18]), Is.False);

        Assert.That(ReportSafetyChecker.IsReportSafe([15, 11]), Is.False);
        Assert.That(ReportSafetyChecker.IsReportSafe([15, 10]), Is.False);
        Assert.That(ReportSafetyChecker.IsReportSafe([15, 9]), Is.False);

        Assert.That(ReportSafetyChecker.IsReportSafe([15, 15]), Is.False);
    }

    [Test]
    public void ReportWithSingleOutOfPlaceElement_IsReportSafeWithinTolerance_IsTrue()
    {
        Assert.That(ReportSafetyChecker.IsReportSafe([15, 15]), Is.False);
        Assert.That(ReportSafetyChecker.IsReportSafeWithinTolerance([15, 15]), Is.True);

        Assert.That(ReportSafetyChecker.IsReportSafe([15, 14, 16, 17]), Is.False);
        Assert.That(ReportSafetyChecker.IsReportSafeWithinTolerance([15, 14, 16, 17]), Is.True);

        Assert.That(ReportSafetyChecker.IsReportSafe([0, 3, 10, 6]), Is.False);
        Assert.That(ReportSafetyChecker.IsReportSafeWithinTolerance([0, 3, 10, 6]), Is.True);
    }

    [Test]
    public void ReportWithMultipleOutOfPlaceElements_IsReportSafeWithinTolerance_IsFalse()
    {
        Assert.That(ReportSafetyChecker.IsReportSafe([15, 15, 15]), Is.False);
        Assert.That(ReportSafetyChecker.IsReportSafeWithinTolerance([15, 15, 15]), Is.False);

        Assert.That(ReportSafetyChecker.IsReportSafe([15, 14, 15, 14]), Is.False);
        Assert.That(ReportSafetyChecker.IsReportSafeWithinTolerance([15, 14, 15, 14]), Is.False);

        Assert.That(ReportSafetyChecker.IsReportSafe([0, 4, 10, 15]), Is.False);
        Assert.That(ReportSafetyChecker.IsReportSafeWithinTolerance([0, 4, 10, 15]), Is.False);
    }
}
