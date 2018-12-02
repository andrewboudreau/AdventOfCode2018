<Query Kind="Statements">
  <Namespace>System.Collections</Namespace>
  <Namespace>System</Namespace>
</Query>

// Advent of Code 2018, DAY 2
Directory.SetCurrentDirectory(Path.GetDirectoryName(Util.CurrentQueryPath));
var input = File.ReadAllLines("input.txt").ToList();

// Part 1 //Checksum: 5976
var doubles = new HashSet<char>();
var triples = new HashSet<char>();
var d = 0;
var t = 0;

foreach (var boxId in input)
{
	var charCount = new Dictionary<char, int>();
	var matches = boxId.Aggregate(
		charCount,
		(acc, current) => acc.AddOrUpdate(current),
		(acc) => (
			Doubles: acc.Where(x => x.Value == 2).Select(p => p.Key),
			Triples: acc.Where(x => x.Value == 3).Select(p => p.Key)
		));

	Console.WriteLine($"{boxId} {d} doubles, {t} triples.");
	if (matches.Doubles.Any())
	{
		d++;
		Console.WriteLine($"2s: {string.Join(" ", matches.Doubles)}");
	}

	if (matches.Triples.Any())
	{
		t++;
		Console.WriteLine($"3s: {string.Join(" ", matches.Triples)}");
	}
}

Console.WriteLine($"Checksum: {d * t}");

// Part 2


// Utilities
public static class SyntaxHelperExtensions
{
	public static Dictionary<char, int> AddOrUpdate(this Dictionary<char, int> tracker, char item)
	{
		if (!tracker.ContainsKey(item))
		{
			tracker[item] = 0;
		}

		tracker[item] += 1;
		return tracker;
	}
}