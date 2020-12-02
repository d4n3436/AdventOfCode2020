using System;
using AdventOfCode2020.Days;

namespace AdventOfCode2020
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("Advent of Code 2020");

            new Day1().Solve();
            new Day2().Solve();

            Console.ReadLine();
        }
    }
}