module StackTest

open ControlWork
open NUnit.Framework
open FsUnit

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
