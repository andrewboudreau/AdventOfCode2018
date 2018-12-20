using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace solve.day6
{
	class Program
	{
		static void Main(string[] args)
		{
			var area = new List<int>(1000);
			var points = ParseInput("part1.txt").ToList();
			//var points = ParseInput("example.txt").ToList();


			

			for (var y = 0; y <= points.Max(p => p.Y); y++)
			{
				for (var x = 0; x <= points.Max(p => p.X); x++)
				{
					var current = new Point(x, y);
					var distances = new Dictionary<int, int>(points.Count);
					var i = 0;

					foreach (var coordinate in points)
					{
						var distance = Distance(current, coordinate);
						distances.Add(i, distance);
						i++;
					}

					var min = distances.Values.Min();
					if (distances.Values.Count(v => v == min) > 1)
					{
						area.Add('.');
					}
					else
					{
						var foo = distances.Where(w => w.Value == min).First().Key;
						area.Add(foo);
					}
				}
			}

			var counts = new Dictionary<int, int>();
			var t = 0;

			var infinite = new HashSet<int>();
			using (var output = new StreamWriter("output.txt"))
			{
				for (var y = 0; y <= points.Max(p => p.Y); y++)
				{
					for (var x = 0; x <= points.Max(p => p.X); x++)
					{
						if (area[t] == '.')
						{
							Console.Write(Convert.ToChar(area[t]));
							output.Write(Convert.ToChar(area[t]));
							t++;
							continue;
						}

						Console.Write(Convert.ToChar((int)'A' + area[t]));
						output.Write(Convert.ToChar((int)'A' + area[t]));
						if (!counts.ContainsKey(area[t]))
						{
							counts.Add(area[t], 0);
						}

						counts[area[t]] += 1;

						if (x == 0 || y == 0 || x == points.Max(p => p.X) || y == points.Max(p => p.Y))
						{
							infinite.Add(area[t]);
						}
						t++;
					}

					Console.WriteLine();
					output.WriteLine();
				}
			}

			var max = -199;
			foreach (var key in counts.Keys)
			{
				if (!infinite.Contains(key))
				{
					Console.WriteLine($"{Convert.ToChar((int)'A' + key)} includes {counts[key]} meters.");
					max = Math.Max(counts[key], max);
				}
				
			}

			Console.WriteLine($"Max area found is {max}");

			Console.WriteLine("Done, press any key to exit.");
			Console.ReadKey();
		}

		public static int Distance(Point source, Point destination)
		{
			return Math.Abs(source.X - destination.X) + Math.Abs(source.Y - destination.Y);
		}

		public static IEnumerable<Point> ParseInput(string filePath)
		{
			foreach (var line in File.ReadAllLines(filePath))
			{
				var parts = line.Split(',').Select(x => int.Parse(x.Trim())).ToArray();
				yield return new Point(parts[0], parts[1]);
			}
		}
	}
}
