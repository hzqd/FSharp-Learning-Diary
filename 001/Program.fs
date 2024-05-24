// For more information see https://aka.ms/fsharp-console-apps

[<Measure>]type 元
[<Measure>]type 小孩
[<Measure>]type 大人

let 儿童票 = 3<元/小孩>
let 成人票 = 5<元/大人>

let familyCost(child: int<小孩>) (adult: int<大人>) = 
    child * 儿童票 + adult * 成人票

familyCost 2<小孩> 2<大人> |> printfn "total cost = %d"