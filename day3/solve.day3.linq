<Query Kind="Program" />

void Main()
{
	// Advent of Code 2018, DAY 3
	Directory.SetCurrentDirectory(Path.GetDirectoryName(Util.CurrentQueryPath));
	var input = File.ReadAllLines("input.txt").Select(f => new Claim(f)).ToList();
	var grid = new Grid<int>();
	
	Console.WriteLine(grid[0,1]);
	
	// Part 1 	
	Console.WriteLine(input.Max(x => x.X2));
	Console.WriteLine(input.Max(x => x.Y2));
	Console.WriteLine("Part 1");
	

	// Part 2
	Console.WriteLine("Part 2");
}

public class Grid<T>
{
	private readonly int size;
	private readonly List<T> data;

	public Grid(int size = 1020)
	{
		this.size = size;
		this.data = new List<T>(size * size);
		for (var i = 0; i < size * size; i++)
		{
			this.data[i] = 0;
		}
	}

	// 0-based index access to x/y coordinate.
	public T this[int x, int y]
	{
		get
		{
			return this.data[x + (this.size * y)];
		}
	}
}

public class Claim
{
	public Claim(string row)
	{
		//"#1 @ 850,301: 23x12"
		var parts = row.Split(' ');
		Id = int.Parse(parts[0].Trim('#'));
		Top = int.Parse(parts[2].Split(',')[0]);
		Left = int.Parse(parts[2].Split(',')[1].Trim(':'));
		Width = int.Parse(parts[3].Split('x')[0]);
		Height = int.Parse(parts[3].Split('x')[1]);
	}

	public int Id { get; }
	public int Top { get; }
	public int Left { get; }
	public int Width { get; }
	public int Height { get; }
	public int X1 => Left;
	public int Y1 => Top;
	public int X2 => Left + Width;
	public int Y2 => Top + Height;

	public bool Overlaps(Claim other)
	{
		return
			(this.X2 >= other.X1 && other.X2 >= this.X1) &&
			(this.Y2 >= other.Y1 && other.Y2 >= this.Y1);
	}
}

// Utilities
public static class SyntaxHelperExtensions
{
	public static string HelpMe(this string me)
	{
		return "Help " + me;
	}
}