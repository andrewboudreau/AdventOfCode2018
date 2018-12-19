using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Solve.Day15
{
    class Program
    {
        static void Main(string[] args)
        {
            var arena = new Arena(Arena.Example1);
            Console.WriteLine(arena.ToString());

            Console.WriteLine("done, press `enter` to quit.");
            Console.ReadLine();
        }
    }

    public class Entity
    {
        public Entity(int x, int y)
        {
            Attack = 3;
            Health = 200;
            Location = new Point(x, y);
        }

        public Point Location { get; }

        public int Health { get; }

        public int Attack { get; }
    }

    public class Goblin : Entity
    {
        const string print = "G";

        public Goblin(int x, int y)
            : base(x, y)
        {
        }

        public override string ToString()
        {
            return print;
        }
    }

    public class Elf : Entity
    {
        const string print = "E";

        public Elf(int x, int y)
            : base(x, y)
        {

        }

        public override string ToString()
        {
            return print;
        }
    }

    public class Arena
    {
        Point Dimensions;
        List<Point> Walls;
        List<Goblin> Goblins;
        List<Elf> Elves;
        char[][] Floor;

        public Arena(string input)
        {
            var height = 0;
            var rows = new List<string>();

            foreach (var line in input.Split("\r\n"))
            {
                rows.Add(line);
            }

            Dimensions.X = rows.First().Length;
            Dimensions.Y = height;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var y in Floor)
            {
                foreach (var x in y)
                {
                    sb.Append(Floor[y][x])
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }




        public const string Example1 = @"#######
#E..G.#
#...#.#
#.G.#G#
#######";

        public const string Example2 = @"#######
#.G...#
#...EG#
#.#.#G#
#..G#E#
#.....#
#######";

        public const string Part1 = @"################################
###########...G...#.##..########
###########...#..G#..G...#######
#########.G.#....##.#GG..#######
#########.#.........G....#######
#########.#..............#######
#########.#...............######
#########.GG#.G...........######
########.##...............##..##
#####.G..##G.......E....G......#
#####.#..##......E............##
#####.#..##..........EG....#.###
########......#####...E.##.#.#.#
########.#...#######......E....#
########..G.#########..E...###.#
####.###..#.#########.....E.####
####....G.#.#########.....E.####
#.........#G#########......#####
####....###G#########......##..#
###.....###..#######....##..#..#
####....#.....#####.....###....#
######..#.G...........##########
######...............###########
####.....G.......#.#############
####..#...##.##..#.#############
####......#####E...#############
#.....###...####E..#############
##.....####....#...#############
####.########..#...#############
####...######.###..#############
####..##########################
################################";
    }


}
