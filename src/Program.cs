using System;
using System.Linq;
using AdventOfCode2020.Days;
using BenchmarkDotNet.Running;

namespace AdventOfCode2020
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("Advent of Code 2020");
            Console.WriteLine();
            Console.WriteLine("Display (s)imple output (results and simple benchmark)\nor (c)omplete output (accurate benchmark)?");
            Console.WriteLine();

            Console.Write("Input: ");

            string input = Console.ReadLine()?.ToLowerInvariant();

            if (input == null || input.Length != 1)
            {
                Console.WriteLine("Invalid input.");
            }
            else
            {
                switch (input[0])
                {
                    case 's':
                        var days = typeof(Program).Assembly
                            .GetTypes()
                            .Where(x => x.IsClass && !x.IsAbstract && IsSubclassOfGeneric(typeof(BaseDay<,>), x))
                            .Select(Activator.CreateInstance)
                            .ToArray();

                        foreach (var day in days)
                        {
                            var method = day.GetType().GetMethod("Solve");
                            method?.Invoke(day, null);
                        }

                        break;

                    case 'c':
                        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run();
                        break;

                    default:
                        Console.WriteLine("Invalid input.");
                        break;
                }
            }

            Console.WriteLine();
            Console.Write("Press any key to exit...");
            Console.ReadKey(false);
        }

        private static bool IsSubclassOfGeneric(Type genericType, Type typeToCheck)
        {
            while (typeToCheck != null && typeToCheck != typeof(object))
            {
                var cur = typeToCheck.IsGenericType ? typeToCheck.GetGenericTypeDefinition() : typeToCheck;
                if (genericType == cur)
                {
                    return true;
                }
                typeToCheck = typeToCheck.BaseType;
            }
            return false;
        }
    }
}