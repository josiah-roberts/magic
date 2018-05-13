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

        static void Main(string[] args)
        {
            var maxValue = -1;
            Write("Enter a max greater than 0: ");
            while (!int.TryParse(ReadLine(), out maxValue) || maxValue < 1)
                WriteLine("Not a number");

            maxValue++;

            var magicNumber = -1;
            Write("Enter a magic number in range: ");
            while (!int.TryParse(ReadLine(), out magicNumber) || magicNumber < 0 || magicNumber >= maxValue)
                WriteLine("Not a number");

            var random = new Random();
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var guessCount = 0;

            while (Random(maxValue) != magicNumber)
                guessCount++;

            var seconds = (double)stopWatch.ElapsedMilliseconds / 1000;
            WriteLine($"Took {guessCount} guesses over {Math.Round(seconds, 5)} seconds");
            WriteLine($"That's {(guessCount / seconds) / 1000000}m/sec");
            ReadKey();
        }
    }
}
