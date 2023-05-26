module Lazy.SimpleLazy

open Lazy.ILazy

type SimpleLazy<'T>(supplier: unit -> 'T) =
    let mutable instance = None

    interface ILazy<'T> with
        member this.Get () =
            if instance.IsNone then
                instance <- (Some (supplier ()))
            instance.Value