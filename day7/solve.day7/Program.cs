using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace solve.day7
{
	class Program
	{
		// https://en.wikipedia.org/wiki/Topological_sorting
		static void Main(string[] args)
		{
			var input = ParseInput("example.txt");
			//var input = ParseInput("part1.txt");

			var steps = new SortedSet<Step>();

			foreach (var (Parent, Child) in input)
			{
				var parent = new Step(Parent);
				var child = new Step(Child);

				steps.Add(parent);
				steps.Add(child);

				parent.AddChild(child);
				child.AddParent(parent);

				//if (steps.All(x => x.Value != Parent))
				//{
				//	var step = new Step(Parent);
				//	step.AddChild(new Step(Child));
				//	steps.Add(step);
				//}
				//else
				//{
				//	var step = steps.Single(x => x.Value == Parent);
				//	step.AddChild(new Step(Child));
				//}
			}

			//noDependency = new SortedSet<char>(noDependency.Except(hasDependency));
			//Console.WriteLine($"No Dependencies: {string.Join(", ", noDependency)}");

			//while (noDependency.Any())
			//{

			//}
			foreach (var step in steps)
			{
				Console.WriteLine($"{step.Value}");
			}
			Console.WriteLine("Done, press any key to exit.");
			Console.ReadKey();

		}

		public static IEnumerable<(char StepOne, char StepTwo)> ParseInput(string filePath)
		{
			var regex = new Regex(@"\b[A-Z]\b");
			foreach (var line in File.ReadAllLines(filePath))
			{
				var matches = regex.Matches(line);
				yield return (matches[0].Value[0], matches[1].Value[0]);
			}
		}

		public class Step : IComparable<Step>
		{
			public Step(char value)
			{
				Value = value;
			}

			public char Value { get; private set; }

			public List<Step> Parents { get; private set; } = new List<Step>();

			public List<Step> Children { get; private set; } = new List<Step>();

			public void AddParent(Step dependency)
			{
				Parents.Add(dependency);
			}

			public void AddChild(Step child)
			{
				Children.Add(child);
			}

			public override string ToString()
			{
				var sb = new StringBuilder();
				if (Parents.Any())
				{
					sb.Append($"({string.Join(", ", Parents.Select(x => x.Value))} -> ");
				}

				sb.Append($"{Value}");

				if (Children.Any())
				{
					sb.Append($" -> {string.Join(", ", Children.Select(x => x.Value))})");
				}

				return sb.ToString();
			}

			public override bool Equals(object obj)
			{
				if (obj == null)
				{
					return false;
				}

				var otherStep = obj as Step;
				if (otherStep == null)
				{
					return false;
				}

				return Value == otherStep.Value;
			}

			public override int GetHashCode()
			{
				return Value.GetHashCode();
			}

			public int CompareTo(Step other)
			{
				if (other == null)
				{
					return 1;
				}

				return Value.CompareTo(other.Value);
			}
		}
	}
}
