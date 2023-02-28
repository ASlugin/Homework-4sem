open System

// Finds number in list and returns first index of the number in list
// If there is not number in list, returns -1
let findInList list number =
    let rec find number list index =
        match list with
        | [] -> -1
        | head :: rest ->
            if head = number then index
            else find number rest (index + 1)
            
    find number list 0
    
printf "Enter length of list: "
let length = Console.ReadLine() |> int
printf "Enter list items in one line: "
let input = Console.ReadLine().Split(" ")
let list = List.init length (fun i -> input[i] |> int)
printf "Enter number to find in list: "
let number = Console.ReadLine() |> int

printf $"Index of the number in list: %d{findInList list number}"