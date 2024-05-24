// For more information see https://aka.ms/fsharp-console-apps

let print = printfn "check here: %s %d"
print "print type is (string -> int -> unit)" 0

let ``inc 1`` = (+) 1
50 |> ``inc 1`` |> printfn "%d"

let distance (x0, y0) (x1, y1) = (x0-x1)**2. + (y0-y1)**2. |> sqrt
let p = 3., 5.
let original = 0., 0.
p |> distance original |> printfn "%f"


// tuple, record, class
let dog0 = ("Tao", "German shepherd", "lucky", 3)

let dog = {| Onwer="Tao"; Breed="German shepherd"; DogName="Lucky"; Age=3 |}

type Dog() =
    member val Onwer = "Tao" with get
    member val Breed = "German shepherd" with get
    member val DogName = "Lucky" with get
    member val Age = 3 with get, set
let dog1 = Dog()