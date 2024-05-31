// For more information see https://aka.ms/fsharp-console-apps
open System
open System.IO

let pipe f = f

let tap f x = 
    f x
    x

let dbg a = a |> tap(fun x -> printfn "%A" x)
 
let lazySplit (predicate: char -> bool) (s:string) =    
    let rec loop p =  
        seq {    
            if p < s.Length then  
                let n = s |> Seq.skip p |> Seq.takeWhile(fun c -> not (predicate c)) |> Seq.length
                yield s.Substring(p, n)    
                yield! loop (p + n + 1) 
        }    
    loop 0  

"data.csv" |> File.ReadAllText |> lazySplit(fun c -> c = '\010')                                // |> dbg

|> Seq.map(fun s -> s |> lazySplit(fun c -> c = ',') |> Seq.toArray |> pipe(fun sa -> (sa, 1))) // |> dbg

|> Seq.groupBy(fun (sa, _) -> sa[1]) |> Seq.sortBy(fun (s, _) -> s)                             // |> dbg

|> Seq.map(fun (name, it) -> it |> Seq.fold(fun (num, age) (sa, mark)  -> (num + mark), age + (sa[2] |> Int32.Parse)) (0,0) |> pipe(fun (n, sum) -> sprintf $"({name}, {sum/n})"))

|> String.concat("\n") |> pipe(fun txt -> File.WriteAllText("result.txt", txt))
