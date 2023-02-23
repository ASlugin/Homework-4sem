open System

// Returns new list with items in reverse order
let reverseList list =
    let rec fillNewList newList list =
        match list with
        | [] -> newList
        | head :: rest -> fillNewList (head :: newList) rest

    fillNewList [] list


// Creates list [2^n; 2^(n + 1); ...; 2^(n + m)]
let listOfDegrees n m =
    let initValue = 2.0 ** n |> int
    // List.init (m + 1) (fun i -> initValue * (2.0 ** i |> int))
    
    let rec makeList list m =
        if m = 0 then list
        else makeList (List.head list * 2 :: list) (m - 1)
    
    makeList [initValue] m |> reverseList
    
printf "%s" "Enter n: "
let nInput = Console.ReadLine() |> int
printf "%s" "Enter m: "
let mInput = Console.ReadLine() |> int

printfn $"%A{listOfDegrees nInput mInput}"