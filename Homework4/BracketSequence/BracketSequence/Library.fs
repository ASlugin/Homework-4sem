module BracketSequence

let checkBrackets (input: string) =
    let isOpeningBracket symbol =
        symbol = '(' || symbol = '[' || symbol = '{'

    let isClosingBracket symbol =
        symbol = ')' || symbol = ']' || symbol = '}'

    let isMatchingPair opening closing =
        (opening = '(' && closing = ')') ||
        (opening = '[' && closing = ']') ||
        (opening = '{' && closing = '}')

    let rec loop (chars: list<char>) (stack: list<char>) =
        match chars with
        | [] ->
            match stack with
            | [] -> true
            | _ -> false
        | head::tail when isOpeningBracket head ->
            loop tail (head::stack)
        | head::tail when isClosingBracket head ->
            match stack with
            | headStack::tailStack when isMatchingPair headStack head -> loop tail tailStack
            | _ -> false
        | head::tail -> loop tail stack

    loop (List.ofSeq input) []