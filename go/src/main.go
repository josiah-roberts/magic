package main

import (
	"fmt"
	"time"
)

func rng() func(int) int {
	STATE0 := 1
	STATE1 := 2
	return func(max_value int) int {
		s1 := STATE0
		s0 := STATE1
		STATE0 = s0
		s1 ^= s1 << 23
		s1 ^= s1 >> 17
		s1 ^= s0
		s1 ^= s0 >> 26
		STATE1 = s1
		return (STATE0 + STATE1) % max_value
	}
}

func prompt(p string, f func(int) bool) int {
	fmt.Print(p)
	for {
		var i int
		_, err := fmt.Scan(&i)
		if err == nil && f(i) {
			return i
		}
	}
}

func main() {
	max := prompt("Enter a max greater than 0: ", func(i int) bool { return i > 0 })
	magic := prompt("Enter a magic number in range: ", func(i int) bool { return i >= 0 && i < max })

	start := time.Now()
	random := rng()
	count := 0
	for {
		if random(max) == magic {
			break
		}
		count++
	}

	seconds := time.Since(start).Seconds()
	fmt.Printf("Took %d guesses over %f seconds\n", count, seconds)
	fmt.Printf("That's %f m/sec", (float64(count)/seconds)/1000000)
}
