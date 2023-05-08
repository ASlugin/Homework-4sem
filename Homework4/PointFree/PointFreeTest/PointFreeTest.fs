module PointFreeTest

open NUnit.Framework
open PointFree
open FsCheck

[<Test>]
let initialAndResultFunctionsAreEqual () =
    let check (x:int) (l:list<int>) = resultFunc x l = func x l
    Check.QuickThrowOnFailure check
