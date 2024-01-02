module WorkflowTest

open NUnit.Framework
open FsUnit
open Builders

[<Test>]
let RoundBuilderTest () =
    let rounding x = RounderBuilder(x)
    let result = rounding 3 {
        let! a = 2.0 / 12.0
        let! b = 3.5
        return a / b
    }
    result |> should equal 0.048

[<Test>]
let ``RoundBuilder should throw exception if incorrect argument``() =
    let rounding x = RounderBuilder(x)
    (fun () ->
        rounding -6 {
            let! a = 2.0 / 12.0
            let! b = 3.5
            return a / b
        } |> ignore) |> should throw typeof<System.ArgumentOutOfRangeException>

[<Test>]
let StringCalculatorBuilderTest () =
    let calculate = StringCalculatorBuilder()
    let result = calculate {
        let! x = "5"
        let! y = "7"
        let z = x + y
        return z
    }
    result |> should equal (Some(12))
    
[<Test>]
let ``StringCalculatorBuilder should return none if string is not number`` () =
    let calculate = StringCalculatorBuilder()
    let result = calculate {
        let! x = "1"
        let! y = "q"
        let z = x + y
        return z
    }
    result |> should equal None