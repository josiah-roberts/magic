// Learn more about F# at http://fsharp.org

open System
open System.Diagnostics

let mutable state0: uint64 = uint64(1);
let mutable state1: uint64 = uint64(2);
let random max =
    let mutable s1 = state0
    let mutable s0 = state1
    state0 <- s0
    s1 <- s1 ^^^ (s1 <<< 23)
    s1 <- s1 ^^^ (s1 >>> 17)
    s1 <- s1 ^^^ s0
    s1 <- s1 ^^^ (s0 >>> 26)
    state1 <- s1
    int((state0 + state1) % uint64(max))

let inline (>->) a b x =
    match a x with
    | Some res -> b res
    | None -> None        

let (|ParseInt|_|) (str:string) =
  match System.Int32.TryParse(str) with
    | (true,int) ->Some int
    | _ -> None

let tryParse str =
   match str with
   | ParseInt int -> Some int
   | _ -> None

let applyValidator validator arg =
    match validator arg with
    | true -> Some arg
    | false -> None

let rec readInt prompt validator =
    printfn "%s" prompt
    let input = Console.ReadLine() |> (tryParse >-> applyValidator validator)
    match input with
    | Some i -> i
    | None -> readInt prompt validator
    
type Timing<'a> = { ElapsedMilliseconds:int64; Result:'a }
let time f x =
    let stopwatch = Stopwatch.StartNew()
    let result = f x
    stopwatch.Stop()
    { 
        ElapsedMilliseconds = stopwatch.ElapsedMilliseconds
        Result = result
    }
    
let rec guessInternal max magic i =
    let nextCount = i + 1
    match random max with
    | guess when guess = magic -> nextCount
    | _ -> guessInternal max magic nextCount

let guess max magic =
    guessInternal max magic 0

[<EntryPoint>]
let main _ =
    let max = readInt "Enter a max greater than 0: " (fun x -> x > 0)
    let magic = readInt "Enter a magic number in range: " (fun x -> x >= 0 && x <= max)
    let result = time (guess max) magic
    printfn "Took %i guesses over %f seconds" result.Result (float(result.ElapsedMilliseconds) / float(1000)) 
    printfn "That's %f m/sec" (float(result.Result) / (float(result.ElapsedMilliseconds) / float(1000)) / float(1000000))
    0
