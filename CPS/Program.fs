let l = [1L..1000000]

let rec badSum l = 
    match l with 
    | [] -> 0L
    | h::t -> h + (badSum t)

// let bad = badSum l
// printfn "%A" bad

let rec sum l cont =
    match l with
    | [] -> cont 0L
    | h::t -> sum t (fun x -> cont (h + x))

let res = sum l id
printfn "%A" res

(* QuickSort *)
let rec qs list cont = 
    match list with
    | [] -> cont([])
    | [a] -> cont([a])
    | head::tail ->
        let lessList = tail |> List.filter (fun i -> i <= head)
        let greaterList = tail |> List.filter (fun i -> i > head)
        qs lessList (fun lessPara -> 
            qs greaterList (fun greaterPara ->
                cont(lessPara@[head]@greaterPara)))

let list = [7;0;2;6;8;15;4;12]
qs list id |> printfn "%A"