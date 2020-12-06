using System;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode2020.Days
{
    public class Day2 : BaseDay<int>
    {
        public override int Day => 2;

        [Benchmark]
        [ArgumentsSource(nameof(Input))]
        public override int Part1(string input)
        {
            string[] list = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            int valid = 0;

            for (int i = 0; i < list.Length; i++)
            {
                var entry = new Entry(list[i]);
                int count = 0;

                for (int j = 0; j < entry.Password.Length; j++)
                {
                    if (entry.Password[j] == entry.Letter)
                        count++;
                }

                if (count >= entry.Number1 && count <= entry.Number2)
                    valid++;
            }

            return valid;
        }

        [Benchmark]
        [ArgumentsSource(nameof(Input))]
        public override int Part2(string input)
        {
            string[] list = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            int valid = 0;

            for (int i = 0; i < list.Length; i++)
            {
                var entry = new Entry(list[i]);

                if (entry.Password[entry.Number1 - 1] == entry.Letter ^
                    entry.Password[entry.Number2 - 1] == entry.Letter)
                {
                    valid++;
                }
            }

            return valid;
        }

        private readonly struct Entry
        {
            public Entry(string line)
            {
                string[] split = line.Split(' ');
                string[] policy = split[0].Split('-');

                Number1 = FastIntParse(policy[0]);
                Number2 = FastIntParse(policy[1]);
                Letter = split[1][0];
                Password = split[2];
            }

            public int Number1 { get; }

            public int Number2 { get; }

            public char Letter { get; }

            public string Password { get; }
        }

        private static int FastIntParse(string s)
        {
            int val = 0;
            for (int i = 0; i < s.Length; i++)
            {
                val *= 10;
                val += s[i] - '0';
            }

            return val;
        }
    }
}