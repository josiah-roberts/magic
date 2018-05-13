import random
import time

def guess(target, max):    
    guessCount = 1
    while random.randint(0, max) != target:
        guessCount += 1
    return guessCount

maxValue = None
while maxValue == None or maxValue < 2:
    try:
        maxValue = int(input('Enter a max greater than 0: ')) + 1
    except ValueError:
        print('Not a number')

magicNumber = None
while magicNumber == None or magicNumber < 0 or magicNumber >= maxValue:
    try:
        magicNumber = int(input('Enter a magic number in range: '))
    except ValueError:
        print('Not a number')

start = time.time()
guessCount = guess(magicNumber, maxValue)
elapsed = time.time() - start
print('Took ' + str(guessCount) + ' guesses over ' + str(round(elapsed, 5)) + ' seconds')
print("That's " + str((guessCount / elapsed) / 1000000) + "m/sec")
exit(0)