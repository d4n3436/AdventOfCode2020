using System;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode2020.Days
{
    [MemoryDiagnoser]
    public class Day8 : BaseDay<int>
    {
        public override int Day => 8;

        [Benchmark]
        [ArgumentsSource(nameof(Input))]
        public override int Part1(string input)
        {
            var interpreter = new Interpreter(input);
            while (!interpreter.InfiniteLoop)
            {
                interpreter.Step();
            }

            return interpreter.Accumulator;
        }

        [Benchmark]
        [ArgumentsSource(nameof(Input))]
        public override int Part2(string input)
        {
            var interpreter = new Interpreter(input);
            while (!interpreter.End)
            {
                interpreter.Step(true);
            }

            return interpreter.Accumulator;
        }

        private class Interpreter
        {
            private enum OpCode { Nop, Acc, Jmp }

            public int Accumulator;
            public bool InfiniteLoop;
            public bool End;

            private readonly (OpCode, int)[] _ops;
            private readonly bool[] _executed;
            private int _pos;

            public Interpreter(string input)
            {
                var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                _ops = new (OpCode, int)[lines.Length];
                _executed = new bool[_ops.Length];

                for (int i = 0; i < _ops.Length; i++)
                {
                    string[] line = lines[i].Split(' ');
                    var op = line[0] == "acc" ? OpCode.Acc
                        : line[0] == "jmp" ? OpCode.Jmp
                        : OpCode.Nop;

                    _ops[i] = (op, int.Parse(line[1]));
                }
            }

            private Interpreter((OpCode, int)[] ops)
            {
                _ops = ops;
                _executed = new bool[_ops.Length];
            }

            public void Step(bool fix = false)
            {
                if (InfiniteLoop || End) return;

                var (op, arg) = _ops[_pos];
                int prevPos = _pos;
                switch (op)
                {
                    case OpCode.Acc:
                        Accumulator += arg;
                        _pos++;
                        break;

                    case OpCode.Jmp:
                        _pos += arg;
                        break;

                    default:
                        _pos++;
                        break;
                }

                if (_pos >= _ops.Length)
                {
                    End = true;
                }
                else if (fix)
                {
                    if (op == OpCode.Acc || (op == OpCode.Nop && arg == 0)) return;
                    _ops[prevPos] = (op == OpCode.Jmp ? OpCode.Nop : OpCode.Jmp, arg);
                    var interpreter = new Interpreter(_ops);

                    while (!interpreter.InfiniteLoop && !interpreter.End)
                    {
                        interpreter.Step();
                    }

                    if (interpreter.End)
                    {
                        Accumulator = interpreter.Accumulator;
                        End = true;
                    }
                    else
                    {
                        _ops[prevPos] = (op, arg);
                    }
                }
                else
                {
                    if (_executed[_pos])
                        InfiniteLoop = true;
                    else
                        _executed[_pos] = true;
                }
            }
        }
    }
}