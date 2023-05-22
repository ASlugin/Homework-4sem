module BracketSequenceTest

open NUnit.Framework
open BracketSequence
open FsUnit

[<Test>]
let testCorrectBracketSequence () =
    let result = checkBrackets "({[[(){}]]}(){ })"
    result |> should be True

[<Test>]
let testIncorrectBracketSequence () =
    let result = checkBrackets "({[})"
    result |> should be False

[<Test>]
let testEmptyString () =
    let result = checkBrackets ""
    result |> should be True

[<Test>]
let testOnlyOpeningBrackets () =
    let result = checkBrackets "( {[ "
    result |> should be False

[<Test>]
let testOnlyClosingBrackets () =
    let result = checkBrackets " ) }]"
    result |> should be False

[<Test>]
let testCorrectSequenceWithOtherSymbols () =
    let result = checkBrackets "((a + b) * (c - d) / {e ^ [f + g]})"
    result |> should be True
    
[<Test>]
let testIncorrectSequenceWithOtherSymbols () =
    let result = checkBrackets "({a + b}*[c - d] / {e ^ [f + g)}})"
    result |> should be False
