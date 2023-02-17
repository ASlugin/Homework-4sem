open System

let input = Console.ReadLine()
let intValue = input |> int

let rec factorial number =
    if number = 0 then 1
    elif number = 1 then 1
    else number * factorial (number - 1)
   
let result = factorial intValue
printfn "%d" result
