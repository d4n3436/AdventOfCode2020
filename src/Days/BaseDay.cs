using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Days
{
    /// <summary>
    /// Represents the base on which the "Day" classes inherit.
    /// </summary>
    /// <typeparam name="TInput">The input type, this must implement <see cref="IConvertible"/>.</typeparam>
    /// <typeparam name="TResult">The result type.</typeparam>
    public abstract class BaseDay<TInput, TResult> : IDay<TInput, TResult> where TInput : IConvertible
    {
        /// <inheritdoc/>
        public abstract int Day { get; }

        private TInput[] _singleInput;

        private TInput[] SingleInput => _singleInput ??= GetInput();

        public IEnumerable<TInput[]> Input
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

            var input = SingleInput;

            var sw = Stopwatch.StartNew();
            TResult part1 = Part1(input);
            sw.Stop();

            var sw2 = Stopwatch.StartNew();
            TResult part2 = Part2(input);
            sw2.Stop();

            Console.WriteLine($"Part 1: {part1}, {sw.Elapsed.TotalMilliseconds}ms");
            Console.WriteLine($"Part 2: {part2}, {sw2.Elapsed.TotalMilliseconds}ms");
            Console.WriteLine("-------------------------------------");
        }

        private TInput[] GetInput()
        {
            if (!File.Exists($"Inputs/day{Day}.txt"))
            {
                return Array.Empty<TInput>();
            }

            string[] rawInput;
            try
            {
                rawInput = File.ReadAllLines($"Inputs/day{Day}.txt");
            }
            catch (Exception)
            {
                return Array.Empty<TInput>();
            }

            if (rawInput.Length == 0)
            {
                return Array.Empty<TInput>();
            }

            bool isConvertRequired = rawInput.GetType().GetElementType() != typeof(TInput);
            try
            {
                return isConvertRequired
                    ? rawInput.Select(x => (TInput)Convert.ChangeType(x, typeof(TInput))).ToArray()
                    : (TInput[])(object)rawInput;
            }
            catch (Exception e) when (e is InvalidCastException || e is FormatException || e is OverflowException)
            {
                return Array.Empty<TInput>();
            }
        }

        /// <inheritdoc/>
        public abstract TResult Part1(TInput[] input);

        /// <inheritdoc/>
        public abstract TResult Part2(TInput[] input);
    }
}