import { createInterface } from 'readline';

const rl = createInterface({
    input: process.stdin,
    output: process.stdout
});

async function getNumber(prompt: string, validator: (value: number) => Boolean) : Promise<number> {
    return await new Promise<number>(res =>
        rl.question(prompt, answer => {
            const number = Number.parseInt(answer);
            if (number == NaN || !validator(number))
                getNumber(prompt, validator).then(result => res(result));
            else
                res(number);
        })
    );
}

let randomInt = (max: number) =>
    Math.floor(Math.random() * (max + 1));

let guess = (max: number, magic: number) => {
    let guess = 1;
    while (randomInt(max) != magic)
        guess++;
    
    return guess;
}

async function main()
{
    const max = await getNumber("Enter a max greater than 0: ", x => x >= 1);
    const magic = await getNumber("Enter a magic number in range: ", x => x >= 0 && x <= max);

    const start = Date.now();
    const guessCount = guess(max, magic);
    const ms = Date.now() - start;

    console.log(`Took ${guessCount} guesses over ${ms / 1000} seconds`);
    console.log(`That's ${(guessCount / (ms / 1000)) / 1000000}m/sec`);
}

main().then(() => rl.close()).catch(() => rl.close());