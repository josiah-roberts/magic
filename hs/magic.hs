import Control.Exception
import Control.Monad
import Data.Bits
import Data.List (intersperse)
import Data.Word
import System.CPUTime (getCPUTime)
import System.Environment
import Text.Printf
import Text.Read (readMaybe)

rng :: Word64 -> Word64 -> Int -> (Int, (Word64, Word64))
rng state0 state1 max =
  let s1' = state0 `xor` (state0 `shiftL` 23)
   in (mod (fromIntegral (state0 + state1)) max, (state1, s1' `xor` state1 `xor` (s1' `shiftR` 17) `xor` (state1 `shiftR` 26)))

newtype RecursiveRng = RecursiveRng {runRng :: Int -> (Int, RecursiveRng)}

rngRec :: Word64 -> Word64 -> Int -> (Int, RecursiveRng)
rngRec state0 state1 max =
  let (v, (state0', state1')) = rng state0 state1 max
   in (v, RecursiveRng (rngRec state0' state1'))

rngPub = rngRec 1 2

validate :: (Int -> Bool) -> Maybe Int -> Maybe Int
validate validator value =
  case value of
    Just x ->
      if validator x
        then return x
        else Nothing
    Nothing -> Nothing

readInt :: String -> (Int -> Bool) -> IO Int
readInt prompt validator = do
  putStr prompt
  lin <- getLine
  case (validate validator . readMaybe) lin of
    Just x -> return x
    Nothing -> putStrLn "Invalid input" >> readInt prompt validator

(&&.) :: (t -> Bool) -> (t -> Bool) -> t -> Bool
(&&.) p p' v = p v && p' v

guess :: Int -> Int -> Int -> RecursiveRng -> Int
guess max magic count next =
  let (i, next') = runRng next max
   in if i == magic
        then count
        else guess max magic (count + 1) next'

guessPub max magic =
  guess max magic 1 (RecursiveRng rngPub)

time :: Int -> IO Int
time y = do
  start <- getCPUTime
  let ret = y
  (printf . show) ret -- Laziness trap
  end <- getCPUTime
  let diff = fromIntegral (end - start) / (10 ^ 12)
  printf "\nComputation time: %0.9f sec\n" (diff :: Double)
  printf "%0.2f guesses/sec\n" (fromIntegral ret / (diff :: Double))
  return ret

main = do
  max' <- readInt "Lul gimmie that max bro: " (> 0)
  magic' <- readInt "Lawlz gimme that magic bro:" ((> 0) &&. (<= max'))
  time (guessPub max' magic')
