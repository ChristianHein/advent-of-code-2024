using System.Diagnostics;
using System.Numerics;

namespace AdventOfCode2024.Day11;

public class Puzzle(string[] input) : BasePuzzle(input)
{
    public override int Number => 11;

    private static Dictionary<BigInteger, BigInteger> ParseStoneOccurrences(string[] input)
    {
        Debug.Assert(input is { Length: 1 });

        var stones = input[0].Split(' ').Select(BigInteger.Parse);

        var stoneOccurrences = new Dictionary<BigInteger, BigInteger>();
        foreach (var stone in stones)
        {
            if (!stoneOccurrences.TryAdd(stone, 1))
            {
                stoneOccurrences[stone]++;
            }
        }

        return stoneOccurrences;
    }

    private static Dictionary<BigInteger, BigInteger> Blink(Dictionary<BigInteger, BigInteger> stones)
    {
        var resultStones = new Dictionary<BigInteger, BigInteger>(stones);

        foreach (var (stone, occ) in stones)
        {
            if (stone == 0)
            {
                if (!resultStones.TryAdd(1, occ))
                {
                    resultStones[1] += occ;
                }
            }
            else if (stone.ToString().Length % 2 == 0)
            {
                var stoneAsString = stone.ToString();

                var leftStone = BigInteger.Parse(stoneAsString[..(stoneAsString.Length / 2)]);
                if (!resultStones.TryAdd(leftStone, occ))
                {
                    resultStones[leftStone] += occ;
                }

                var rightStone = BigInteger.Parse(stoneAsString[(stoneAsString.Length / 2)..]);
                if (!resultStones.TryAdd(rightStone, occ))
                {
                    resultStones[rightStone] += occ;
                }
            }
            else
            {
                if (!resultStones.TryAdd(stone * 2024, occ))
                {
                    resultStones[stone * 2024] += occ;
                }
            }

            resultStones[stone] -= occ;
            if (resultStones[stone] == 0)
            {
                resultStones.Remove(stone);
            }
        }

        return resultStones;
    }

    public override string Part1Solution()
    {
        var stones = ParseStoneOccurrences(Input);

        const int numberOfBlinks = 25;
        for (var i = 0; i < numberOfBlinks; i++)
        {
            stones = Blink(stones);
        }

        return stones.Aggregate(BigInteger.Zero, (sum, next) => sum + next.Value).ToString();
    }

    public override string Part2Solution()
    {
        var stones = ParseStoneOccurrences(Input);

        const int numberOfBlinks = 75;
        for (var i = 0; i < numberOfBlinks; i++)
        {
            stones = Blink(stones);
        }

        return stones.Aggregate(BigInteger.Zero, (sum, next) => sum + next.Value).ToString();
    }
}
