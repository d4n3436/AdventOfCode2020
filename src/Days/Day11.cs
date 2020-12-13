using System;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode2020.Days
{
    [MemoryDiagnoser]
    public class Day11 : BaseDay<int>
    {
        public override int Day => 11;

        [Benchmark]
        public override int Part1()
        {
            string[] lines = Input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            char[][] layout = new char[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                layout[i] = lines[i].ToCharArray();
            }

            int occupied = 0;

            while (true)
            {
                //Draw(layout);
                //Console.WriteLine();

                int current = ApplyRules(ref layout, false);
                //Console.WriteLine(occupied);
                if (current == occupied)
                    break;

                occupied = current;
            }

            return occupied;
        }

        [Benchmark]
        public override int Part2()
        {
            string[] lines = Input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            char[][] layout = new char[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                layout[i] = lines[i].ToCharArray();
            }

            int occupied = 0;

            while (true)
            {
                //Draw(layout);
                //Console.WriteLine();

                int current = ApplyRules(ref layout, true);
                //Console.WriteLine(occupied);
                if (current == occupied)
                    break;

                occupied = current;
            }

            return occupied;
        }

        private static int ApplyRules(ref char[][] layout, bool onlyVisible)
        {
            var newLayout = CopyJaggedArray(layout);
            int occupied = 0;

            for (int x = 0; x < layout.Length; x++)
            {
                for (int y = 0; y < layout[x].Length; y++)
                {
                    if (layout[x][y] == '.') continue;
                    int count = onlyVisible
                        ? GetVisibleOccupiedCount(layout, x, y)
                        : GetAdjacentOccupiedCount(layout, x, y);

                    if (layout[x][y] == 'L' && count == 0)
                        newLayout[x][y] = '#';
                    else if (layout[x][y] == '#' && count >= (onlyVisible ? 5 : 4))
                        newLayout[x][y] = 'L';

                    if (newLayout[x][y] == '#')
                        occupied++;
                }
            }

            layout = newLayout;
            return occupied;
        }

        private static int GetAdjacentOccupiedCount(char[][] layout, int x, int y)
        {
            int occupied = 0;

            if (y > 0)
            {
                if (x > 0 && layout[x - 1][y - 1] == '#')
                    occupied++;

                if (layout[x][y - 1] == '#')
                    occupied++;

                if (x + 1 < layout.Length && layout[x + 1][y - 1] == '#')
                    occupied++;
            }

            if (x > 0 && layout[x - 1][y] == '#')
                occupied++;

            if (x + 1 < layout.Length && layout[x + 1][y] == '#')
                occupied++;

            if (y + 1 < layout[x].Length)
            {
                if (x > 0 && layout[x - 1][y + 1] == '#')
                    occupied++;

                if (layout[x][y + 1] == '#')
                    occupied++;

                if (x + 1 < layout.Length && layout[x + 1][y + 1] == '#')
                    occupied++;
            }

            return occupied;
        }

        private static int GetVisibleOccupiedCount(char[][] layout, int x, int y)
        {
            int occupied = 0;
            int i, j;

            // left
            for (j = y - 1; j >= 0; j--)
            {
                if (layout[x][j] == 'L') break;
                if (layout[x][j] == '#')
                {
                    occupied++;
                    break;
                }
            }

            // right
            for (j = y + 1; j < layout[x].Length; j++)
            {
                if (layout[x][j] == 'L') break;
                if (layout[x][j] == '#')
                {
                    occupied++;
                    break;
                }
            }

            // up
            for (i = x - 1; i >= 0; i--)
            {
                if (layout[i][y] == 'L') break;
                if (layout[i][y] == '#')
                {
                    occupied++;
                    break;
                }
            }

            // down
            for (i = x + 1; i < layout.Length; i++)
            {
                if (layout[i][y] == 'L') break;
                if (layout[i][y] == '#')
                {
                    occupied++;
                    break;
                }
            }

            // diagonal up right
            i = x - 1;
            j = y + 1;
            for (; i >= 0 && j < layout[x].Length; i--, j++)
            {
                if (layout[i][j] == 'L') break;
                if (layout[i][j] == '#')
                {
                    occupied++;
                    break;
                }
            }

            // diagonal down right
            i = x + 1;
            j = y + 1;
            for (; i < layout.Length && j < layout[x].Length; i++, j++)
            {
                if (layout[i][j] == 'L') break;
                if (layout[i][j] == '#')
                {
                    occupied++;
                    break;
                }
            }

            // diagonal up left
            i = x - 1;
            j = y - 1;
            for (; i >= 0 && j >= 0; i--, j--)
            {
                if (layout[i][j] == 'L') break;
                if (layout[i][j] == '#')
                {
                    occupied++;
                    break;
                }
            }

            // diagonal down left
            i = x + 1;
            j = y - 1;
            for (; i < layout.Length && j >= 0; i++, j--)
            {
                if (layout[i][j] == 'L') break;
                if (layout[i][j] == '#')
                {
                    occupied++;
                    break;
                }
            }

            return occupied;
        }

        private static void Draw(char[][] layout)
        {
            for (int x = 0; x < layout.Length; x++)
            {
                for (int y = 0; y < layout[x].Length; y++)
                {
                    Console.Write(layout[x][y]);
                }

                Console.WriteLine();
            }
        }

        private static T[][] CopyJaggedArray<T>(T[][] source)
        {
            var dest = new T[source.Length][];

            for (var i = 0; i < source.Length; i++)
            {
                var inner = source[i];
                var newer = new T[inner.Length];
                Array.Copy(inner, newer, inner.Length);
                dest[i] = newer;
            }

            return dest;
        }
    }
}