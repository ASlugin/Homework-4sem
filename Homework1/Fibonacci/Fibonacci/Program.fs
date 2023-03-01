open System

let input = Console.ReadLine()
let intValue = input |> int

// Counts Fibonacci number; if number is negative, throws ArgumentException
let fibonacci number =
    if number < 0 then raise(ArgumentException("Number cannot be negative"))
    
    let rec fibonacciCompute number current next =
        if number = 0 then current
        else fibonacciCompute (number - 1) next (current + next)
        
    fibonacciCompute number 0 1

printfn $"%d{fibonacci intValue}"