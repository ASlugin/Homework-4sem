module LambdaInterpreter.Test

open LambdaInterpreter

open NUnit.Framework
open FsUnit

[<Test>]
let IdenticalTermTest () =
    let expression = Application(Abstraction("x", Variable "x"), Variable("y"))
    betaReduction expression |> should equal (Variable "y")
    
[<Test>]
let DivergentCombinatorTest () =
    let expression = Application(Abstraction("x", Application(Variable "x", Variable "x")), Abstraction("x", Application(Variable "x", Variable "x")))
    betaReduction expression |> should equal expression
    
[<Test>]
let SubstitutionWithChangingNameTest () =
    let expression = Application(Abstraction("x", Abstraction("y", Variable "x")), Variable "y")
    betaReduction expression |> should equal (Abstraction ("A", Variable "y"))
    
[<Test>]
let EvaluatingIsCorrectTest () =
    let expression = Application(Abstraction("x", Variable "x"), Abstraction("y", Abstraction("z", Variable "z")))
    betaReduction expression |> should equal (Abstraction ("y", Abstraction ("z", Variable "z")))
    

    
