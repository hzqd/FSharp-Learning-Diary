// https://www.codewars.com/kata/57a37f3cbb99449513000cd8/train/fsharp
// Get number from string

type System.String with     
    member s.LazySplit(predicate: char -> bool) =    
        let rec loop p =  
            seq {    
                if p < s.Length then  
                    let n = s |> Seq.skip p |> Seq.takeWhile(fun c -> not (predicate c)) |> Seq.length
                    yield s.Substring(p, n)  
                    yield! loop (p + n + 1) 
            }    
        loop 0 

let getNumberFromString (s:string) =
    s.LazySplit(fun c -> not (c >= '0' && c <= '9')) |> Seq.fold (+) "" |> System.Int32.Parse

"hell5o wor6ld" |> getNumberFromString |> printfn "%d"