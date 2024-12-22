using System.Text.RegularExpressions;
using AdventOfCode2024.Utils;

namespace AdventOfCode2024.Day14;

public partial class Puzzle(int width, int height, string[] input) : BasePuzzle(input)
{
    public override int Number => 14;

    private struct Robot
    {
        public (int row, int col) Position { get; set; }
        public (int row, int col) Velocity { get; init; }
    }

    [GeneratedRegex(@"p=(\d+),(\d+) v=(-?\d+),(-?\d+)")]
    private static partial Regex RobotStateRegex();

    private static Robot[] ParseInput(string[] input)
    {
        var robots = new Robot[input.Length];
        for (var i = 0; i < input.Length; i++)
        {
            var regex = RobotStateRegex();
            var match = regex.Match(input[i]);

            var posX = int.Parse(match.Groups[1].Value);
            var posY = int.Parse(match.Groups[2].Value);
            var velX = int.Parse(match.Groups[3].Value);
            var velY = int.Parse(match.Groups[4].Value);

            robots[i] = new Robot
            {
                Position = (posY, posX),
                Velocity = (velY, velX)
            };
        }

        return robots;
    }

    private static void SimulateSpace(int width, int height, ref Robot[] robots)
    {
        for (var i = 0; i < robots.Length; i++)
        {
            (int row, int col) newPos = DirectionUtils.Translate(robots[i].Position, robots[i].Velocity);
            newPos.row = (newPos.row + height) % height;
            newPos.col = (newPos.col + width) % width;

            robots[i].Position = newPos;
        }
    }

    private static int CalculateSafetyFactor(int width, int height, Robot[] robots)
    {
        // Quadrants:
        //   1 | 2
        //   -----
        //   3 | 4
        var quadrant1Corners = ((0, 0), (height / 2 - 1, width / 2 - 1));
        var quadrant2Corners = ((0, width / 2 + 1), (height / 2 - 1, width - 1));
        var quadrant3Corners = ((height / 2 + 1, 0), (height - 1, width / 2 - 1));
        var quadrant4Corners = ((height / 2 + 1, width / 2 + 1), (height - 1, width - 1));

        var quadrants = new ((int row, int col) topLeft, (int row, int col) bottomRight)[]
            { quadrant1Corners, quadrant2Corners, quadrant3Corners, quadrant4Corners };

        var safetyFactor = 1;
        foreach (var quadrant in quadrants)
        {
            var robotsInQuadrant = robots.AsEnumerable()
                .Count(robot => robot.Position.row >= quadrant.topLeft.row &&
                                robot.Position.row <= quadrant.bottomRight.row &&
                                robot.Position.col >= quadrant.topLeft.col &&
                                robot.Position.col <= quadrant.bottomRight.col);
            safetyFactor *= robotsInQuadrant;
        }

        return safetyFactor;
    }

    public override string Part1Solution()
    {
        var robots = ParseInput(Input);
        const int seconds = 100;
        for (var s = 0; s < seconds; s++)
        {
            SimulateSpace(width, height, ref robots);
        }

        return CalculateSafetyFactor(width, height, robots).ToString();
    }

    public override string Part2Solution()
    {
        var robots = ParseInput(Input);

        for (var s = 0;; s++)
        {
            SimulateSpace(width, height, ref robots);

            var grid = new Grid2D<bool>(width, height, false);
            foreach (var robot in robots)
            {
                grid.SetCellValue(robot.Position, true);
            }

            var totalConnectedNeighbors = 0;
            foreach (var robot in robots)
            {
                var neighborsCoords = grid.GetValidNeighborsCoords(robot.Position, DirectionUtils.CardinalDirections);

                var neighborsCount = neighborsCoords.Count(neighborsCoord => grid.GetCellValue(neighborsCoord));
                totalConnectedNeighbors += neighborsCount;
            }

            var averageRobotNeighbors = totalConnectedNeighbors / robots.Length;
            if (averageRobotNeighbors >= 1.5)
            {
                //Console.WriteLine(grid.ToString(x => x.ToString(), null));
                return (s + 1).ToString();
            }
        }
    }
}
