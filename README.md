# magic

AMD® Ryzen 7 3700x 8-core processor × 16
16GB x 2 DDR4 @ 3200 Mhz
Pop!\_OS 20.04 LTS

Optimizations enabled everywhere.  
These numbers generally just go to show you that in most cases nothing matters besides not using Python.

| Language  | Variant        | Guesses (m/sec) | +/-  | Notes                          |
| --------- | -------------- | --------------- | ---- | ------------------------------ |
| TS (Node) | 14.13.0        | 132             | 2m   | I don't know how V8 does this  |
| Go        | 1.15.6         | 125             | 2m   |
| C++       | gcc -O3        | 124             | 5m   | Loss since switching off Intel |
| Java      | OpenJDK 11.0.9 | 123             | 1m   |
| C#        | core 3.1       | 120             | 10m  | Highest variance               |
| Rust      | 1.48.0         | 115             | 3m   |
| F#        | core 3.1       | 97              | 1m   | State-modifying RNG            |
| Haskell   | ghc 8.8.4      | 9.0             | 0.1m |
| Python    | 3              | 1.8             | -    | Benefits a lot from fast RAM   |
