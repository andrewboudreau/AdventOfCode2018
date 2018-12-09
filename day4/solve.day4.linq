<Query Kind="Program">
  <Namespace>System.Globalization</Namespace>
</Query>

void Main()
{
	// Advent of Code 2018, DAY 4
	Directory.SetCurrentDirectory(Path.GetDirectoryName(Util.CurrentQueryPath));
	var data_in = @"[1518-11-01 00:00] Guard #10 begins shift|[1518-11-01 00:05] falls asleep|[1518-11-01 00:25] wakes up|[1518-11-01 00:30] falls asleep|[1518-11-01 00:55] wakes up|[1518-11-01 23:58] Guard #99 begins shift|[1518-11-02 00:40] falls asleep|[1518-11-02 00:50] wakes up|[1518-11-03 00:05] Guard #10 begins shift|[1518-11-03 00:24] falls asleep|[1518-11-03 00:29] wakes up|[1518-11-04 00:02] Guard #99 begins shift|[1518-11-04 00:36] falls asleep|[1518-11-04 00:46] wakes up|[1518-11-05 00:03] Guard #99 begins shift|[1518-11-05 00:45] falls asleep|[1518-11-05 00:55] wakes up".Split('|');
	var data_in2 = File.ReadAllLines("input.txt");

	var input = data_in2.Select(f => new GuardEvent(f)).OrderBy(f => f.DateTime).ToList();

	Console.WriteLine($"There are {input.Count} events with {input.GroupBy(x => x.GuardId).Count()} Guards");
	var guardNaps = new GuardsNaps(input);

	// Part 1
	var sleepyGuard = guardNaps
		.TotalNapMinutes()
		.OrderByDescending(x => x.Value)
		.First();

	Console.WriteLine("Part 1");
	Console.WriteLine($"Guard #{sleepyGuard.Key} slept {sleepyGuard.Value} mins.");

	var sleepyGuardNaps = guardNaps.Naps.Where(n => n.GuardId == sleepyGuard.Key).ToList();
	Console.WriteLine($"Guard #{sleepyGuard.Key}'s took {sleepyGuardNaps.Count} naps");

	var minutes = sleepyGuardNaps.CountSleepByMinute();
	var maxOverlap = minutes.Max();
	var maxMinute = minutes.IndexOf(maxOverlap);

	Console.WriteLine();
	Console.WriteLine($"Guard #{sleepyGuard.Key} slept {maxOverlap} times at minute {maxMinute}");
	Console.WriteLine($"Guard #{sleepyGuard.Key} * {maxMinute} = {sleepyGuard.Key * maxMinute}");

	// Part 2
	Console.WriteLine("");
	Console.WriteLine("Part 2");
	var globalOverlap = 0;
	var globalMinute = 0;
	var id = 0;

	for (var i = 0; i < 60; i++)
	{
		Console.Write(i % 10);
	}
	Console.WriteLine();
	foreach (var guard in guardNaps.ByGuard())
	{
		var tally = guard.Naps.CountSleepByMinute();
		Console.WriteLine($"{string.Join("", tally)}");
		var overlap = tally.Max();
		var m = tally.IndexOf(overlap);
		if (overlap > globalOverlap )
		{
			globalOverlap = overlap;
			globalMinute = m;
			id = guard.GuardId;
		}
		Console.WriteLine($"Guard #{guard.GuardId}: was asleep {overlap} nights at minute {m}");
		Console.WriteLine("");
	}

	Console.WriteLine($"Guard #{id}: was asleep the most times ({globalOverlap}) nights at minutes {globalMinute}");
	Console.WriteLine($"{id} * {globalMinute} = {id * globalMinute}");
	Console.WriteLine("");
}

public static class Extensions
{
	public static List<int> CountSleepByMinute(this List<Nap> naps)
	{
		var minutes = new List<int>(60);
		for (var i = 0; i < 60; i++)
		{
			//Console.Write(i % 10);
			minutes.Add(0);
		}

		//Console.WriteLine("");
		foreach (var nap in naps)
		{
			for (var i = 0; i < 60; i++)
			{
				if (i >= nap.Start.Minute && i < nap.End.Minute)
				{
					//Console.Write("1");
					minutes[i] += 1;
				}
				else
				{
					//Console.Write("0");
				}
			}

			//Console.WriteLine();
		}

		return minutes;
	}
}

public class GuardsNaps
{
	public GuardsNaps(List<GuardEvent> events)
	{
		Naps = new List<Nap>();
		var guardId = int.MinValue;
		var startNapTime = DateTime.MinValue;

		foreach (var e in events)
		{
			switch (e.EventType)
			{
				case EventType.Begins:
					guardId = e.GuardId;
					break;

				case EventType.Sleeps:
					startNapTime = e.DateTime;
					break;

				case EventType.Wakes:
					Naps.Add(new Nap(guardId, startNapTime, e.DateTime));
					break;

				default:
					throw new NotImplementedException($"unknown event type {e.EventType}");
			}
		}
	}

	public List<Nap> Naps { get; }

	public Dictionary<int, int> TotalNapMinutes()
	{
		var lookup = Naps.ToLookup(k => k.GuardId, v => v.Length);
		return lookup.ToDictionary(k => k.Key, v => v.Sum());
	}

	public IEnumerable<(int GuardId, List<Nap> Naps)> ByGuard()
	{
		foreach (var guard in Naps.ToLookup(x => x.GuardId, x => x))
		{
			yield return (guard.Key, guard.ToList());
		}
	}
}

public class Nap
{
	public int GuardId { get; set; }
	public int Length { get; set; }
	public DateTime Start { get; set; }
	public DateTime End { get; set; }

	public Nap(int id, DateTime start, DateTime end)
	{
		if (id <= 0) throw new ArgumentException(nameof(id));

		GuardId = id;
		Length = (int)((end - start).TotalMinutes);
		Start = start;
		End = end;
	}
}

public enum EventType
{
	Begins,
	Sleeps,
	Wakes
}

public class GuardEvent
{
	public int GuardId { get; }

	public DateTime DateTime { get; }

	public EventType EventType { get; }

	public GuardEvent(string input)
	{
		var parts = input.Split(']');
		var dateParts = parts[0].Trim('[');

		DateTime = System.DateTime.ParseExact(dateParts, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
		var action = parts[1].Trim();
		if (action.StartsWith("Guard #"))
		{
			var t = action.Substring(7).Split(' ')[0];
			GuardId = int.Parse(t);
		}
		else if (action.StartsWith("wakes"))
		{
			EventType = EventType.Wakes;
		}
		else if (action.StartsWith("falls"))
		{
			EventType = EventType.Sleeps;
		}
	}

	public override string ToString()
	{
		if (GuardId == 0)
		{
			return $"{this.DateTime.Minute} {this.EventType.ToString()}";
		}

		return $"Guard {GuardId} begins shift.";
	}
}