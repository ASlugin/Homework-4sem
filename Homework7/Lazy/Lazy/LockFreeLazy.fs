module Lazy.LockFreeLazy

open System.Threading
open Lazy.ILazy

type LockFreeLazy<'T>(supplier: unit -> 'T) =
    let mutable instance = None

    interface ILazy<'T> with
        member this.Get () =
            if instance.IsNone then
                let res = Some (supplier ())
                Interlocked.CompareExchange(&instance, res, None) |> ignore
                
            instance.Value