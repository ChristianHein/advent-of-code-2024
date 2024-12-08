using AdventOfCode2024.Utils;

namespace AdventOfCode2024.Day08;

public class Day8(string[] input) : Puzzle(input)
{
    public override int Number => 8;

    private static (Grid2DChar grid, Dictionary<char, List<(int, int)>> antennas) ParseInput(string[] input)
    {
        var antennas = new Dictionary<char, List<(int, int)>>();
        var grid = new Grid2DChar(input);
        foreach (var coords in grid.GetCoordinatesEnumerable())
        {
            var cell = grid.GetCellValue(coords);
            if (cell == '.')
                continue;

            if (antennas.TryGetValue(cell, out var coordsList))
            {
                coordsList.Add(coords);
            }
            else
            {
                antennas.Add(cell, [coords]);
            }
        }

        return (grid, antennas);
    }

    public override string Part1Solution()
    {
        var (grid, antennas) = ParseInput(Input);

        var antinodes = new HashSet<(int, int)>();
        foreach (var (_, locations) in antennas)
        {
            foreach (var location1 in locations)
            {
                foreach (var location2 in locations)
                {
                    if (location1 == location2)
                        continue;

                    var antinode1 = DirectionUtils.Translate(location1,
                        DirectionUtils.Difference(location1, location2));
                    var antinode2 = DirectionUtils.Translate(location2,
                        DirectionUtils.Difference(location2, location1));

                    if (grid.AreCoordsValid(antinode1))
                    {
                        antinodes.Add(antinode1);
                    }

                    if (grid.AreCoordsValid(antinode2))
                    {
                        antinodes.Add(antinode2);
                    }
                }
            }
        }

        return antinodes.Count.ToString();
    }

    public override string Part2Solution()
    {
        var (grid, antennas) = ParseInput(Input);

        var antinodes = new HashSet<(int, int)>();
        foreach (var (_, locations) in antennas)
        {
            foreach (var location1 in locations)
            {
                foreach (var location2 in locations)
                {
                    if (location1 == location2)
                        continue;

                    var i = 1;
                    while (true)
                    {
                        var antinode1 = DirectionUtils.Translate(location1,
                            DirectionUtils.Scale(DirectionUtils.Difference(location1, location2), i));
                        if (grid.AreCoordsValid(antinode1))
                        {
                            antinodes.Add(antinode1);
                            i++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    var j = 1;
                    while (true)
                    {
                        var antinode2 = DirectionUtils.Translate(location2,
                            DirectionUtils.Scale(DirectionUtils.Difference(location2, location1), j));
                        if (grid.AreCoordsValid(antinode2))
                        {
                            antinodes.Add(antinode2);
                            j++;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            locations.ForEach(loc => antinodes.Add(loc));
        }

        return antinodes.Count.ToString();
    }
}
