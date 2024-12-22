using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;
using Microsoft.Z3;

namespace AdventOfCode2024.Day17;

public class Puzzle(string[] input) : BasePuzzle(input)
{
    public override int Number => 17;

    private record struct Registers
    {
        public ulong A;
        public ulong B;
        public ulong C;
    }

    private static (Registers, List<byte>) ParseInput(string[] input)
    {
        var registers = new Registers
        {
            A = ulong.Parse(input[0].Split(": ", 2)[1]),
            B = ulong.Parse(input[1].Split(": ", 2)[1]),
            C = ulong.Parse(input[2].Split(": ", 2)[1])
        };
        var instructions = input[4].Split(": ", 2)[1].Split(',').Select(byte.Parse).ToList();

        return (registers, instructions);
    }

    private static string RunProgram(List<byte> instructions, Registers registers)
    {
        var consoleOut = new StringBuilder();
        var ip = 0;
        while (ip < instructions.Count)
        {
            switch (instructions[ip])
            {
                // adv
                case 0:
                    registers.A >>>= (byte)Combo(instructions[ip + 1]);
                    break;
                // bxl
                case 1:
                    registers.B ^= instructions[ip + 1];
                    break;
                // bst
                case 2:
                    registers.B = Combo(instructions[ip + 1]) % 8;
                    break;
                // jnz
                case 3 when registers.A != 0:
                    ip = instructions[ip + 1];
                    continue;
                case 3:
                    break;
                // bxc
                case 4:
                    registers.B ^= registers.C;
                    break;
                // out
                case 5:
                {
                    var outValue = (Combo(instructions[ip + 1]) % 8) switch
                    {
                        0 => '0',
                        1 => '1',
                        2 => '2',
                        3 => '3',
                        4 => '4',
                        5 => '5',
                        6 => '6',
                        7 => '7',
                        _ => throw new UnreachableException()
                    };
                    consoleOut.Append(outValue);
                }
                    break;
                // bdv
                case 6:
                    registers.B = registers.A >>> (byte)Combo(instructions[ip + 1]);
                    break;
                // cdv
                case 7:
                    registers.C = registers.A >>> (byte)Combo(instructions[ip + 1]);
                    break;
                default:
                    throw new InvalidOperationException();
            }

            ip += 2;
        }

        return string.Join(',', consoleOut.ToString().ToImmutableArray());

        ulong Combo(byte val)
        {
            return val switch
            {
                0 => 0,
                1 => 1,
                2 => 2,
                3 => 3,
                4 => registers.A,
                5 => registers.B,
                6 => registers.C,
                _ => throw new InvalidOperationException()
            };
        }
    }

    private static string Part2SolutionForMySpecificInput()
    {
        // The program has only one jump (the last instruction) that always loops back to the beginning if `A != 0`.
        // `A` only gets manipulated at the end of the loop, so that `B` and `C` are only dependent on `A`'s value at
        // the beginning of the loop. `B` and `C`'s values do not carry over into the next loop.
        //
        // We can thus create a formula/function that calculates for any value of `A` what will be printed in a
        // particular loop iteration.
        //
        // Here is the program written out:
        // ```asm
        // bst 4 ; B := A % 8
        // bxl 1 ; B := B XOR 1
        // cdv 5 ; C := A >> B
        // bxl 5 ; B := B XOR 5
        // bxc 5 ; B := B XOR C
        // adv 3 ; A := A >> 3
        // out 5 ; Print(B % 8)
        // jnz 0 ; if A != 0 then goto 0
        // ```
        //
        // Here is code that calculates the printed result in the first loop is the following:
        // ```c#
        // ((A % 8) ^ 4 ^ (x >>> (byte)((A % 8) ^ 1))) % 8
        // ```
        // To calculate the result of the next loop, execute the `adv 3` instruction like in the code - meaning,
        // multiply `A` by 8 or do `A >>> 3`.
        //
        // Calculating this value for all integers is still much too slow to solve this puzzle. A different approach
        // works better: Using an SMT solver.
        var ctx = new Context();
        var solver = ctx.MkOptimize();

        // (declare-const a (_ BitVec 64))
        var a = ctx.MkBVConst("a", 64);

        // (declare-fun f ((_ BitVec 64)) (_ BitVec 64))
        var f = ctx.MkFuncDecl("f", ctx.MkBitVecSort(64), ctx.MkBitVecSort(64));

        // (assert (forall ((x (_ BitVec 64))) (= (f x)
        //             (bvxor
        //                 (bvxor (bvand x (_ bv7 64)) (_ bv4 64))
        //                 (bvand (bvlshr x (bvxor (bvand x (_ bv7 64)) (_ bv1 64)))
        //                        (_ bv7 64))))))
        var x = (BitVecExpr)ctx.MkBound(0, ctx.MkBitVecSort(64));
        var functionBody = ctx.MkBVXOR(
            ctx.MkBVXOR(ctx.MkBVAND(x, ctx.MkBV(7, 64)), ctx.MkBV(4, 64)),
            ctx.MkBVAND(ctx.MkBVLSHR(x, ctx.MkBVXOR(ctx.MkBVAND(x, ctx.MkBV(7, 64)), ctx.MkBV(1, 64))),
                ctx.MkBV(7, 64)));
        solver.Add(ctx.MkForall([ctx.MkBitVecSort(64)], [ctx.MkSymbol("x")], ctx.MkEq(ctx.MkApp(f, x), functionBody)));

        var instructions = new List<byte> { 2, 4, 1, 1, 7, 5, 1, 5, 4, 5, 0, 3, 5, 5, 3, 0 };
        for (var i = 0; i < instructions.Count; i++)
        {
            // (assert (= (f (bvlshr a (bvmul (_ bv3 64) (_ bv{i} 64)))) (_ bv{instructions[i]} 64)))
            solver.Assert(ctx.MkEq(ctx.MkApp(f, ctx.MkBVLSHR(a, ctx.MkBV(3 * i, 64))), ctx.MkBV(instructions[i], 64)));
        }

        // NOTE: Technically, this next MkMinimize is necessary to always get the smallest answer. But it makes Z3 much
        // slower, whereas without minimize the answer currently is still correct and is generated instantly. So I've
        // commented it out for now.

        // (minimize a)
        //solver.MkMinimize(a);

        var status = solver.Check();
        if (status != Status.SATISFIABLE)
            throw new InvalidOperationException();

        return solver.Model.Eval(a).ToString();
    }

    public override string Part1Solution()
    {
        var (registers, instructions) = ParseInput(Input);
        var consoleOut = RunProgram(instructions, registers);
        return consoleOut;
    }

    public override string Part2Solution()
    {
        var (registers, instructionsList) = ParseInput(Input);

        var myInputRegisters = new Registers { A = 30344604, B = 0, C = 0 };
        var myInputInstructionsList = new List<byte> { 2, 4, 1, 1, 7, 5, 1, 5, 4, 5, 0, 3, 5, 5, 3, 0 };

        if (registers.Equals(myInputRegisters) && instructionsList.SequenceEqual(myInputInstructionsList))
        {
            return Part2SolutionForMySpecificInput();
        }

        var targetOutput = string.Join(',', instructionsList);
        for (var a = 0ul;; a++)
        {
            registers.A = a;
            var consoleOut = RunProgram(instructionsList, registers);
            if (consoleOut == targetOutput)
            {
                return a.ToString();
            }
        }
    }
}
