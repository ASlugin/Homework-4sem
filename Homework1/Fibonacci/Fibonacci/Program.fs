open System

let input = Console.ReadLine()
let intValue = input |> int

// Counts Fibonacci number
let fibonacci number =
    let rec fibonacciCompute number current next =
        if number = 0 then current
        else fibonacciCompute (number - 1) next (current + next)
        
    fibonacciCompute number 0 1

printfn $"%d{fibonacci intValue}"
