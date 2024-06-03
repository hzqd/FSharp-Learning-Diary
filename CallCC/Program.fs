type Cont<'r, 'a> = Cont of (('a -> 'r) -> 'r)

let ret x = 
    Cont <| fun k -> k x

let bind f (Cont a) =
    Cont <| fun k -> a <| fun a' -> let (Cont x) = f a' in x k

let callcc f =
    Cont <| fun k -> let (Cont x) = f (fun a -> Cont <| fun _ -> k a) in x k

let runCont (Cont a: Cont<'a, 'a>) =
    a id

type ContBuilder () =
    member _.Bind (x, f) = bind f x 
    member _.Return x = ret x 
    member _.ReturnFrom x = x

(* Test *)
// Define the ContBuilder instance
let cont = ContBuilder()

// Basic test: simple return
let test1 () =
    let computation = 
        cont {
            return 42
        }
    let result = runCont computation
    assert (result = 42)

// Test callcc: early escape
let test2 () =
    let computation = 
        cont {
            let! x = callcc <| fun k -> 
                cont { return! k 99 } // Immediately escape with value 99
            return x
        }
    let result = runCont computation
    assert (result = 99)

// Test callcc with inner computation
let test3 () =
    let computation =
        cont {
            let! x = callcc <| fun k ->
                cont {
                    let! y = k 10
                    return y + 1 // This should not be reached
                }
            return x + 32 // This should be reached
        }
    let result = runCont computation
    assert (result = 42)

// Test callcc: using continuation later
let test4 () =
    let computation =
        cont {
            let! y = callcc <| fun k ->        // k is the stored continuation for later use
                cont {
                    let! x = cont { return 5 } // Normal computation path
                    return! k (x * 2)          // Use the stored continuation with a new value
                }
            return y + 2
        }
    let result = runCont computation
    assert (result = 12)

// Test callcc: nested callcc
let test5 () =
    let computation =
        cont {
            let! x = callcc <| fun k1 ->
                cont {
                    let! y = callcc <| fun k2 ->
                        cont {
                            return! k1 20
                        }
                    return y + 1 // This should not be reached
                }
            return x + 22 // This should be reached
        }
    let result = runCont computation
    assert (result = 42)

// All tests
let runTests () =
    test1 ()
    test2 ()
    test3 ()
    test4 ()
    test5 ()
    printfn "All tests passed!"

// Run tests
runTests ()