using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace solve.day5
{
    class Program
    {
        static void Main(string[] args)
        {

            var polymer = File.ReadAllText("part1.txt").Trim();
            var length = CollapsePolymer(polymer);
            Console.WriteLine($"The remaining polymer has {length} units");

            var min = int.MaxValue;
            for (var chr = 'a'; chr <= 'z'; chr++)
            {
                var poly = polymer.Replace(chr.ToString(), string.Empty, StringComparison.OrdinalIgnoreCase);
                var collapsedLength = CollapsePolymer(poly);
                Console.WriteLine($"Removing '{chr.ToString()}/{chr.ToString().ToUpper()}' gives {collapsedLength} units");

                min = Math.Min(min, collapsedLength);
            }

            Console.WriteLine($"The shortest polymer that could be made is {min} units");

            Console.ReadKey();
        }

        public static int CollapsePolymer(string polymer)
        {
            // check if the polymer units are the magic number away from each other on the ascii table.
            Func<int, int, bool> Reacts = (poly1, poly2) => Math.Abs(poly1 - poly2) == 32;

            var stack = new Stack<char>(polymer.Length);
            foreach (var unit in polymer)
            {
                if (stack.TryPop(out var adjacent))
                {
                    if (!Reacts(unit, adjacent))
                    {
                        stack.Push(adjacent);
                        stack.Push(unit);
                    }
                }
                else
                {
                    stack.Push(unit);
                }
            }

            return stack.Count;
        }
    }
}
