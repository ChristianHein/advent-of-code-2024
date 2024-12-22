using AdventOfCode2024.Utils;
using static AdventOfCode2024.Utils.DirectionUtils;

namespace AdventOfCode2024.Day08;

public class Puzzle(string[] input) : BasePuzzle(input)
{
    public override int Number => 8;

    private static (Grid2D<char> grid, Dictionary<char, List<(int, int)>> antennas) ParseInput(string[] input)
    {
        var antennas = new Dictionary<char, List<(int, int)>>();
        var grid = Grid2DCharFactory.Create(input);
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

                    var antinode1 = Translate(location1, Difference(location1, location2));
                    var antinode2 = Translate(location2, Difference(location2, location1));

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
                        var antinode1 = Translate(location1, Scale(Difference(location1, location2), i));
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
                        var antinode2 = Translate(location2, Scale(Difference(location2, location1), j));
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
