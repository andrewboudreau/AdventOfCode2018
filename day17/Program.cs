using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace AOC2018.Day17
{
	class Program
	{
		public static HashSet<Point> Clay = new HashSet<Point>();

		static void Main(string[] args)
		{
			ParseInput("example.txt");

			Console.ReadKey();
		}

		public static IEnumerable<Point> ParseInput(string filePath)
		{
			using(var stream = new StreamReader(filePath))
			{
				while (!stream.EndOfStream)
				{
					var line = stream.ReadLine();
					Console.WriteLine(line);
				}
			}

			return null;
		}
	}
}
