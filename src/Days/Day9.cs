using System;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode2020.Days
{
    [MemoryDiagnoser]
    public class Day9 : BaseDay<long>
    {
        public override int Day => 9;

        [Benchmark]
        public override long Part1()
        {
            long[] arr = Array.ConvertAll(Input.Split('\n', StringSplitOptions.RemoveEmptyEntries), long.Parse);

            return FindInvalid(arr, 25);
        }

        [Benchmark]
        public override long Part2()
        {
            long[] arr = Array.ConvertAll(Input.Split('\n', StringSplitOptions.RemoveEmptyEntries), long.Parse);
            long invalid = FindInvalid(arr, 25);
            long[] precomputed = Precompute(arr);

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 2; j < arr.Length; j++)
                {
                    long sum = RangeSum(precomputed, i, j);

                    if (sum != invalid) continue;

                    long min = sum;
                    long max = sum;

                    for (int k = i; k < j; k++)
                    {
                        if (arr[k] > max)
                            max = arr[k];

                        if (arr[k] < min)
                            min = arr[k];
                    }
                    return min + max;
                }
            }

            return default;
        }

        private static long FindInvalid(long[] arr, int preamble)
        {
            for (int i = preamble; i < arr.Length; i++)
            {
                bool valid = false;
                for (int j = i - preamble; j < i; j++)
                {
                    for (int k = i - preamble; k < i; k++)
                    {
                        if (arr[j] != arr[k] && arr[j] + arr[k] == arr[i])
                        {
                            valid = true;
                            break;
                        }
                    }
                    if (valid)
                        break;
                }
                if (!valid)
                    return arr[i];
            }

            return default;
        }

        private static long[] Precompute(long[] arr)
        {
            long[] pre = new long[arr.Length];
            pre[0] = arr[0];

            for (int i = 1; i < arr.Length; i++)
                pre[i] = arr[i] + pre[i - 1];

            return pre;
        }

        private static long RangeSum(long[] pre, int i, int j)
            => i == 0 ? pre[j] : pre[j] - pre[i - 1];
    }
}