using System;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode2020.Days
{
    [MemoryDiagnoser]
    public class Day12 : BaseDay<int>
    {
        public override int Day => 12;

        [Benchmark]
        [ArgumentsSource(nameof(Input))]
        public override int Part1(string input)
        {
            string[] split = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            char[] directions = { 'N', 'E', 'S', 'W' };
            char currentDir = 'E';
            int ewPos = 0;
            int nsPos = 0;

            for (int i = 0; i < split.Length; i++)
            {
                char dir = split[i][0];
                int value = int.Parse(split[i].Remove(0, 1));

                switch (dir)
                {
                    case 'F':
                        switch (currentDir)
                        {
                            case 'E':
                                ewPos += value;
                                break;

                            case 'W':
                                ewPos -= value;
                                break;

                            case 'N':
                                nsPos += value;
                                break;

                            case 'S':
                                nsPos -= value;
                                break;
                        }
                        break;

                    case 'E':
                        ewPos += value;
                        break;

                    case 'W':
                        ewPos -= value;
                        break;

                    case 'N':
                        nsPos += value;
                        break;

                    case 'S':
                        nsPos -= value;
                        break;

                    case 'L':
                    case 'R':
                        currentDir = directions[((value != 180 && dir == 'L' ? value + 180 : value) / 90 + Array.IndexOf(directions, currentDir)) % 4];
                        break;
                }
            }

            return Math.Abs(ewPos) + Math.Abs(nsPos);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Input))]
        public override int Part2(string input)
        {
            string[] split = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            int ewPos = 0;
            int nsPos = 0;
            int wpEwPos = 10;
            int wpNsPos = 1;

            for (int i = 0; i < split.Length; i++)
            {
                char dir = split[i][0];
                int value = int.Parse(split[i].Remove(0, 1));

                switch (dir)
                {
                    case 'F':
                        ewPos += wpEwPos * value;
                        nsPos += wpNsPos * value;
                        break;

                    case 'E':
                        wpEwPos += value;
                        break;

                    case 'W':
                        wpEwPos -= value;
                        break;

                    case 'N':
                        wpNsPos += value;
                        break;

                    case 'S':
                        wpNsPos -= value;
                        break;

                    case 'L':
                    case 'R':
                        if (value == 180)
                        {
                            wpNsPos = -wpNsPos;
                            wpEwPos = -wpEwPos;
                        }
                        else
                        {
                            int temp = -wpNsPos;
                            wpNsPos = wpEwPos;
                            wpEwPos = temp;

                            if (value == 90 && dir == 'R' || value == 270 && dir == 'L')
                            {
                                wpNsPos = -wpNsPos;
                                wpEwPos = -wpEwPos;
                            }
                        }
                        break;
                }
            }

            return Math.Abs(ewPos) + Math.Abs(nsPos);
        }
    }
}