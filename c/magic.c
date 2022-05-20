#include <stdio.h>
#include <stdlib.h>
#include <time.h>

int atoiStdin(void) {
    int c, ret = 0;

    system("stty raw"); 

    while ((c = getchar()) != EOF) {
        if (c >= '0' && c <= '9') {
            ret = (ret * 10) + (c - '0');
        } else if (c == '\n' || c == '\r') {
            system("stty sane"); 
            puts("\b\b  ");
            return ret;
        } else {
            putchar('\b');
        }
    }
}

int main()
{
    int max, magic;

    do {
        printf("Enter a max greater than %d: \n", 0);
    } while ((max = atoiStdin()) <= 0);

    do {
        printf("Enter a number between 0 and %d\n", max - 1);
    } while ((magic = atoiStdin()) <= 0 || magic >= max);

    printf("%d %d\n", max, magic);

    clock_t before = clock();

    int guessCount;
    while (rand() % max != magic)
        guessCount++;

    clock_t after = clock();
    double seconds = (double)(after - before) / CLOCKS_PER_SEC;

    printf("Took %d guesses over %f seconds\n", guessCount, seconds);
    printf("That's %f M/sec\n", (guessCount / seconds) / 1000000);
}