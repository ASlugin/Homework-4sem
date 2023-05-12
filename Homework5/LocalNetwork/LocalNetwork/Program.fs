module LocalNetwork

type OS =
    | Windows
    | Linux
    | MacOS

type Infected =
    | Clear
    | AtThisTurn
    | Infected

type Computer(operatingSystem: OS, computerID: string, isInfected: Infected) =
    member c.Os = operatingSystem
    member c.Id = computerID
    member val IsInfected = isInfected with get, set

    override x.Equals other =
        match other with
        | :? Computer as y -> (y.Id = x.Id && y.IsInfected = x.IsInfected && y.Os = x.Os)
        | _ -> false
    
    override _.GetHashCode () = computerID.GetHashCode()

    member val InfectionChance =
        match operatingSystem with
        | Windows -> 0.8
        | Linux -> 0.5
        | MacOS -> 0.7

type Network(computers : list<Computer>, connectionsList, infectingCondition : unit -> double) =
    let computersCount = computers.Length
    let mutable changedThisStep = false

    member val InfectedComputers =
        List.fold
            (fun infectedCount (pc: Computer) ->
                if pc.IsInfected = Clear then
                    infectedCount
                else
                    infectedCount + 1)
            0
            computers with get, set

    member this.getComputers
        with get () = computers

    member _.willInfect(computer: Computer) =
        computer.InfectionChance > infectingCondition()
        
    member _.IsClear(pcID: string) =
        let computer = computers |> List.find (fun pc -> pc.Id = pcID)
        computer.IsInfected = Clear

    member this.infectNeighbours(computer: Computer) =
        connectionsList |> List.iter (fun (firstPC, secondPC) ->
                if firstPC = computer.Id && this.IsClear(secondPC) then
                    this.infectComputer secondPC
                if secondPC = computer.Id && this.IsClear(firstPC) then
                    this.infectComputer firstPC)

    member this.infectComputer(pcID) =
        let computer = computers |> List.find (fun pc -> pc.Id = pcID)
        if this.willInfect computer && computer.IsInfected = Clear then
            computer.IsInfected <- AtThisTurn
            this.InfectedComputers <- this.InfectedComputers + 1
            changedThisStep <- true

    member this.nextStep() =
        computers |> List.iter (fun (pc: Computer) ->
                    if pc.IsInfected = AtThisTurn then
                        pc.IsInfected <- Infected)
        computers |> List.iter (fun (pc: Computer) ->
                    if pc.IsInfected = Infected then
                        this.infectNeighbours pc)

    member this.Run() =
        match this.InfectedComputers with
        | 0 -> printfn "No infected computers"
        | x when x = computersCount -> printfn $"All of {x} computers are infected"
        | x when x > 0 && x < computersCount ->
            changedThisStep <- false
            printfn $"{this.InfectedComputers} computers are infected"
            this.nextStep ()
            if not changedThisStep then
                printfn ""
            else
                this.Run()
        | _ -> printfn ""