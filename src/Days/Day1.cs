using System;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode2020.Days
{
    [MemoryDiagnoser]
    public class Day1 : BaseDay<int>
    {
        public override int Day => 1;

        private const int Target = 2020;

        [Benchmark]
        [ArgumentsSource(nameof(Input))]
        public override int Part1(string input)
        {
            int[] entries = Array.ConvertAll(input.Split('\n', StringSplitOptions.RemoveEmptyEntries), int.Parse);

            for (int i = 0; i < entries.Length; i++)
            {
                for (int j = 0; j < entries.Length; j++)
                {
                    if (entries[i] + entries[j] == Target)
                    {
                        return entries[i] * entries[j];
                    }
                }
            }

            return default;
        }

        [Benchmark]
        [ArgumentsSource(nameof(Input))]
        public override int Part2(string input)
        {
            int[] entries = Array.ConvertAll(input.Split('\n', StringSplitOptions.RemoveEmptyEntries), int.Parse);

            for (int i = 0; i < entries.Length; i++)
            {
                for (int j = 0; j < entries.Length; j++)
                {
                    for (int k = 0; k < entries.Length; k++)
                    {
                        if (entries[i] + entries[j] + entries[k] == Target)
                        {
                            return entries[i] * entries[j] * entries[k];
                        }
                    }
                }
            }

            return default;
        }
    }
}