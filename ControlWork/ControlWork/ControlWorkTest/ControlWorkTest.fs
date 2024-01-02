module ControlWorkTest

open ControlWork
open NUnit.Framework
open FsUnit

[<Test>]
let ``superMap should apply function to every element and concatenate the results`` () =
    let list = [1.0; 2.0; 3.0]
    let f x = [sin x; cos x]
    superMap list f |> should equal [sin 1.0; cos 1.0; sin 2.0; cos 2.0; sin 3.0; cos 3.0]

[<Test>]
let ``superMap should handle empty list`` () =
    let list = []
    let func x = [sin x; cos x]
    superMap list func |> List.isEmpty |> should be True

[<Test>]
let ``superMap should handle function that returns an empty list`` () =
    let list = [1; 2; 3]
    let func x = []
    superMap list func |> should equal []

[<Test>]
let ``superMap should handle function that returns a list with one element`` () =
    let list = [1; 2; 3]
    let func x = [x]
    superMap list func |> should equal [1; 2; 3]

[<Test>]
let ``superMap work correctly`` () =
    let list = [1.0; 2.0; 3.0]
    let func x = [x + 1.; x + 2.]
    superMap list func |> should equal [2.0; 3.0; 3.0; 4.0; 4.0; 5.0;]

[<Test>]
let ``Push returns elements in right order`` () =
    let stack = ThreadSafeStack<int>()
    stack.Push(2)
    stack.Push(5)

    stack.TryPop() |> should equal (Some 5)
    stack.TryPop() |> should equal (Some 2)

[<Test>]
let ``TryPop returns None if stack is empty`` () =
    let stack = ThreadSafeStack<float>()
    stack.TryPop() |> should equal None
    