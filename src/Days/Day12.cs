using System;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode2020.Days
{
    [MemoryDiagnoser]
    public class Day12 : BaseDay<int>
    {
        public override int Day => 12;

        [Benchmark]
        public override int Part1()
        {
            string[] split = Input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            char[] directions = { 'N', 'E', 'S', 'W' };
            char currentDir = 'E';
            int ewPos = 0;
            int nsPos = 0;

            for (int i = 0; i < split.Length; i++)
            {
                char dir = split[i][0];
                int value = int.Parse(split[i].Remove(0, 1));

                if (dir == 'F')
                {
                    if (currentDir == 'E')
                        ewPos += value;
                    else if (currentDir == 'W')
                        ewPos -= value;
                    else if (currentDir == 'N')
                        nsPos += value;
                    else if (currentDir == 'S')
                        nsPos -= value;
                }
                else if (dir == 'E')
                    ewPos += value;
                else if (dir == 'W')
                    ewPos -= value;
                else if (dir == 'N')
                    nsPos += value;
                else if (dir == 'S')
                    nsPos -= value;
                else
                    currentDir = directions[((value != 180 && dir == 'L' ? value + 180 : value) / 90 + Array.IndexOf(directions, currentDir)) % 4];
            }

            return Math.Abs(ewPos) + Math.Abs(nsPos);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Input))]
        public override int Part2()
        {
            string[] split = Input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            int ewPos = 0;
            int nsPos = 0;
            int wpEwPos = 10;
            int wpNsPos = 1;

            for (int i = 0; i < split.Length; i++)
            {
                char dir = split[i][0];
                int value = int.Parse(split[i].Remove(0, 1));

                if (dir == 'F')
                {
                    ewPos += wpEwPos * value;
                    nsPos += wpNsPos * value;
                }
                else if (dir == 'E')
                    wpEwPos += value;
                else if (dir == 'W')
                    wpEwPos -= value;
                else if (dir == 'N')
                    wpNsPos += value;
                else if (dir == 'S')
                    wpNsPos -= value;
                else
                {
                    if (value != 180)
                    {
                        int temp = -wpNsPos;
                        wpNsPos = wpEwPos;
                        wpEwPos = temp;
                    }

                    if (value == 180 || value == 90 && dir == 'R' || value == 270 && dir == 'L')
                    {
                        wpNsPos = -wpNsPos;
                        wpEwPos = -wpEwPos;
                    }
                }
            }

            return Math.Abs(ewPos) + Math.Abs(nsPos);
        }
    }
}