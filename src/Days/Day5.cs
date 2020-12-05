using System;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode2020.Days
{
    public class Day5 : BaseDay<string, int>
    {
        public override int Day => 5;

        [Benchmark]
        [ArgumentsSource(nameof(Input))]
        public override int Part1(string[] input)
        {
            int max = 0;

            for (int i = 0; i < input.Length; i++)
            {
                int id = DecodeSeat(input[i]);
                if (id > max)
                    max = id;
            }

            return max;
        }

        [Benchmark]
        [ArgumentsSource(nameof(Input))]
        public override int Part2(string[] input)
        {
            int sum = 0;
            int min = input.Length;
            int max = 0;

            for (int i = 0; i < input.Length; i++)
            {
                int id = DecodeSeat(input[i]);
                if (id > max)
                    max = id;
                if (id < min)
                    min = id;
                sum += id;
            }

            int seat = ((max + min) * (max - min + 1) / 2) - sum;

            return seat;
        }

        private static int DecodeSeat(string seat)
        {
            return Convert.ToInt32(seat
                .Replace('L', '0')
                .Replace('B', '1')
                .Replace('F', '0')
                .Replace('R', '1'), 2);
        }
    }
}