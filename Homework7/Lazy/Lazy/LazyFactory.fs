module Lazy.LazyFactory

open Lazy.ILazy
open Lazy.SimpleLazy
open Lazy.ThreadsafeLazy
open Lazy.LockFreeLazy

type LazyFactory = 
    static member CreateSimpleLazy supplier = 
        new SimpleLazy<'T>(supplier) :> ILazy<'T>

    static member CreateConcurrentLazy supplier = 
        new ThreadsafeLazy<'T>(supplier) :> ILazy<'T>
        
    static member CreateLockFreeLazy supplier = 
        new LockFreeLazy<'T>(supplier) :> ILazy<'T>