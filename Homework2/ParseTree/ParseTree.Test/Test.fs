module ParseTree.Test

open CalculatingOfExpression
open NUnit.Framework
open FsUnit


[<Test>]
let calculateShallThrowExceptionIfDividingByZero () =
    (fun () -> calculate (Expression.Division(Expression.Number(7), Expression.Number(0))) |> ignore)
    |> should throw typeof<System.DivideByZeroException>
    
    
let caseOfExpression =
    [
      (Expression.Number(3), 3)
      (Expression.Addition(Expression.Number(5), Expression.Number(2)), 7)
      (Expression.Subtraction(Expression.Number(17), Expression.Number(27)), -10)
      (Expression.Multiplication(Expression.Number(97), Expression.Number(2)), 194)
      (Expression.Division(Expression.Number(28), Expression.Number(3)), 9)
      (Expression.Addition(
        Expression.Subtraction(Expression.Number(173), Expression.Number(27)),
        Expression.Multiplication(Expression.Number(67), Expression.Number(-3))
        ), -55
      )
      
    ] |> List.map (fun (expression, result) -> TestCaseData(expression, result))

[<TestCaseSource("caseOfExpression")>]
let calculateShallWorkCorrectly expression result =
    1 |> should equal 1