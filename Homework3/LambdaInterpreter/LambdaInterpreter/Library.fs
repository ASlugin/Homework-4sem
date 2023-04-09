module LambdaInterpreter

let letters = List.map (fun x -> x.ToString()) [ 'A' .. 'z' ]

type Term =
    | Variable of string
    | Abstraction of string * Term
    | Application of Term * Term

let findNewName (usedVariables: string list) =
    let unused = List.except usedVariables letters
    List.head unused

let rec getFreeVariables term = 
    match term with
    | Variable x -> [x]
    | Abstraction (x, t) -> List.filter (fun y -> y <> x) (getFreeVariables t)
    | Application (t1, t2) -> List.append (getFreeVariables t1) (getFreeVariables t2)

let rec substitution variable term newTerm = 
    match (term, newTerm) with
    | Application(left, right), _ -> Application(substitution variable left newTerm, substitution variable right newTerm)
    | Variable(x), _ when x = variable -> newTerm
    | Variable _, _ -> term
    | Abstraction(var, currentTerm), Variable _ when var = variable -> Abstraction(var, currentTerm)
    | Abstraction(var, currentTerm), _ ->
        let newTermFV = getFreeVariables newTerm
        let currentTermFV = getFreeVariables currentTerm
        if (not (List.contains var newTermFV) || not (List.contains variable currentTermFV)) then
            Abstraction(var, substitution variable currentTerm newTerm)
        else 
            let newName = findNewName (newTermFV @ currentTermFV)
            let newVar = substitution var currentTerm (Variable newName)
            let substitutedTerm = substitution variable newVar newTerm
            Abstraction(newName, substitutedTerm)

let betaReduction term = 
    let rec reduction term=
        match term with
        | Variable(var) -> Variable(var)
        | Application(Abstraction(variable, term2), term) -> substitution variable term2 term
        | Application(term1, term2) ->
            let countedTerm1 = reduction term1
            match countedTerm1 with
            | Abstraction(variable, term3) -> reduction(substitution variable term3 term2)
            | _ -> Application(countedTerm1, reduction(term2))
        | Abstraction(variable, term) -> Abstraction(variable, reduction(term))
    reduction term