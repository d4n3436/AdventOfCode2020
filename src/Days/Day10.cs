using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode2020.Days
{
    [MemoryDiagnoser]
    public class Day10 : BaseDay<long>
    {
        public override int Day => 10;

        [Benchmark]
        public override long Part1()
        {
            var set = new SortedSet<int>(Array.ConvertAll(Input.Split('\n', StringSplitOptions.RemoveEmptyEntries), int.Parse));
            int oneDiff = 0;
            int threeDiff = 0;
            int current = 0;

            foreach (int adapter in set)
            {
                if (adapter - current == 1)
                    oneDiff++;
                else
                    threeDiff++;

                current = adapter;
            }

            threeDiff++;

            return oneDiff * threeDiff;
        }

        [Benchmark]
        public override long Part2()
        {
            // TODO: Finish this

            return 0;

            /*
            string[] split = Input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            int[] arr = new int[split.Length + 2];

            int max = 0;
            for (int i = 1; i < arr.Length - 1; i++)
            {
                int current = int.Parse(split[i - 1]);
                arr[i] = current;
                if (current > max)
                    max = current;
            }

            arr[split.Length] = max + 3;
            Array.Sort(arr);

            long total = 1;
            int group = 0;
            int groups = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                bool isGroup = (i != 0 && arr[i] - arr[i - 1] == 1 && i != arr.Length - 1 && arr[i + 1] - arr[i] == 1)
                    || (i != 0 && arr[i] - arr[i - 1] == 2 && i != arr.Length - 1 && arr[i + 1] - arr[i] == 2);

                if (isGroup)
                {
                    group++;
                }
                else if (group > 0)
                {
                    long fact = Factorial(group + 1);
                    Console.WriteLine($"Add factorial of {group + 1} ({fact}) to total ({total})");
                    total += fact;
                    group = 0;
                    groups++;
                }
            }

            return total + Factorial(groups + 1);
            */
        }

        private static long Factorial(int n)
            => n == 1 ? 1 : n * Factorial(n - 1);
    }
}