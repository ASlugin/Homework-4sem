module LazyTest

open NUnit.Framework
open FsUnit
open Lazy.LazyFactory

[<TestFixture>]
type OneThreadLazyTests() =
    let supplier = fun () -> 10 * 4 + 2
    let simpleLazy = LazyFactory.CreateSimpleLazy<int>(supplier)
    let threadSafeLazy = LazyFactory.CreateThreadSafeLazy<int>(supplier)
    let lockFreeLazy = LazyFactory.CreateLockFreeLazy<int>(supplier)

    [<Test>]
    member this.``Get returns the same value several times``() =
        let value1 = (simpleLazy.Get(), threadSafeLazy.Get(), lockFreeLazy.Get())
        let value2 = (simpleLazy.Get(), threadSafeLazy.Get(), lockFreeLazy.Get())
        value1 |> should equal (42, 42, 42)
        value1 |> should equal value2

    [<Test>]
    member this.``Supplier function is called only once``() =
        let mutable callCount = 0
        let supplier = fun () ->
            callCount <- callCount + 1
            7 + 100

        let simpleLazy = LazyFactory.CreateSimpleLazy<int>(supplier)
        let threadSafeLazy = LazyFactory.CreateThreadSafeLazy<int>(supplier)
        let lockFreeLazy = LazyFactory.CreateLockFreeLazy<int>(supplier)
        
        (simpleLazy.Get(), threadSafeLazy.Get(), lockFreeLazy.Get()) |> should equal (107, 107, 107)
        (simpleLazy.Get(), threadSafeLazy.Get(), lockFreeLazy.Get()) |> should equal (107, 107, 107)
        callCount |> should equal 3