open System

let println x = printfn $"{x}"

let tap f x = 
    f x
    x

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

let cangjie_official_language_design_member_liu_jun_jie_implementation_based_on_inite_state_machine_translated_by_hzqd (text: string) =  
    let states = Array.init 3 (fun _ -> Array.create 128 -1)
    Array.blit [|1; 2; -1; 1; 1; 1; 1; 1; 1; 1; 1; 1; 1|] 0 states.[0] 45 13
    Array.blit [|-1; 2; -1; 1; 1; 1; 1; 1; 1; 1; 1; 1; 1|] 0 states.[1] 45 13
    Array.blit [|-1; -1; -1; 2; 2; 2; 2; 2; 2; 2; 2; 2; 2|] 0 states.[2] 45 13

    let mutable i = 0
    let mutable sum = 0.0
    while i < text.Length do
        let mutable s = 0
        while i < text.Length && states.[s].[int (text.[i])] = -1 do
            i <- i + 1
        let beginIndex = i
        while i < text.Length && states.[s].[int (text.[i])] <> -1 do
            s <- states.[s].[int (text.[i])]
            i <- i + 1
        let numStr = text.Substring(beginIndex, i - beginIndex)
        sum <- sum + convert numStr
    sum

// Sum calc
while true do
    Console.ReadLine()
    |> tap(fun s -> if s.Length = 0 then Environment.Exit 0)
    |> cangjie_official_language_design_member_liu_jun_jie_implementation_based_on_inite_state_machine_translated_by_hzqd
    |> println