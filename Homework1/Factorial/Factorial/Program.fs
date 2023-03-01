open System

let input = Console.ReadLine()
let intValue = input |> int

// Counts factorial of the number; if number is negative, throws ArgumentException
let factorial number =
    if number < 0 then raise(ArgumentException("Number cannot be negative"))
    
    let rec countFactorial number multiplier =
        if number = 0 then 1
        elif number = 1 then multiplier
        else countFactorial (number - 1) (number * multiplier)
        
    countFactorial number 1
    
printfn $"%d{factorial intValue}"
