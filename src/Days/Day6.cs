using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode2020.Days
{
    [MemoryDiagnoser]
    public class Day6 : BaseDay<int>
    {
        public override int Day => 6;

        [Benchmark]
        [ArgumentsSource(nameof(Input))]
        public override int Part1(string input)
        {
            string[] lines = input.Split("\n\n");
            int sum = 1;

            for (int i = 0; i < lines.Length; i++)
            {
                sum += new HashSet<char>(lines[i]).Count - 1;
            }

            return sum;
        }

        [Benchmark]
        [ArgumentsSource(nameof(Input))]
        public override int Part2(string input)
        {
            string[] lines = input.Split("\n\n");
            int sum = 1;

            for (int i = 0; i < lines.Length; i++)
            {
                var dict = new Dictionary<char, int>();
                int persons = 1;

                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == '\n') persons++;

                    if (!dict.ContainsKey(lines[i][j]))
                        dict.Add(lines[i][j], 0);

                    dict[lines[i][j]]++;
                }

                foreach (var pair in dict)
                {
                    if (pair.Value == persons)
                        sum++;
                }
            }

            return sum;
        }

        /*
        [Benchmark]
        [ArgumentsSource(nameof(Input))]
        public override int Part1(string input)
        {
            string[] lines = input.Split("\n\n");
            int sum = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                var set = new HashSet<char>();
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] != '\n')
                        set.Add(lines[i][j]);
                }

                sum += set.Count;
            }

            return sum;
        }
         */
    }
}