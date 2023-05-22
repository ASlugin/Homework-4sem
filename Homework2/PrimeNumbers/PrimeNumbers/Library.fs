module PrimeNumbers 

let numberIsPrime number =
    seq {2 .. int(sqrt(float number))} |> Seq.forall (fun x -> number % x <> 0)

let getSequence () = Seq.initInfinite(fun x -> x + 2) |> Seq.filter (numberIsPrime)