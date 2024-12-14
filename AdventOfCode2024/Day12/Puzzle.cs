using AdventOfCode2024.Utils;

namespace AdventOfCode2024.Day12;

public class Puzzle(string[] input) : BasePuzzle(input)
{
    public override int Number => 12;

    private struct Region
    {
        public int Area;
        public int Perimeter;
        public int Sides;
    }

    private static List<Region> GetRegions<T>(Grid2D<T> grid)
        where T : notnull
    {
        var regions = new List<Region>();

        var plantPerimeters = new Dictionary<(int, int), List<(int, int)>>();
        foreach (var coords in grid.GetCoordinatesEnumerable())
        {
            if (!plantPerimeters.TryAdd(coords, []))
            {
                continue;
            }

            var regionArea = 0;
            var regionPerimeter = 0;
            var regionSides = 0;

            var next = new Stack<(int, int)>([coords]);
            while (next.Count > 0)
            {
                var current = next.Pop();

                var regionNeighbors = new List<(int, int)>();

                var currentLabel = grid.GetCellValue(current);
                var currentPerimeters = new List<(int, int)>();

                foreach (var direction in DirectionUtils.CardinalDirections)
                {
                    var neighbor = DirectionUtils.Translate(current, direction);
                    if (grid.AreCoordsValid(neighbor) && grid.GetCellValue(neighbor).Equals(currentLabel))
                    {
                        regionNeighbors.Add(neighbor);
                        if (!plantPerimeters.ContainsKey(neighbor) && !next.Contains(neighbor))
                        {
                            next.Push(neighbor);
                        }
                    }
                    else
                    {
                        currentPerimeters.Add(direction);
                    }
                }

                var newPerimeterSides = currentPerimeters.Count;
                foreach (var perimeter in currentPerimeters)
                {
                    foreach (var neighbor in regionNeighbors)
                    {
                        if (plantPerimeters.TryGetValue(neighbor, out var values) && values.Contains(perimeter))
                        {
                            newPerimeterSides--;
                        }
                    }
                }

                plantPerimeters[current] = currentPerimeters;

                regionArea++;
                const int maxPerimeterPerSquare = 4;
                regionPerimeter += maxPerimeterPerSquare - regionNeighbors.Count;
                regionSides += newPerimeterSides;
            }

            regions.Add(new Region
            {
                Area = regionArea,
                Perimeter = regionPerimeter,
                Sides = regionSides
            });
        }

        return regions;
    }

    public override string Part1Solution()
    {
        var grid = new Grid2DChar(Input);

        var regions = GetRegions(grid);
        return regions.Sum(region => region.Area * region.Perimeter).ToString();
    }

    public override string Part2Solution()
    {
        var grid = new Grid2DChar(Input);

        var regions = GetRegions(grid);
        return regions.Sum(region => region.Area * region.Sides).ToString();
    }
}
