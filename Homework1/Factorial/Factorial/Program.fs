open System

let input = Console.ReadLine()
let intValue = input |> int

// Counts factorial of the number
let rec factorial number =
    if number = 0 then 1
    elif number = 1 then 1
    else number * factorial (number - 1)
    
printfn $"%d{factorial intValue}"
