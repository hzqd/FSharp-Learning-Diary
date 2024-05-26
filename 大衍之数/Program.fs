open System

// 大衍之数五十
let 大衍之数 = 50

// 其用四十有九
let 用 = 大衍之数 - 1 // 1 为 不易
let 易 = Random()

// 揲之以四以象四时，归奇于扐以象闰
let 奇 x = match x % 4 with 0 -> 4 | n -> n

let 遍 手 = 
    let 余 = 用 - 手
    // 分而为二以象两
    let 左 = 易.Next(1, 余) // 在天成象
    // 挂一以象三
    let 右 = 余 - 左 - 1    // 地随天变；生人为 1，现有天地人三才
    // 揲四归奇；五岁再闰，故再扐而后挂
    手 + 1 + 奇 左 + 奇 右

let 轮 _ =
    // 三遍成爻
    let 手 = 遍 0
    let 手 = 遍 手
    let 手 = 遍 手

    match (用 - 手) / 4 with
    | 6 (* 老阴 *) -> ("阴", "阳")
    | 7 (* 少阳 *) -> ("阳", "阳")
    | 8 (* 少阴 *) -> ("阴", "阴")
    | 9 (* 老阳 *) -> ("阳", "阴")
    | n -> failwith $"internal calculate error: {n}"
    
let 卦 = seq {1..6} |> Seq.map (fun _ -> 轮()) |> Seq.toList
printfn "本:  变:"  
卦 |> Seq.rev |> Seq.iter(fun (爻, 变) -> printfn $"{爻}   {变}")

while Console.ReadLine().Length > 0 do
()