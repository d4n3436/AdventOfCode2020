using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Days
{
    /// <summary>
    /// Represents the base of the "Day" classes inherits. This class only implements <see cref="Solve()"/>.
    /// </summary>
    /// <typeparam name="TInput">The input type, this must implement <see cref="IConvertible"/>.</typeparam>
    /// <typeparam name="TResult">The result type.</typeparam>
    public abstract class BaseDay<TInput, TResult> : IDay<TInput, TResult> where TInput : IConvertible
    {
        /// <inheritdoc/>
        public abstract int Day { get; }

        /// <inheritdoc/>
        public void Solve()
        {
            string[] rawInput = File.ReadAllLines($"Inputs/day{Day}.txt");
            bool isConvertRequired = rawInput.GetType().GetElementType() != typeof(TInput);

            TInput[] input = isConvertRequired
                ? rawInput.Select(x => (TInput)Convert.ChangeType(x, typeof(TInput))).ToArray()
                : (TInput[])(object)rawInput;

            Console.WriteLine("-------------------------------------");
            Console.WriteLine($"Day {Day}");
            Console.WriteLine();

            var sw = Stopwatch.StartNew();
            TResult day1Part1 = Part1(input);
            sw.Stop();

            var sw2 = Stopwatch.StartNew();
            TResult day1Part2 = Part2(input);
            sw2.Stop();

            Console.WriteLine($"Part 1: {day1Part1}, {sw.Elapsed.TotalMilliseconds}ms");
            Console.WriteLine($"Part 2: {day1Part2}, {sw2.Elapsed.TotalMilliseconds}ms");
            Console.WriteLine("-------------------------------------");
        }

        /// <inheritdoc/>
        public abstract TResult Part1(TInput[] input);

        /// <inheritdoc/>
        public abstract TResult Part2(TInput[] input);
    }
}