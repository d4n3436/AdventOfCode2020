using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode2020.Days
{
    [MemoryDiagnoser]
    public class Day16 : BaseDay<long>
    {
        public override int Day => 16;

        [Benchmark]
        public override long Part1()
        {
            string[] parts = Input.Split("\n\n");
            string[] rawRules = parts[0].Split('\n');
            int[][] rules = new int[rawRules.Length][];

            for (int i = 0; i < rawRules.Length; i++)
            {
                rules[i] = ParseRule(rawRules[i]);
            }

            int rate = 0;
            string[] rawTickets = parts[2].Split('\n', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 1; i < rawTickets.Length; i++)
            {
                int[] ticket = Array.ConvertAll(rawTickets[i].Split(','), int.Parse);

                for (int j = 0; j < ticket.Length; j++)
                {
                    int invalid = 0;

                    for (int k = 0; k < rules.Length; k++)
                    {
                        if (!IsValidField(ticket[j], rules[k]))
                            invalid++;
                    }

                    if (invalid == rules.Length)
                        rate += ticket[j];
                }
            }

            return rate;
        }

        [Benchmark]
        public override long Part2()
        {
            string[] parts = Input.Split("\n\n");
            string[] rawRules = parts[0].Split('\n');
            var rules = new Dictionary<string, int[]>();

            // parse rules
            for (int i = 0; i < rawRules.Length; i++)
            {
                rules.Add(rawRules[i].Split(':')[0], ParseRule(rawRules[i]));
            }

            string[] rawTickets = parts[2].Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var tickets = new List<int[]>
            {
                // my ticket
                Array.ConvertAll(parts[1].Split('\n')[1].Split(','), int.Parse)
            };

            // parse and discard tickets
            for (int i = 1; i < rawTickets.Length; i++)
            {
                int[] ticket = Array.ConvertAll(rawTickets[i].Split(','), int.Parse);

                if (IsValidTicket(ticket, rules.Values))
                    tickets.Add(ticket);
            }

            int[][] ticketArr = tickets.ToArray();
            var orderedRules = new Dictionary<string, int>();
            var candidates = new List<string>(rules.Keys);
            var discarded = new List<int>();
            bool[][] valid = new bool[ticketArr.Length][];

            // get rules in order
            while (discarded.Count != rules.Count)
            {
                for (int i = 0; i < rules.Count; i++)
                {
                    if (discarded.Contains(i))
                        continue;

                    valid[i] = new bool[rules.Count];
                    int[] column = GetColumn(ticketArr, i);

                    int j = 0;
                    int count = 0;
                    int validPos = 0;

                    foreach ((string name, int[] rule) in rules)
                    {
                        valid[i][j] = !orderedRules.ContainsKey(name) && IsValidTicket(column, rule);
                        if (valid[i][j])
                        {
                            validPos = j;
                            count++;
                        }

                        j++;
                    }

                    if (count == 1)
                    {
                        orderedRules.Add(candidates[validPos], i);
                        discarded.Add(i);
                        break;
                    }
                }
            }

            long result = 1;

            foreach (var pair in orderedRules)
            {
                if (pair.Key.StartsWith("departure"))
                    result *= ticketArr[0][pair.Value];
            }

            return result;
        }

        private static int[] ParseRule(string rawRule)
        {
            return Array.ConvertAll(
                rawRule
                    .Remove(0, rawRule.IndexOf(':') + 2)
                    .Replace(" or ", "-")
                    .Split('-')
                , int.Parse);
        }

        private static bool IsValidTicket(int[] ticket, ICollection<int[]> rules)
        {
            for (int i = 0; i < ticket.Length; i++)
            {
                int invalid = 0;

                foreach (var rule in rules)
                {
                    if (!IsValidField(ticket[i], rule))
                        invalid++;
                }

                if (invalid == rules.Count)
                    return false;
            }

            return true;
        }

        private static bool IsValidTicket(int[] ticket, int[] rule)
        {
            for (int i = 0; i < ticket.Length; i++)
            {
                if (!IsValidField(ticket[i], rule))
                    return false;
            }

            return true;
        }

        private static bool IsValidField(int field, int[] rule)
        {
            return field >= rule[0] && field <= rule[1]
                || field >= rule[2] && field <= rule[3];
        }

        private static T[] GetColumn<T>(T[][] arr, int pos)
        {
            T[] column = new T[arr.Length];

            for (int i = 0; i < arr.Length; i++)
            {
                column[i] = arr[i][pos];
            }

            return column;
        }
    }
}