using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode2020.Days
{
    [MemoryDiagnoser]
    public class Day13 : BaseDay<long>
    {
        public override int Day => 13;

        [Benchmark]
        public override long Part1()
        {
            string[] split = Input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            string[] ids = split[1].Split(',');
            int targetTime = int.Parse(split[0]);
            int closestTime = int.MaxValue;
            int closestId = 0;

            for (int i = 0; i < ids.Length; i++)
            {
                if (ids[i] == "x") continue;
                int id = int.Parse(ids[i]);

                int time = MultipleClosestTo(id, targetTime);

                if (time >= targetTime && time < closestTime)
                {
                    closestTime = time;
                    closestId = id;
                }
            }

            return closestId * (closestTime - targetTime);
        }

        [Benchmark]
        public override long Part2()
        {
            // TODO: Finish this without brute-force

            return 0;

            /*
            string[] split = Input.Split('\n', StringSplitOptions.RemoveEmptyEntries)[1].Split(',');

            var ids = new Dictionary<int, int>();

            int range = 0;

            for (int i = 0; i < split.Length; i++)
            {
                if (i != 0 && i != split.Length - 1 && split[i] != "x")
                    ids.Add(int.Parse(split[i]), i);

                range++;
            }

            int firstId = int.Parse(split[0]);
            int lastId = int.Parse(split[split.Length - 1]);
            long startRange = MultipleClosestTo(firstId, 100000000000000);

            int valid = 0;

            while (valid != ids.Count)
            {
                // the timestamp must be multiple of the first id.
                startRange += firstId;
                if (MultipleHigherThan(lastId, startRange) - range + 1 != startRange)
                    continue;

                valid = 0;

                foreach (var pair in ids)
                {
                    long time = MultipleClosestTo(pair.Key, startRange);

                    if (time >= startRange && time <= startRange + range - 1 && time - pair.Value == startRange)
                        valid++;
                }
            }

            return startRange;
            */
        }

        private static long MultipleClosestTo(long m, long n)
        {
            long result = n + m / 2;
            result -= result % m;
            return result;
        }

        private static int MultipleClosestTo(int m, int n)
        {
            int result = n + m / 2;
            result -= result % m;
            return result;
        }

        private static long MultipleHigherThan(long n, long min)
            => (int)Math.Ceiling((double)min / n) * n;
    }
}