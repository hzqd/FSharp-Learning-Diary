let (|A|) x y z = x + y + z
let (A 1 (|B|)) = 10
let (B x) = 100
printfn "%d" x // output: 111

let matches (pat: string) (s: string) =
    let l = pat.Length
    if l = 0 then  
        Seq.empty  
    else
        let rec loop p =
            let p' = p + l
            seq {
                if p' <= s.Length then
                    if pat = s.[p..p' - 1] then 
                        yield s.Substring(p, l)
                        yield! loop p'
                    else
                        yield! loop (p + 1)
            }
        loop 0

"ssss" |> matches "ss" |> printfn "%A"