using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Text;

namespace AdventOfCode2024.Day09;

public class Day9(string[] input) : Puzzle(input)
{
    public override int Number => 9;

    private const int FreeSpaceId = -1;

    private static List<(int id, byte blockCount)> ParseInput(string[] input)
    {
        Debug.Assert(input.Length == 1);
        var result = new List<(int id, byte blockCount)>(input[0].Length);

        var fileId = 0;
        for (var i = 0; i < input[0].Length; i++)
        {
            if (IsFileAtIndex(i))
            {
                result.Add((fileId, CharToByte(input[0][i])));
                fileId++;
            }
            else
            {
                result.Add((FreeSpaceId, CharToByte(input[0][i])));
            }
        }

        return result;

        bool IsFileAtIndex(int index) => index % 2 == 0;
        byte CharToByte(char c) => (byte)(c - '0');
    }

    public override string Part1Solution()
    {
        var blockGroups = ParseInput(Input);
        var firstFreeSpaceIndex = blockGroups.FindIndex(x => x.id == -1);
        var lastFileIndex = blockGroups.FindLastIndex(x => x.id != -1);

        while (firstFreeSpaceIndex < lastFileIndex)
        {
            if (blockGroups[firstFreeSpaceIndex].id != FreeSpaceId)
            {
                firstFreeSpaceIndex++;
                continue;
            }

            if (blockGroups[lastFileIndex].id == FreeSpaceId)
            {
                lastFileIndex--;
                continue;
            }

            var freeSpace = blockGroups[firstFreeSpaceIndex];
            var file = blockGroups[lastFileIndex];
            if (freeSpace.blockCount < file.blockCount)
            {
                blockGroups[firstFreeSpaceIndex] = (file.id, freeSpace.blockCount);
                blockGroups[lastFileIndex] = (file.id, (byte)(file.blockCount - freeSpace.blockCount));
            }
            else if (freeSpace.blockCount == file.blockCount)
            {
                blockGroups[lastFileIndex] = (FreeSpaceId, file.blockCount);
                blockGroups[firstFreeSpaceIndex] = (file.id, file.blockCount);
            }
            else if (freeSpace.blockCount > file.blockCount)
            {
                blockGroups[lastFileIndex] = (FreeSpaceId, file.blockCount);
                blockGroups[firstFreeSpaceIndex] = (FreeSpaceId, (byte)(freeSpace.blockCount - file.blockCount));
                blockGroups.Insert(firstFreeSpaceIndex, (file.id, file.blockCount));
            }
        }

        return CalculateChecksum(blockGroups).ToString();
    }

    public override string Part2Solution()
    {
        var blockGroups = ParseInput(Input);
        var nextFileIndex = blockGroups.FindLastIndex(x => x.id != -1);

        var fileId = blockGroups[nextFileIndex].id;
        while (fileId >= 0)
        {
            var nextFreeSpaceIndex = 0;

            while (nextFreeSpaceIndex < nextFileIndex)
            {
                if (blockGroups[nextFreeSpaceIndex].id != FreeSpaceId)
                {
                    nextFreeSpaceIndex++;
                    continue;
                }

                if (blockGroups[nextFileIndex].id != fileId)
                {
                    nextFileIndex--;
                    continue;
                }

                var freeSpace = blockGroups[nextFreeSpaceIndex];
                var file = blockGroups[nextFileIndex];
                if (freeSpace.blockCount < file.blockCount)
                {
                    nextFreeSpaceIndex++;
                }
                else if (freeSpace.blockCount == file.blockCount)
                {
                    blockGroups[nextFileIndex] = (FreeSpaceId, file.blockCount);
                    blockGroups[nextFreeSpaceIndex] = (file.id, file.blockCount);
                    break;
                }
                else if (freeSpace.blockCount > file.blockCount)
                {
                    blockGroups[nextFileIndex] = (FreeSpaceId, file.blockCount);
                    blockGroups[nextFreeSpaceIndex] = (FreeSpaceId, (byte)(freeSpace.blockCount - file.blockCount));
                    blockGroups.Insert(nextFreeSpaceIndex, (file.id, file.blockCount));
                    break;
                }
            }

            fileId--;
        }

        return CalculateChecksum(blockGroups).ToString();
    }

    private static BigInteger CalculateChecksum(List<(int id, byte blockCount)> blockGroups)
    {
        var checksum = new BigInteger(0);
        var i = 0;
        foreach (var (id, size) in blockGroups)
        {
            if (id == FreeSpaceId)
            {
                i += size;
                continue;
            }

            // Equals: checksum += id * (i + i+1 + i+2 + ... + i+size-1)
            checksum += BigInteger.Multiply(id, size * i + size * (size - 1) / 2);
            i += size;
        }

        return checksum;
    }

    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private static string BlockMapToString(List<(int id, byte blockCount)> blockGroups)
    {
        var sb = new StringBuilder();
        foreach (var blockGroup in blockGroups)
        {
            sb.Append($"({blockGroup.id}, {blockGroup.blockCount}) ");
        }

        return sb.ToString();
    }
}
