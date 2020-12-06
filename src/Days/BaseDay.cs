﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AdventOfCode2020.Days
{
    /// <summary>
    /// Represents the base on which the "Day" classes inherit.
    /// </summary>
    /// <typeparam name="TResult">The result type.</typeparam>
    public abstract class BaseDay<TResult> : IDay<TResult>
    {
        /// <inheritdoc/>
        public abstract int Day { get; }

        private string _singleInput;

        private string SingleInput => _singleInput ??= GetInput();

        public IEnumerable<string> Input
        {
            get
            {
                yield return SingleInput;
            }
        }

        /// <inheritdoc/>
        public void Solve()
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine($"Day {Day}");
            Console.WriteLine();

            if (SingleInput == null)
            {
                Console.WriteLine($"Error: Could not get the text from the input file. (Inputs/day{Day}.txt)");
            }
            else
            {
                var sw = Stopwatch.StartNew();
                TResult part1 = Part1(SingleInput);
                sw.Stop();

                var sw2 = Stopwatch.StartNew();
                TResult part2 = Part2(SingleInput);
                sw2.Stop();

                Console.WriteLine($"Part 1: {part1}, {sw.Elapsed.TotalMilliseconds}ms");
                Console.WriteLine($"Part 2: {part2}, {sw2.Elapsed.TotalMilliseconds}ms");
            }

            Console.WriteLine("-------------------------------------");
        }

        private string GetInput()
        {
            if (!File.Exists($"Inputs/day{Day}.txt"))
            {
                return null;
            }

            string rawInput;
            try
            {
                rawInput = File.ReadAllText($"Inputs/day{Day}.txt");
            }
            catch (Exception)
            {
                return null;
            }

            return rawInput.Length == 0 ? null : rawInput;
        }

        /// <inheritdoc/>
        public abstract TResult Part1(string input);

        /// <inheritdoc/>
        public abstract TResult Part2(string input);
    }
}