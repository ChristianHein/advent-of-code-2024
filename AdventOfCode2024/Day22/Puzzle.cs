using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AdventOfCode2024.Day22;

public class Puzzle(string[] input) : BasePuzzle(input)
{
    public override int Number => 22;

    private static List<long> ParseInput(string[] input)
    {
        return input.Select(long.Parse).ToList();
    }

    private static List<long> CalculateSecretNumbers(long initialSecretNumber, int count)
    {
        var secretNumbers = new List<long>(count) { initialSecretNumber };

        var n = initialSecretNumber;
        while (count-- > 0)
        {
            n = Mix(n, n * 64);
            n = Prune(n);
            n = Mix(n, n / 32);
            n = Prune(n);
            n = Mix(n, n * 2048);
            n = Prune(n);
            secretNumbers.Add(n);
            continue;

            long Mix(long a, long b)
            {
                return a ^ b;
            }

            long Prune(long a)
            {
                return a & 0xFF_FFFF;
            }
        }

        return secretNumbers;
    }

    public override string Part1Solution()
    {
        var initialSecretNumbers = ParseInput(Input);
        var calculatedFutureSecrets = initialSecretNumbers
            .Select(initial => CalculateSecretNumbers(initial, 2000).Last())
            .ToList();
        return calculatedFutureSecrets.Sum().ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string Part2Solution()
    {
        var initialSecretNumbers = ParseInput(Input);
        var secretNumberChains = initialSecretNumbers
            .Select(initial => CalculateSecretNumbers(initial, 2000))
            .ToList();

        var diffChains = secretNumberChains
            .Select(chain => chain
                .Zip(chain.Skip(1), (a, b) => SecretNumberToPrice(b) - SecretNumberToPrice(a))
                .ToList())
            .ToList();

        var maxBananas = 0;

        const int sequenceLength = 4;
        foreach (var referenceDiffs in diffChains)
        {
            for (var diffOffset = 0; diffOffset < 2000 - sequenceLength + 1; diffOffset++)
            {
                var sequence = new[]
                {
                    referenceDiffs[diffOffset],
                    referenceDiffs[diffOffset + 1],
                    referenceDiffs[diffOffset + 2],
                    referenceDiffs[diffOffset + 3]
                };

                var bananas = Enumerable.Range(0, secretNumberChains.Count)
                    .Sum(buyerIdx =>
                        CalculateSalesPrice(secretNumberChains[buyerIdx], diffChains[buyerIdx], sequence));
                if (bananas > maxBananas)
                {
                    Console.WriteLine($"[{DateTime.Now}] New max bananas: {bananas}");
                }
                maxBananas = Math.Max(bananas, maxBananas);
                //Console.WriteLine($"{string.Join(',', sequence)} -> {bananas} bananas");
            }
        }

        return maxBananas.ToString();
    }

    private static int CalculateSalesPrice(List<long> secretNumberChain, List<int> diffChain, int[] sequence)
    {
        Debug.Assert(sequence.Length == 4);
        var firstDiffMatchIdx = Enumerable
            .Range(0, diffChain.Count - sequence.Length + 1)
            .FirstOrDefault(i => diffChain[i] == sequence[0] &&
                                 diffChain[i + 1] == sequence[1] &&
                                 diffChain[i + 2] == sequence[2] &&
                                 diffChain[i + 3] == sequence[3], -1);

        return firstDiffMatchIdx == -1
            ? 0
            : SecretNumberToPrice(secretNumberChain[firstDiffMatchIdx + sequence.Length]);
    }

    private static byte SecretNumberToPrice(long secretNumber)
    {
        return (byte)(secretNumber % 10);
    }
}
