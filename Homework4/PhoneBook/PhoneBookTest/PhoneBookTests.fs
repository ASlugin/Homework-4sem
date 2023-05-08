module PhoneBookTest

open NUnit.Framework
open PhoneBook
open FsUnit

let name = "Alexander"
let number = "88005553535"
    
[<Test>]
let ``After adding new entry database should contains this entry``() =
    let database, itIsNewEntry = addNewEntry [] name number
    
    List.exists (fun entry -> entry.Name = name && entry.Number = number) database |> should be True
    itIsNewEntry |> should be True

[<Test>]
let ``After adding same entry database should contains only one such entry``() =
    let database, _ = addNewEntry [] name number
    let database, itIsNewEntry = addNewEntry database name number
    
    List.filter (fun entry -> entry.Name = name && entry.Number = number) database |> List.length |> should equal 1
    itIsNewEntry |> should be False

[<Test>]
let ``findNumberByName should work correctly``() =
    let database, _ = addNewEntry [] name number
    
    findNumberByName database name |> should equal (Some number)
    findNumberByName database "NonExistentName" |> should equal None

[<Test>]
let ``findNameByNumber should work correctly``() =
    let database, _ = addNewEntry [] name number
    
    findNameByNumber database number |> should equal (Some name)
    findNumberByName database "00000" |> should equal None

[<Test>]
let ``Databases before saveDataTiFile and after readDataFromFile should be equal``() =
    let database, _ = addNewEntry [] name number
    let database, _ = addNewEntry database "Egor" "987654321"
    
    saveDataToFile database "test.txt"
    let databaseFromFile = readDataFromFile database "test.txt"
    
    List.length database |> should equal (List.length databaseFromFile)
    
    List.zip database databaseFromFile |> List.iter (fun (a, b) ->
        a.Name |> should equal b.Name
        a.Number |> should equal b.Number
        )