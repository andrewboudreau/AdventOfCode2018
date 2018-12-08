<Query Kind="Program" />

void Main()
{
	// Advent of Code 2018, DAY 3
	Directory.SetCurrentDirectory(Path.GetDirectoryName(Util.CurrentQueryPath));
	var input = File.ReadAllLines("input.txt").Select(f => new Claim(f)).ToList();
	//var input = "#1 @ 1,3: 4x4|#2 @ 3,1: 4x4|#3 @ 5,5: 2x2".Split('|').Select(f => new Claim(f)).ToList();

	// Part 1
	var overlap = new OverlapTracker(input);

	Console.WriteLine("Part 1");
	Console.WriteLine($"{overlap.UsedMoreThanOnce()} points where used more than once.");
	Console.WriteLine();

	// Part 2
	Console.WriteLine("Part 2");
	Console.WriteLine($"Claim {overlap.ClaimWithoutOverlap()} doesn't overlap any other claims.");
	Console.WriteLine();
}

public class OverlapTracker
{
	private Dictionary<System.Drawing.Point, List<Claim>> UsedPoints { get; } = new Dictionary<System.Drawing.Point, List<Claim>>();
	
	private List<Claim> claims;
	
	public OverlapTracker(List<Claim> inputs)
	{
		claims = inputs;
		foreach (var input in inputs)
		{
			this.Add(input);
		}
	}

	/// <summary>
	/// Adds the point to a tracker which returns true if the point as already been added, false otherwise.
	/// </summary>
	public void Add(Claim claim)
	{
		foreach (var point in claim.Points())
		{
			if (!UsedPoints.ContainsKey(point))
			{
				UsedPoints.Add(point, new List<Claim>() { claim });
			}
			else
			{
				UsedPoints[point].Add(claim);
			}
		}
	}

	public int UsedMoreThanOnce()
	{
		return UsedPoints.Values.Count(v => v.Count > 1);
	}

	public int ClaimWithoutOverlap()
	{
		var set = new HashSet<int>(claims.Select(c => c.Id));
		foreach (var point in UsedPoints)
		{
			if (point.Value.Count >= 2)
			{
				point.Value.ForEach(x => set.Remove(x.Id));
			}
		}
		
		return set.Single();
	}
}

public class Claim
{
	public Claim(string row)
	{
		//"#1 @ 850,301: 23x12"
		var parts = row.Split(' ');
		Id = int.Parse(parts[0].Trim('#'));
		Left = int.Parse(parts[2].Split(',')[0]);
		Top = int.Parse(parts[2].Split(',')[1].Trim(':'));
		Width = int.Parse(parts[3].Split('x')[0]);
		Height = int.Parse(parts[3].Split('x')[1]);
		//Console.WriteLine($"X1:{X1}, X2:{X2}, Y1:{Y1}, Y2:{Y2}");
	}

	public int Id { get; }
	public int Top { get; }
	public int Left { get; }
	public int Width { get; }
	public int Height { get; }
	public int X1 => Left + 1;
	public int Y1 => Top + 1;
	public int X2 => X1 + Width - 1;
	public int Y2 => Y1 + Height - 1;

	public IEnumerable<System.Drawing.Point> Points()
	{
		for (var x = X1; x <= X2; x++)
		{
			for (var y = Y1; y <= Y2; y++)
			{
				yield return new System.Drawing.Point(x, y);
			}
		}
	}
}