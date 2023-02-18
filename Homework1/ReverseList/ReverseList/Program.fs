open System

printfn "%s" "Enter length of list:"
let length = Console.ReadLine() |> int
printfn "%s" "Enter list items in one line:"
let input = Console.ReadLine().Split(" ")
let list = List.init length (fun i -> input[i] |> int)

// Returns new list with items in reverse order
let reverseList list =
    let newList = []
    
    let rec recursiveReverseList list newList indexOfElement =
        if indexOfElement >= List.length list then newList
        else recursiveReverseList list (list.Item(indexOfElement) :: newList) (indexOfElement + 1)
    
    recursiveReverseList list newList 0

printfn $"%A{reverseList list}"