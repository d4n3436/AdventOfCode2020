using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode2020.Days
{
    [MemoryDiagnoser]
    public class Day14 : BaseDay<ulong>
    {
        public override int Day => 14;

        [Benchmark]
        public override ulong Part1()
        {
            string[] program = Input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var pattern = new Regex(@"mem\[([0-9]+)] = ([0-9]+)", RegexOptions.Compiled);
            string mask = "";
            var memory = new Dictionary<int, ulong>();

            for (int i = 0; i < program.Length; i++)
            {
                if (program[i].StartsWith("mask = "))
                {
                    mask = program[i].Remove(0, 7);
                }
                else
                {
                    var match = pattern.Match(program[i]);
                    int address = int.Parse(match.Groups[1].Value);
                    ulong value = ulong.Parse(match.Groups[2].Value);

                    memory[address] = ApplyMask(mask, value);
                }
            }

            ulong sum = 0;
            foreach (ulong value in memory.Values)
            {
                sum += value;
            }

            return sum;
        }

        [Benchmark]
        public override ulong Part2()
        {
            string[] program = Input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var pattern = new Regex(@"mem\[([0-9]+)] = ([0-9]+)", RegexOptions.Compiled);
            string mask = "";
            var memory = new Dictionary<ulong, ulong>();

            for (int i = 0; i < program.Length; i++)
            {
                if (program[i].StartsWith("mask = "))
                {
                    mask = program[i].Remove(0, 7);
                }
                else
                {
                    var match = pattern.Match(program[i]);
                    string address = Convert.ToString(long.Parse(match.Groups[1].Value), 2);
                    ulong value = ulong.Parse(match.Groups[2].Value);
                    char[] result = address.PadLeft(36, '0').ToCharArray();

                    for (int j = 0; j < mask.Length; j++)
                    {
                        if (mask[j] != '0')
                            result[j] = mask[j];
                    }

                    var addresses = GetPermutations(result);

                    for (int j = 0; j < addresses.Length; j++)
                    {
                        memory[addresses[j]] = value;
                    }
                }
            }

            ulong sum = 0;
            foreach (ulong value in memory.Values)
            {
                sum += value;
            }

            return sum;
        }

        private static ulong ApplyMask(string mask, ulong value)
        {
            for (int j = 0; j < mask.Length; j++)
            {
                if (mask[j] == '1')
                    value |= 1UL << 35 - j;
                else if (mask[j] == '0')
                    value &= ~(1UL << 35 - j);
            }

            return value;
        }

        private static ulong[] GetPermutations(char[] mask)
        {
            // get indexes
            var indexes = new List<int>();

            for (int i = 0; i < mask.Length; i++)
            {
                if (mask[i] == 'X')
                    indexes.Add(i);
            }

            int n = (int)Math.Pow(2, indexes.Count);
            ulong[] addresses = new ulong[n];

            for (int i = 0; i < n; i++)
            {
                char[] permutation = new char[mask.Length];
                Array.Copy(mask, permutation, mask.Length);

                for (int j = 0; j < indexes.Count; j++)
                {
                    permutation[indexes[j]] = (i >> j & 1) != 0 ? '1' : '0';
                }

                ulong pAddress = Convert.ToUInt64(string.Concat(permutation), 2);
                addresses[i] = pAddress;
            }

            return addresses;
        }
    }
}