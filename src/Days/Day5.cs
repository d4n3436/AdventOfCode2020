using System;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode2020.Days
{
    [MemoryDiagnoser]
    public class Day5 : BaseDay<int>
    {
        public override int Day => 5;

        [Benchmark]
        [ArgumentsSource(nameof(Input))]
        public override int Part1(string input)
        {
            string[] lines = DecodeAllSeats(input).Split('\n', StringSplitOptions.RemoveEmptyEntries);
            int max = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                int id = Convert.ToInt32(lines[i], 2);
                if (id > max)
                    max = id;
            }

            return max;
        }

        [Benchmark]
        [ArgumentsSource(nameof(Input))]
        public override int Part2(string input)
        {
            string[] lines = DecodeAllSeats(input).Split('\n', StringSplitOptions.RemoveEmptyEntries);
            int sum = 0;
            int min = lines.Length;
            int max = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                int id = Convert.ToInt32(lines[i], 2);

                if (id > max)
                    max = id;

                if (id < min)
                    min = id;

                sum += id;
            }

            int seat = ((max + min) * (max - min + 1) / 2) - sum;

            return seat;
        }

        private static string DecodeAllSeats(string seat)
        {
            return seat.Replace('L', '0')
                .Replace('B', '1')
                .Replace('F', '0')
                .Replace('R', '1');
        }
    }
}