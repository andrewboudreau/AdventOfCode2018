using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace solve.day16
{
    class Program
    {
        public static int[] Registers = new int[4];

        static void Main(string[] args)
        {
            Registers.Write(3, 2, 1, 1);
            var count = 0;
            var total = 0;

            // Example
            var results = SameBehaviorOnThreeOrMoreOperations(Registers, (Op)3, 2, 1, 2, new int[4] { 3, 2, 2, 1 });

            // Part 1
            foreach (var op in ParsePart1Input("part1.txt"))
            {
                if (SameBehaviorOnThreeOrMoreOperations(
                    op.Registers,
                    (Op)op.Instruction[0],
                    op.Instruction[1],
                    op.Instruction[2],
                    op.Instruction[3],
                    op.Expected))
                    count++;

                total++;
            }

            Console.WriteLine($"There were {count} similar ops of {total} ops.");


            // Part 2
            Registers.Write(0, 0, 0, 0);
            foreach (var line in File.ReadAllLines("part2.txt"))
            {
                var instruction = line.Split(" ").Select(int.Parse).ToArray();
                Execute(Registers, instruction);
            }
            Console.WriteLine($"Part2: {Registers.Print()}");

            Console.WriteLine();
            Console.WriteLine("Done, press 'enter' to quit");
            Console.ReadKey();
        }

        public static void Execute(int[] registers, int[] instruction)
        {
            int operand1, operand2, output;
            operand1 = instruction[1];
            operand2 = instruction[2];
            output = instruction[3];

            Op op = (Op)instruction[0];

            switch (op)
            {
                case Op.Eqri:
                    registers.EQRI(operand1, operand2, output);
                    break;

                case Op.Bori:
                    registers.BORI(operand1, operand2, output);
                    break;

                case Op.Addi:
                    registers.ADDI(operand1, operand2, output);
                    break;

                case Op.Bani:
                    registers.BANI(operand1, operand2, output);
                    break;

                case Op.Seti:
                    registers.SETI(operand1, operand2, output);
                    break;

                case Op.Eqrr:
                    registers.EQRR(operand1, operand2, output);
                    break;

                case Op.Addr:
                    registers.ADDR(operand1, operand2, output);
                    break;

                case Op.Gtri:
                    registers.GTRI(operand1, operand2, output);
                    break;

                case Op.Borr:
                    registers.BORR(operand1, operand2, output);
                    break;

                case Op.Gtir:
                    registers.GTIR(operand1, operand2, output);
                    break;

                case Op.Setr:
                    registers.SETR(operand1, operand2, output);
                    break;

                case Op.Eqir:
                    registers.EQIR(operand1, operand2, output);
                    break;

                case Op.Mulr:
                    registers.MULR(operand1, operand2, output);
                    break;

                case Op.Muli:
                    registers.MULI(operand1, operand2, output);
                    break;

                case Op.Gtrr:
                    registers.GTRR(operand1, operand2, output);
                    break;

                case Op.Banr:
                    registers.BANR(operand1, operand2, output);
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }

        public static IEnumerable<(int[] Registers, int[] Instruction, int[] Expected)> ParsePart1Input(string filePath)
        {
            using (var reader = new StreamReader("part1.txt"))
            {
                while (!reader.EndOfStream)
                {
                    var before = reader.ReadLine();
                    var instruction = reader.ReadLine().Split(' ').Select(int.Parse).ToArray();
                    var after = reader.ReadLine();

                    reader.ReadLine();

                    var registers = before.Split("[")[1].Split(',').Select(x => int.Parse(x.Trim(']').Trim())).ToArray();
                    var expected = after.Split("[")[1].Split(',').Select(x => int.Parse(x.Trim(']').Trim())).ToArray();

                    yield return (registers, instruction, expected);
                }
            }
        }

        public static bool SameBehaviorOnThreeOrMoreOperations(
            int[] registers,
            Op op,
            int operand1,
            int operand2,
            int output,
            int[] expected)
        {
            var count = 0;
            var initial = registers.Copy();
            var results = new Dictionary<Op, string>(16);

            Action setup = () =>
            {
                if (registers.Print() == expected.Print())
                {
                    count++;
                }


                registers.Write(initial);
            };

            registers.ADDR(operand1, operand2, output);
            results.Add(Op.Addr, registers.Print());
            setup();

            registers.ADDI(operand1, operand2, output);
            results.Add(Op.Addi, registers.Print());
            setup();

            registers.MULR(operand1, operand2, output);
            results.Add(Op.Mulr, registers.Print());
            setup();

            registers.MULI(operand1, operand2, output);
            results.Add(Op.Muli, registers.Print());
            setup();

            registers.BANR(operand1, operand2, output);
            results.Add(Op.Banr, registers.Print());
            setup();

            registers.BANI(operand1, operand2, output);
            results.Add(Op.Bani, registers.Print());
            setup();

            registers.BORR(operand1, operand2, output);
            results.Add(Op.Borr, registers.Print());
            setup();

            registers.BORI(operand1, operand2, output);
            results.Add(Op.Bori, registers.Print());
            setup();

            registers.SETR(operand1, operand2, output);
            results.Add(Op.Setr, registers.Print());
            setup();

            registers.SETI(operand1, operand2, output);
            results.Add(Op.Seti, registers.Print());
            setup();

            registers.GTIR(operand1, operand2, output);
            results.Add(Op.Gtir, registers.Print());
            setup();

            registers.GTRI(operand1, operand2, output);
            results.Add(Op.Gtri, registers.Print());
            setup();

            registers.GTRR(operand1, operand2, output);
            results.Add(Op.Gtrr, registers.Print());
            setup();

            registers.EQIR(operand1, operand2, output);
            results.Add(Op.Eqir, registers.Print());
            setup();

            registers.EQRI(operand1, operand2, output);
            results.Add(Op.Eqri, registers.Print());
            setup();

            registers.EQRR(operand1, operand2, output);
            results.Add(Op.Eqrr, registers.Print());
            setup();

            // this was used to map the opcodes by iterating 0...15
            var possibleOpCodes = results.ToLookup(x => x.Value, y => y.Key);
            if ((int)op == 15)
            {
                //Console.WriteLine($"{(int)op} {string.Join(", ", possibleOpCodes[expected.Print()])}");
            }

            return count > 2;
        }
    }

    public enum Op
    {
        Eqri = 0,
        Bori = 1,
        Addi = 2,
        Bani = 3,
        Seti = 4,
        Eqrr = 5,
        Addr = 6,
        Gtri = 7,
        Borr = 8,
        Gtir = 9,
        Setr = 10,
        Eqir = 11,
        Mulr = 12,
        Muli = 13,
        Gtrr = 14,
        Banr = 15
    }

    public static class Operation
    {

        // Add
        public static void ADDR(this int[] registers, int operand1, int operand2, int output)
        {
            registers[output] = Add(registers[operand1], registers[operand2]);
        }
        public static void ADDI(this int[] registers, int operand1, int operand2, int output)
        {
            registers[output] = Add(registers[operand1], operand2);
        }
        private static int Add(int a, int b)
        {
            return a + b;
        }


        // Multiply
        public static void MULR(this int[] registers, int operand1, int operand2, int output)
        {
            registers[output] = Multiply(registers[operand1], registers[operand2]);
        }
        public static void MULI(this int[] registers, int operand1, int operand2, int output)
        {
            registers[output] = Multiply(registers[operand1], operand2);
        }
        private static int Multiply(int a, int b)
        {
            return a * b;
        }


        // Bitwise AND
        public static void BANR(this int[] registers, int operand1, int operand2, int output)
        {
            registers[output] = BitwiseAnd(registers[operand1], registers[operand2]);
        }
        public static void BANI(this int[] registers, int operand1, int operand2, int output)
        {
            registers[output] = BitwiseAnd(registers[operand1], operand2);
        }
        private static int BitwiseAnd(int a, int b)
        {
            return a & b;
        }


        // Bitwise OR
        public static void BORR(this int[] registers, int operand1, int operand2, int output)
        {
            registers[output] = BitwiseOr(registers[operand1], registers[operand2]);
        }
        public static void BORI(this int[] registers, int operand1, int operand2, int output)
        {
            registers[output] = BitwiseOr(registers[operand1], operand2);
        }
        private static int BitwiseOr(int a, int b)
        {
            return a | b;
        }


        // Set Registers
        public static void SETR(this int[] registers, int operand1, int operand2, int output)
        {
            registers[output] = registers[operand1];
        }
        public static void SETI(this int[] registers, int operand1, int operand2, int output)
        {
            registers[output] = operand1;
        }


        // Greater-Than testing

        // (greater-than immediate/register) 
        public static void GTIR(this int[] registers, int operand1, int operand2, int output)
        {
            registers[output] = Gt(operand1, registers[operand2]);
        }

        // (greater-than register/immediate) 
        public static void GTRI(this int[] registers, int operand1, int operand2, int output)
        {
            registers[output] = Gt(registers[operand1], operand2);
        }

        // (greater-than register/register)
        public static void GTRR(this int[] registers, int operand1, int operand2, int output)
        {
            registers[output] = Gt(registers[operand1], registers[operand2]);
        }

        private static int Gt(int a, int b)
        {
            return a > b ? 1 : 0;
        }

        // Equality testing
        //(equal register/immediate)
        public static void EQIR(this int[] registers, int operand1, int operand2, int output)
        {
            registers[output] = Eq(operand1, registers[operand2]);
        }

        // (equal register/immediate)
        public static void EQRI(this int[] registers, int operand1, int operand2, int output)
        {
            registers[output] = Eq(registers[operand1], operand2);
        }

        // (equal register/register)
        public static void EQRR(this int[] registers, int operand1, int operand2, int output)
        {
            registers[output] = Eq(registers[operand1], registers[operand2]);
        }

        private static int Eq(int a, int b)
        {
            return a == b ? 1 : 0;
        }


        public static string Print(this int[] registers)
        {
            return $"[{registers[0]}, {registers[1]}, {registers[2]}, {registers[3]}]";
        }

        public static void Write(this int[] registers, int a, int b, int c, int d)
        {
            registers[0] = a;
            registers[1] = b;
            registers[2] = c;
            registers[3] = d;
        }
        public static void Write(this int[] registers, int[] other)
        {
            registers[0] = other[0];
            registers[1] = other[1];
            registers[2] = other[2];
            registers[3] = other[3];
        }

        public static int[] Copy(this int[] registers)
        {
            return new int[4] { registers[0], registers[1], registers[2], registers[3] };
        }
    }
}
