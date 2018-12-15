<Query Kind="Program" />

void Main()
{
	// Advent of Code 2018, DAY 9
	(int Players, int Marbles)[] inputs = new[] {
		(9, 25),
		(10, 1618),
		(13, 7999),
		(441, 71032) // CHALLENGE
	};

	var n = 0;

	var board = new Board(inputs[n].Players);
	Console.WriteLine($"[-] {board.ToString()}");

	for (var i = 0; i < inputs[n].Marbles; i++)
	{
		board.AddMarble();
		Console.WriteLine($"[{board.Player}] {board.ToString()}");
	}
	var highscore = board.Scores.Max();
	var winner = Array.IndexOf(board.Scores, board.Scores.Max()) + 1;
	Console.WriteLine($"HighScore: [{winner}] {highscore}");
	board.PrintScores();
}

public class Board
{
	private readonly int players;
	private readonly int[] scores;

	public Board(int players)
	{
		this.players = players;
		this.scores = new int[this.players];
	}

	public List<int> Circle { get; } = new List<int>() { 0 };

	public int Current { get; private set; } = 0;

	public int Marble { get; private set; } = 0;

	public int Player
	{
		get
		{
			var player = ((Marble - 1) % Players) + 1;
			return player;
		}
	}

	public int Players
	{
		get
		{
			return players;
		}
	}

	public int CurrentValue
	{
		get
		{
			return Circle[Current];
		}
	}

	public int[] Scores
	{
		get
		{
			return scores;
		}
	}

	public void PrintScores()
	{
		var i = 0;
		foreach (var score in Scores)
		{
			Console.WriteLine($"[{++i}] {score}");
		}
	}

	public int Clockwise(int moves)
	{
		var index = moves + Current;
		int capped = 0;
		if (index < 0)
		{
			capped = Circle.Count + index;
		}
		else
		{
			capped = index % Circle.Count;
		}

		return capped;
	}

	public void AddMarble()
	{
		Marble++;
		if (Marble % 23 == 0)
		{
			Console.WriteLine($"[{Player}] current score {scores[Player - 1]}");

			AddScore(Marble);
			Console.WriteLine($"MOD23: adding {Marble} to [{Player}]");

			var removal = Clockwise(-7);
			AddScore(Circle[removal]);
			Console.WriteLine($"CCW7: adding {Circle[removal]} to [{Player}]");

			Circle.RemoveAt(removal);
			Current = removal;
			Console.WriteLine($"[{Player}] current score {scores[Player - 1]}");
		}
		else
		{
			var insert = Clockwise(1);
			Current = insert + 1;
			Circle.Insert(Current, Marble);
			Console.WriteLine($"[{Player}] inserted marble {Marble}");
		}
	}

	public void AddScore(int score)
	{
		scores[Player - 1] += score;
	}

	public override string ToString()
	{
		var sb = new StringBuilder();
		for (var i = 0; i < Circle.Count; i++)
		{
			if (i == Current)
			{
				sb.Append($"({Circle[i]}) ");
			}
			else
			{
				sb.Append($"{Circle[i]} ");
			}
		}

		return sb.ToString();
	}
}