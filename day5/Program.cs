using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace solve.day5
{
    class Program
    {
        static void Main(string[] args)
        {
            var foo = React('A', 'a');
            var foo2 = React('A', 'b');

            var polymer = File.ReadAllText("part1.txt").Trim();
            var stack = new Stack<char>(polymer.Length);

            foreach (var unit in polymer)
            {
               
            }
        }

        public static bool React(char poly1, char poly2)
        {
            return Math.Abs(poly1 - poly2) == 32;
        }
    }
}
