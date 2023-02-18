open System

// Finds number in list and returns first index of the number in list
// If there is not number in list, returns -1
let findInList list number =
    let rec recursiveFind list number index =
        if index >= List.length list then -1
        elif number = list.Item(index) then index
        else recursiveFind list number (index + 1)
    
    recursiveFind list number 0

printf "%s" "Enter length of list: "
let length = Console.ReadLine() |> int
printf "%s" "Enter list items in one line: "
let input = Console.ReadLine().Split(" ")
let list = List.init length (fun i -> input[i] |> int)
printf "%s" "Enter number to find in list: "
let number = Console.ReadLine() |> int

printf $"Index of the number in list: %d{findInList list number}"