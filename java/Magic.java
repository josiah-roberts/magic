import java.util.function.*;
import java.io.*;

public class Magic {
    static long state0 = 1;
    static long state1 = 2;
    
    static int random(int maxValue)
    {         
        long s1 = state0;
        long s0 = state1;
        state0 = s0;
        s1 ^= s1 << 23;
        s1 ^= s1 >> 17;
        s1 ^= s0;
        s1 ^= s0 >> 26;
        state1 = s1;
        return (int)((state0 + state1) % (long)maxValue);
    }

    static int ReadInt(String prompt, Predicate<Integer> predicate)
    {
        try
        {
            System.out.println(prompt);
            BufferedReader buffer = new BufferedReader(new InputStreamReader(System.in));
            String input = buffer.readLine();
            try {
                int intValue = Integer.parseInt(input);
                if (predicate.test(intValue)) {
                    return intValue;
                }
                else {
                    return ReadInt(prompt, predicate);
                }
            } catch (NumberFormatException e) {
                return ReadInt(prompt, predicate);
            }
        }
        catch (IOException e) {
            return ReadInt(prompt, predicate);
        }
    }

    public static void main(String[] args) {
        int maxValue = ReadInt("Enter a max greater than 0: ", x -> x >= 1);
        int magicNumber = ReadInt("Enter a magic number in range: ", x -> x >= 0 && x < maxValue);

        double oldMs = System.currentTimeMillis();

        int guessCount = 1;
        while (random(maxValue) != magicNumber)
            guessCount++;

        double seconds = (System.currentTimeMillis() - oldMs) / 1000;
        System.out.println(String.format("Took %s guesses over %s seconds", guessCount, Math.round(seconds * 100000d) / 100000d));
        System.out.println(String.format("That's %sm/sec", (guessCount / seconds) / 1000000));
    }
}