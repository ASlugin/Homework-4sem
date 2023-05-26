module Builders

open System

type RounderBuilder (digits: int) =
    member this.Bind(x, func) = func x
    member this.Return(x: float) = Math.Round(x, digits)

type StringCalculatorBuilder () =
    member this.Bind(x: string, func) = 
        try 
            x |> int |> func
        with :? FormatException
            -> None
    member this.Return(x) = Some x