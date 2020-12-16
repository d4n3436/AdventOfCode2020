using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode2020.Days
{
    [MemoryDiagnoser]
    public class Day15 : BaseDay<int>
    {
        public override int Day => 15;

        [Benchmark]
        public override int Part1()
            => GetNumberSpokenAt(2020 - 1, Array.ConvertAll(Input.Split(','), int.Parse));

        [Benchmark]
        public override int Part2()
            => GetNumberSpokenAt(30000000 - 1, Array.ConvertAll(Input.Split(','), int.Parse));

        private static int GetNumberSpokenAt(int index, int[] initial)
        {
            int[] turns = new int[index + 1];
            var dict = new Dictionary<int, (int, int)>();

            for (int i = 0; i < turns.Length; i++)
            {
                if (i < initial.Length)
                    turns[i] = initial[i];
                else if (dict.TryGetValue(turns[i - 1], out var last) && last.Item1 != -1 && last.Item2 != -1)
                    turns[i] = last.Item1 - last.Item2;

                if (dict.TryGetValue(turns[i], out var current))
                    dict[turns[i]] = (i, current.Item1);
                else
                    dict[turns[i]] = (i, -1);
            }

            return turns[index];
        }
    }
}