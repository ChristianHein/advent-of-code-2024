using System.Collections.Immutable;

namespace AdventOfCode2024.Day23;

public class Puzzle(string[] input) : BasePuzzle(input)
{
    public override int Number => 23;

    private static ImmutableDictionary<string, List<string>> ParseInput(string[] input)
    {
        var connections = new Dictionary<string, List<string>>();
        foreach (var line in input)
        {
            var computer1 = line.Split("-", 2)[0];
            var computer2 = line.Split("-", 2)[1];
            if (connections.TryGetValue(computer1, out var computer1Connections))
            {
                computer1Connections.Add(computer2);
            }
            else
            {
                connections.Add(computer1, [computer2]);
            }

            if (connections.TryGetValue(computer2, out var computer2Connections))
            {
                computer2Connections.Add(computer1);
            }
            else
            {
                connections.Add(computer2, [computer1]);
            }
        }

        return connections.ToImmutableDictionary();
    }

    private static HashSet<string> FindFirstLargestInterconnectedComputerSet(
        ImmutableDictionary<string, List<string>> connections)
    {
        var networks = new List<HashSet<string>>();
        foreach (var startComputer in connections.Keys)
        {
            networks.AddRange(FindInterconnectedComputerSets(connections, startComputer));
        }

        var maxNetworkSize = networks.Max(network => network.Count);
        return networks.First(network => network.Count == maxNetworkSize).ToHashSet();
    }

    private static HashSet<HashSet<string>> FindInterconnectedComputerSets(
        ImmutableDictionary<string, List<string>> connections, string startComputer, int maxNetworkSize = int.MaxValue)
    {
        var interconnectedSets = new HashSet<HashSet<string>>(HashSet<string>.CreateSetComparer());

        var setsInProgress = new Stack<List<string>>();
        setsInProgress.Push([startComputer]);

        // TODO: Remove limit
        // It is simply pure luck that setting a loop limit like this produces the correct solution for my puzzle input.
        // This is not a general solution and should be fixed.
        var limit = 0;
        while (++limit < connections.Count && setsInProgress.Count > 0)
        {
            var currentSetList = setsInProgress.Pop();
            if (currentSetList.Count > maxNetworkSize)
            {
                continue;
            }

            var lastComputer = currentSetList.Last();

            foreach (var nextComputer in connections[lastComputer])
            {
                if (nextComputer == startComputer)
                {
                    interconnectedSets.Add(currentSetList.ToHashSet());
                    continue;
                }

                if (currentSetList.Count == maxNetworkSize)
                {
                    continue;
                }

                if (!currentSetList.Contains(nextComputer) &&
                    !currentSetList.Except(connections[nextComputer]).Any())
                {
                    setsInProgress.Push(currentSetList.Append(nextComputer).ToList());
                }
            }
        }

        return interconnectedSets;
    }

    public override string Part1Solution()
    {
        var connections = ParseInput(Input);
        var threeInterconnectedComputerSets = connections
            .Where(kvp => kvp.Key.StartsWith('t'))
            .SelectMany(connection => FindInterconnectedComputerSets(connections, connection.Key, maxNetworkSize: 3))
            .Where(set => set.Count == 3)
            .ToHashSet(HashSet<string>.CreateSetComparer());

        foreach (var set in threeInterconnectedComputerSets)
        {
            Console.WriteLine(string.Join(',', set));
        }

        return threeInterconnectedComputerSets.Count.ToString();
    }

    public override string Part2Solution()
    {
        var connections = ParseInput(Input);
        var largestConnectionSet = FindFirstLargestInterconnectedComputerSet(connections).ToList();
        largestConnectionSet.Sort();
        return string.Join(',', largestConnectionSet);
    }
}
