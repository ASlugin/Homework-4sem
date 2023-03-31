namespace EvenNumbers.Test

module TestsOfEvenNumbersCounters =
    open EvenNumbers.CountEvenNumbers
    open NUnit.Framework
    open FsUnit
    open FsCheck

    let functionsAreEqual list = countEvenNumbersWithMap list = countEvenNumbersWithFilter list &&
                                 countEvenNumbersWithFilter list = countEvenNumbersWithFold list
    Check.QuickThrowOnFailure functionsAreEqual
    
    let caseDataForCounters =
        [
            ([-1; 27; 8; -6], 2)
            ([], 0)
            ([-1; -4; -9999; -3], 1)
            ([0; 12; 78; 46; 14], 5)
        ] |> List.map (fun (list, correctResult) -> TestCaseData(list, correctResult))
        
    [<TestCaseSource("caseDataForCounters")>]
    let counterWithMapWorkCorrectly list result =
        countEvenNumbersWithMap list |> should equal result
        
    [<TestCaseSource("caseDataForCounters")>]
    let counterWithFilterWorkCorrectly list result =
        countEvenNumbersWithFilter list |> should equal result
        
    [<TestCaseSource("caseDataForCounters")>]
    let counterWithFoldWorkCorrectly list result =
        countEvenNumbersWithFold list |> should equal result
