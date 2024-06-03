// For more information see https://aka.ms/fsharp-console-apps

open System

// Test fn to dbg
let println x = printfn $"{x}"

// Pipe
let pipe f = f

// Tap
let tap f x = 
    f x
    x

// Ext fn `LazySplit` for String type
type System.String with     
    member s.LazySplit(predicate: char -> bool) =    
        let rec loop p =  
            seq {    
                if p < s.Length then  
                    let n = s |> Seq.skip p |> Seq.takeWhile(fun c -> not (predicate c)) |> Seq.length
                    if n > 0 then // Avoid yield empty string
                        yield s.Substring(p, n)    
                    yield! loop (p + n + 1) 
            }    
        loop 0  

let tryParseInt (x:string) =
    match Int32.TryParse x with
    | (true, x) -> Some x
    | _ -> None

let tryParseFloat (x:string) = 
    match Double.TryParse x with
    | (true, x) -> Some x
    | _ -> None

let check (|Int|_|) (|Float|_|) value =
    match value with
    | Int i -> float i
    | Float f -> f
    | s -> failwith $"Parse '{s}' fail"

let convert x =
    check tryParseInt tryParseFloat x

// Sum calc
while true do
    Console.ReadLine()
    |> tap(fun s -> if s.Length = 0 then Environment.Exit 0)
    |> pipe(_.LazySplit(fun c -> not (c >= '0' && c <= '9') && not (c = '.') && not(c = '-')))
    |> Seq.map convert
    |> Seq.sum
    |> println