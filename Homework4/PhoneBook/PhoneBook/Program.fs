module Interface

open System
open PhoneBook

let printHelp() =
    printfn "Enter one of the next command:"
    printfn "-1 - exit"
    printfn "0 - help"
    printfn "1 - add a new entry"
    printfn "2 - find phone number by name"
    printfn "3 - find name by phone number"
    printfn "4 - print all entries"
    printfn "5 - save data to file"
    printfn "6 - read data from file"

let start =
    let rec loop database =
        let command = Console.ReadLine()
        match command with
        | "-1" -> ()
        | "0" ->
            printHelp()
            loop database
        | "1" ->
            printf "Enter the name: "
            let name = Console.ReadLine()
            printf "Enter the number: "
            let number = Console.ReadLine()
            
            let newDatabase, itIsNewNumber = addNewEntry database name number
            if not itIsNewNumber then
                printfn "Such number already exist in phone book"
            else
                printfn "Phone number added"
            loop newDatabase
        | "2" ->
            printf "Enter the name: "
            let name = Console.ReadLine()
            let numberOption = findNumberByName database name
            match numberOption with
            | Some number -> printfn $"{number}"
            | None -> printfn $"There is no entry with name \"{name}\""
            loop database
        | "3" ->
            printf "Enter the number: "
            let number = Console.ReadLine()
            let nameOption = findNameByNumber database number
            match nameOption with
            | Some name -> printfn $"{name}"
            | None -> printfn $"There is no entry with number \"{number}\""
            loop database
        | "4" ->
            printfn "All entries:"
            database |> List.iter (fun (phone:Phone) -> printfn $"{phone.Name} {phone.Number}")
            loop database
        | "5" ->
            saveDataToFile database
            printfn "Data saved to file"
            loop database
        | "6" ->
            let newDatabase = readDataFromFile database
            printfn "Data read from file"
            loop newDatabase
        | _ ->
            printfn "Unknown command"
            loop database

    printHelp()
    loop []

start