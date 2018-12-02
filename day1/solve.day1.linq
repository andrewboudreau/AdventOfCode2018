<Query Kind="Statements" />

// Advent of Code 2018, DAY 1
Directory.SetCurrentDirectory(Path.GetDirectoryName(Util.CurrentQueryPath));
var input = File.ReadAllLines("input.txt").Select(int.Parse).ToList();

// Part 1
var result = input.Aggregate((x, y) => x + y);
Console.WriteLine($"Part 1: {result}");

// Part 2
var freq = 0;
var step = 0;
var set = new SortedSet<int>();
while(set.Add(freq))
{
	freq += input[step++ % input.Count];
}
Console.WriteLine($"Part 2: {freq} after {step} iterations");