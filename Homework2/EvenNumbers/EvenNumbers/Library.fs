namespace EvenNumbers

module CountEvenNumbers =
    let countEvenNumbersWithMap list = list |> List.map (fun x -> (abs(x) + 1) % 2) |> List.sum
    let countEvenNumbersWithFilter list = list |> List.filter (fun x -> x % 2 = 0) |> List.length
    let countEvenNumbersWithFold list = list |> List.fold (fun acc element -> acc + (abs(element) + 1) % 2) 0
