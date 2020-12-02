namespace AdventOfCode2020.Days
{
    public class Day1 : BaseDay<int, int>
    {
        public override int Day => 1;

        private const int Target = 2020;

        public override int Part1(int[] entries)
        {
            for (int i = 0; i < entries.Length; i++)
            {
                for (int j = 0; j < entries.Length; j++)
                {
                    if (entries[i] + entries[j] == Target)
                    {
                        return entries[i] * entries[j];
                    }
                }
            }

            return default;
        }

        public override int Part2(int[] entries)
        {
            for (int i = 0; i < entries.Length; i++)
            {
                for (int j = 0; j < entries.Length; j++)
                {
                    for (int k = 0; k < entries.Length; k++)
                    {
                        if (entries[i] + entries[j] + entries[k] == Target)
                        {
                            return entries[i] * entries[j] * entries[k];
                        }
                    }
                }
            }

            return default;
        }
    }
}