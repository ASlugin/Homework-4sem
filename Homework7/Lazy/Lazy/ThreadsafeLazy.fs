module Lazy.ThreadsafeLazy

open System
open Lazy.ILazy

type ThreadsafeLazy<'T>(supplier: unit -> 'T) =
    [<VolatileField>]
    let mutable instance = None
    let obj = Object ()

    interface ILazy<'T> with
        member this.Get () =
            if instance.IsNone then
                lock obj (fun () ->
                    if instance.IsNone then
                        instance <- Some (supplier ())
                    )

            instance.Value