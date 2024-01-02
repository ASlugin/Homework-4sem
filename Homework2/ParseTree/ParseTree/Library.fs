module CalculatingOfExpression

type Expression =
    | Number of int
    | Addition of Expression * Expression
    | Subtraction of Expression * Expression
    | Multiplication of Expression * Expression
    | Division of Expression * Expression
    
let rec calculate expression =
    match expression with
    | Number x -> x
    | Addition(left, right) -> (calculate left) + (calculate right)
    | Subtraction(left, right) -> (calculate left) - (calculate right)
    | Multiplication(left, right) -> (calculate left) * (calculate right)
    | Division(left, right) -> (calculate left) / (calculate right)
