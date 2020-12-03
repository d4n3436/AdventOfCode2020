using BenchmarkDotNet.Attributes;

namespace AdventOfCode2020.Days
{
    public class Day3 : BaseDay<string, int>
    {
        public override int Day => 3;

        [Benchmark]
        [ArgumentsSource(nameof(Input))]
        public override int Part1(string[] map)
            => GetEncounteredTrees(map, 3, 1);

        [Benchmark]
        [ArgumentsSource(nameof(Input))]
        public override int Part2(string[] map)
        {
            var slopes = new[]
            {
                (1, 1),
                (3, 1),
                (5, 1),
                (7, 1),
                (1, 2)
            };

            int count = 1;

            for (int i = 0; i < slopes.Length; i++)
            {
                count *= GetEncounteredTrees(map, slopes[i].Item1, slopes[i].Item2);
            }

            return count;
        }

        private static int GetEncounteredTrees(string[] map, int right, int down)
        {
            int count = 0;
            int y = 0;

            for (int x = 0; x < map.Length; x += down)
            {
                if (map[x][y] == '#')
                {
                    count++;
                }

                y += right;
                y %= map[x].Length;
            }

            return count;
        }
    }
}