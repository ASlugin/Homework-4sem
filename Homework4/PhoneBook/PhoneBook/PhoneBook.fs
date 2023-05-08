module PhoneBook

open System.IO

type Phone = {Name: string; Number: string;}

// Returns new database and true if it was new number, else false 
let addNewEntry (database : list<Phone>) name number =
    let numberIsExist = database |> List.exists (fun entry -> entry.Number = number)
    if not numberIsExist then
        let newEntry = { Name = name; Number = number }
        newEntry::database, true
    else
        database, false

// Returns option with number
let findNumberByName (database : list<Phone>) name =
    let numberOption = database |> List.tryFind (fun entry -> entry.Name = name)
    match numberOption with
    | Some value -> Some value.Number
    | None -> None
    
// Returns option with name
let findNameByNumber (database : list<Phone>) number =
    let nameOption = database |> List.tryFind (fun entry -> entry.Number = number)
    match nameOption with
    | Some value -> Some value.Name
    | None -> None

let saveDataToFile (database : list<Phone>) (path : string) = 
    use stream = new StreamWriter(path)
    database |> List.iter (fun entry -> stream.WriteLine($"{entry.Name} {entry.Number}"))
    ()

let readDataFromFile (database : list<Phone>) (path : string) =
    use stream = new StreamReader(path)
    
    let rec readLoop localDatabase =
        let str = stream.ReadLine()
        if isNull str then localDatabase
        else
            let splitLine = str.Split(' ')
            if splitLine.Length <> 2 then raise (invalidArg "Name and number" "Phone book entry should have two argument")
            let newDatabase, _ = addNewEntry localDatabase splitLine[0] splitLine[1]
            readLoop newDatabase
    
    readLoop database