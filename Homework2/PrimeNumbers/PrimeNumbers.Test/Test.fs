module PrimeNumbers.Test

open PrimeNumbers
open NUnit.Framework
open FsUnit

[<Test>]
let sequenceOfPrimeNumbersShallBeCorrect () =
    let sequence = getSequence () |> Seq.take 7 |> List.ofSeq
    let result = [2; 3; 5; 7; 11; 13; 17]
    sequence |> should equal result