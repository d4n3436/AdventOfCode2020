namespace AdventOfCode2020.Days
{
    /// <summary>
    /// Represents the base of Advent of Code days.
    /// </summary>
    /// <typeparam name="TResult">The result type.</typeparam>
    public interface IDay<out TResult> : IDay
    {
        /// <summary>
        /// Gets the result of the part 1 of this day.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The result.</returns>
        public TResult Part1(string input);

        /// <summary>
        /// Gets the result of the part 2 of this day.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The result.</returns>
        public TResult Part2(string input);
    }

    /// <summary>
    /// Represents the non-generic base of Advent of Code days.
    /// </summary>
    public interface IDay
    {
        /// <summary>
        /// Gets the day number.
        /// </summary>
        public int Day { get; }

        /// <summary>
        /// Solves the problems of this day, displaying the result of both parts and their execution time.
        /// </summary>
        public void Solve();
    }
}