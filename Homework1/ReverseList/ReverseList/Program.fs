open System

printf "%s" "Enter length of list: "
let length = Console.ReadLine() |> int
printf "%s" "Enter list items in one line: "
let input = Console.ReadLine().Split(" ")
let list = List.init length (fun i -> input[i] |> int)

// Returns new list with items in reverse order
let reverseList list =
    let rec fillNewList newList list =
        match list with
        | [] -> newList
        | head :: rest -> fillNewList (head :: newList) rest
    
    fillNewList [] list
    
printfn $"%A{reverseList list}"