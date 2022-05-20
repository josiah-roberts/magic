#include <stdio.h>
#include <stdlib.h>
#include <time.h>

int atoiStdin(void)
{
    int c, ret = 0;

    system("stty raw");

    while ((c = getchar()) != EOF)
    {
        if (c >= '0' && c <= '9')
        {
            ret = (ret * 10) + (c - '0');
        }
        else if (c == '\n' || c == '\r')
        {
            system("stty sane");
            puts("\b\b  ");
            return ret;
        }
        else
        {
            putchar('\b');
        }
    }
}

struct rand_data
{
    unsigned long int state0;
    unsigned long int state1;
};

unsigned long int xorshift128plus(struct rand_data *data)
{
    unsigned long int s1 = data->state0;
    unsigned long int s0 = data->state1;
    data->state0 = s0;
    s1 ^= s1 << 23;
    s1 ^= s1 >> 17;
    s1 ^= s0;
    s1 ^= s0 >> 26;
    data->state1 = s1;
    return s0 + s1;
}

int main()
{
    int max, magic;

    do
    {
        printf("Enter a max greater than %d: \n", 0);
    } while ((max = atoiStdin()) <= 0);

    do
    {
        printf("Enter a number between 0 and %d\n", max - 1);
    } while ((magic = atoiStdin()) <= 0 || magic >= max);

    printf("%d %d\n", max, magic);

    clock_t before = clock();

    struct rand_data data;
    data.state0 = 1;
    data.state1 = 2;

    int guessCount;
    while (xorshift128plus(&data) % max != magic)
        guessCount++;

    clock_t after = clock();
    double seconds = (double)(after - before) / CLOCKS_PER_SEC;

    printf("Took %d guesses over %f seconds\n", guessCount, seconds);
    printf("That's %f M/sec\n", (guessCount / seconds) / 1000000);
}