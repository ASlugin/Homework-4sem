module ControlWork

// Task 1
let superMap list func =
    let rec loop list acc =
        match list with
        | [] -> List.rev acc
        | head::tail -> loop tail (acc |> List.append (head |> func |> List.rev))
    loop list []

// Task 2
let printDiamond n =
    let maxWidth = n * 2 - 1 
    
    let printLineWithStars amountOfStars =
        let printSymbols symbol n =
            let rec print symbol n acc =
                if n > 0 then
                    printf symbol
                    if acc < n then
                        print symbol n (acc + 1)
            print symbol n 1
            
        printSymbols " " ((maxWidth - amountOfStars) / 2)
        printSymbols "*" amountOfStars
        printSymbols " " ((maxWidth - amountOfStars) / 2)
        printfn ""
    
    let rec printUpPart amountOfStartInLine =
        printLineWithStars amountOfStartInLine
        if amountOfStartInLine < maxWidth then
            printUpPart (amountOfStartInLine + 2)
    
    let rec printDownPart amountOfStartInLine =
        printLineWithStars amountOfStartInLine
        if amountOfStartInLine > 0 then
            printDownPart (amountOfStartInLine - 2)
    
    printUpPart 1
    printDownPart (maxWidth - 2)

// Task 3
type ThreadSafeStack<'a>() =
    let mutable items : 'a list = []
    
    member this.Push(item : 'a) =
        lock items (fun () -> items <- item :: items)

    member this.TryPop() =
        lock items (fun () ->
            match items with
            | [] -> None
            | head :: tail ->
                items <- tail
                Some head
        )
