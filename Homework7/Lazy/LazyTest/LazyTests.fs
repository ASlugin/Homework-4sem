module LazyTest

open System.Threading
open NUnit.Framework
open FsUnit
open Lazy.LazyFactory
open Lazy.ILazy

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
        
        
[<TestFixture>]
type MultiThreadLazyTests() =
    let runTest (lazyObj : ILazy<obj>) (manualResetEvent : ManualResetEvent) =
        let tasks = Seq.init 100 (fun _ -> async { return lazyObj.Get() })
        
        manualResetEvent.Reset() |> ignore
        let asyncTask = tasks |> Async.Parallel |> Async.StartAsTask
        Thread.Sleep(500)
        manualResetEvent.Set() |> ignore
        
        let resultValues = asyncTask.Result
        let firstValue = Seq.item 0 resultValues

        resultValues |> Seq.forall (fun value -> obj.ReferenceEquals(value, firstValue)) |> should be True

    [<Test>]
    member this.ThreadSafeLazyTest () =
        let manualResetEvent = new ManualResetEvent(false)
        let counter = ref 0
        let supplier () =
            manualResetEvent.WaitOne() |> ignore
            Interlocked.Increment counter |> ignore
            obj()
    
        let lazyObject = LazyFactory.CreateThreadSafeLazy<obj>(supplier)
    
        runTest lazyObject manualResetEvent
        counter.Value |> should equal 1

    [<Test>]
    member this.LockFreeTest () =
        let manualResetEvent = new ManualResetEvent(false)
        let supplier () =
            manualResetEvent.WaitOne() |> ignore
            obj()

        let lazyObject = LazyFactory.CreateLockFreeLazy<obj>(supplier)
        
        runTest lazyObject manualResetEvent