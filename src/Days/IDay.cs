using System;

namespace AdventOfCode2020.Days
{
    /// <summary>
    /// Represents the base of Advent of Code days.
    /// </summary>
    /// <typeparam name="TInput">The input type, this must implement <see cref="IConvertible"/>.</typeparam>
    /// <typeparam name="TResult">The result type.</typeparam>
    public interface IDay<in TInput, out TResult> where TInput : IConvertible
    {
        /// <summary>
        /// Gets the day number.
        /// </summary>
        public int Day { get; }

        /// <summary>
        /// Solves the problems of this day, displaying the result of both parts and their execution time.
        /// </summary>
        public void Solve();

        /// <summary>
        /// Gets the result of the part 1 of this day.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The result.</returns>
        public TResult Part1(TInput[] input);

        /// <summary>
        /// Gets the result of the part 2 of this day.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The result.</returns>
        public TResult Part2(TInput[] input);
    }
}