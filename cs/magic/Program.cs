using System;
using System.Diagnostics;
using static System.Console;

namespace magic
{
    class Program
    {
        static ulong state0 = 1;
        static ulong state1 = 2;
        static int Random(int maxValue)
        {         
            ulong s1 = state0;
            ulong s0 = state1;
            state0 = s0;
            s1 ^= s1 << 23;
            s1 ^= s1 >> 17;
            s1 ^= s0;
            s1 ^= s0 >> 26;
            state1 = s1;
            return (int)((state0 + state1) % (ulong)maxValue);
        }

        static int ReadInt(string prompt, Func<int, bool> predicate)
        {
            var output = -1;
            Write(prompt);
            while (!int.TryParse(ReadLine(), out output) || !predicate(output))
                WriteLine("Not a valid value");

            return output;
        }

        static void Main(string[] args)
        {
            var maxValue = ReadInt("Enter a max greater than 0: ", x => x >= 1);
            var magicNumber = ReadInt("Enter a magic number in range: ", x => x >= 0 && x < maxValue);

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var guessCount = 1;

            while (Random(maxValue) != magicNumber)
                guessCount++;

            var seconds = (double)stopWatch.ElapsedMilliseconds / 1000;
            WriteLine($"Took {guessCount} guesses over {Math.Round(seconds, 5)} seconds");
            WriteLine($"That's {(guessCount / seconds) / 1000000}m/sec");
            ReadKey();
        }
    }
}
