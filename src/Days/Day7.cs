using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode2020.Days
{
    [MemoryDiagnoser]
    public class Day7 : BaseDay<int>
    {
        public override int Day => 7;

        [Benchmark]
        [ArgumentsSource(nameof(Input))]
        public override int Part1(string input) => GetShinyBagCount(ParseBags(input));

        [Benchmark]
        [ArgumentsSource(nameof(Input))]
        public override int Part2(string input) => GetBagCount("shiny gold", ParseBags(input)) - 1;

        private static Dictionary<string, Dictionary<string, int>> ParseBags(string input)
        {
            string[] lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var bags = new Dictionary<string, Dictionary<string, int>>();

            for (int i = 0; i < lines.Length; i++)
            {
                string[] split = lines[i].Split(" bags contain ");
                bags.Add(split[0], new Dictionary<string, int>());
                if (split[1] == "no other bags.") continue;

                string[] split2 = split[1].Split(", ");
                for (int j = 0; j < split2.Length; j++)
                {
                    string[] split3 = split2[j].Split(' ');
                    bags[split[0]][$"{split3[1]} {split3[2]}"] = int.Parse(split3[0]);
                }
            }

            return bags;
        }

        private static int GetShinyBagCount(Dictionary<string, Dictionary<string, int>> bags)
        {
            int count = 0;

            foreach (var bag in bags)
            {
                if (HasBag(bag.Key, bags))
                    count++;
            }

            return count;
        }

        private static bool HasBag(string type, Dictionary<string, Dictionary<string, int>> bags)
        {
            foreach (var bag in bags[type])
            {
                if (bag.Key == "shiny gold" || HasBag(bag.Key, bags))
                    return true;
            }

            return false;
        }

        private static int GetBagCount(string type, Dictionary<string, Dictionary<string, int>> bags)
        {
            int count = 1;

            foreach (var bag in bags[type])
            {
                count += bag.Value * (!bags.ContainsKey(type) ? 1 : GetBagCount(bag.Key, bags));
            }

            return count;
        }
    }
}