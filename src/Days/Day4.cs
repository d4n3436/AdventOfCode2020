using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode2020.Days
{
    public class Day4 : BaseDay<string, int>
    {
        public override int Day => 4;

        [Benchmark]
        [ArgumentsSource(nameof(Input))]
        public override int Part1(string[] input) => GetValidPassports(input, false);

        [Benchmark]
        [ArgumentsSource(nameof(Input))]
        public override int Part2(string[] input) => GetValidPassports(input, true);

        private static int GetValidPassports(string[] input, bool strict)
        {
            string passport = "";
            int valid = 0;

            for (int i = 0; i < input.Length; i++)
            {
                // I had to do it this way because the input was already split.
                if (input[i].Length != 0)
                {
                    passport += $" {input[i]}";
                }
                else
                {
                    if (IsValid(passport, strict))
                        valid++;

                    passport = "";
                }
            }

            return valid;
        }

        private static bool IsValid(string passport, bool strict)
        {
            var split = passport.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var dict = new Dictionary<string, string>(7);

            int fieldCount = 0;
            for (int i = 0; i < split.Length; i++)
            {
                var split2 = split[i].Split(':');
                dict.Add(split2[0], split2[1]);
                if (Array.Exists(_fields, x => x == split2[0]))
                    fieldCount++;
            }

            if (fieldCount < 7) return false;
            if (!strict) return true;

            if (!int.TryParse(dict["byr"], out int birthYear) || birthYear < 1920 || birthYear > 2002)
                return false;

            if (!int.TryParse(dict["iyr"], out int issueYear) || issueYear < 2010 || issueYear > 2020)
                return false;

            if (!int.TryParse(dict["eyr"], out int expirationYear) || expirationYear < 2020 || expirationYear > 2030)
                return false;

            if (dict["hgt"].Length < 3 || int.TryParse(dict["hgt"], out _))
                return false;

            int.TryParse(dict["hgt"].Substring(0, dict["hgt"].Length - 2), out int height);
            string unit = dict["hgt"].Substring(dict["hgt"].Length - 2, 2);

            switch (unit)
            {
                case "cm":
                    if (height < 150 || height > 193)
                        return false;
                    break;

                case "in":
                    if (height < 59 || height > 76)
                        return false;
                    break;

                default:
                    return false;
            }

            if (dict["hcl"][0] != '#')
                return false;

            for (int i = 1; i < dict["hcl"].Length; i++)
            {
                if (!IsHex(dict["hcl"][i]))
                    return false;
            }

            if (!Array.Exists(_validEyeColors, x => x == dict["ecl"]))
                return false;

            return dict["pid"].Length == 9 && IsDigit(dict["pid"]);
        }

        private static readonly string[] _validEyeColors =
        {
            "amb", "blu", "brn", "gry", "grn", "hzl", "oth"
        };

        private static readonly string[] _fields =
        {
            "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"
        };

        private static bool IsDigit(string s)
        {
            for (var i = 0; i < s.Length; i++)
            {
                if (s[i] < '0' || s[i] > '9')
                    return false;
            }

            return true;
        }

        private static bool IsHex(char c)
        {
            return (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f');
        }
    }
}