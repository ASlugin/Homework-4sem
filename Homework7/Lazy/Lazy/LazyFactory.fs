module Lazy.LazyFactory

open Lazy.ILazy
open Lazy.SimpleLazy
open Lazy.ThreadSafeLazy
open Lazy.LockFreeLazy

type LazyFactory = 
    static member CreateSimpleLazy supplier = 
        new SimpleLazy<'T>(supplier) :> ILazy<'T>

    static member CreateThreadSafeLazy supplier = 
        new ThreadSafeLazy<'T>(supplier) :> ILazy<'T>
        
    static member CreateLockFreeLazy supplier = 
        new LockFreeLazy<'T>(supplier) :> ILazy<'T>