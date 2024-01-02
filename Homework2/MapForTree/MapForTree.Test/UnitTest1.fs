module MapForTree.Test

open MapForTree
open NUnit.Framework
open FsUnit

let caseBinTree =
    [
    (Tree.Empty, (fun x -> x + 77), Tree.Empty)
    (Tree.Tip(2), (fun x -> x), Tree.Tip(2))
    (Tree.Tree(1, Tree.Tip(2), Tree.Empty), (fun x -> x * 3 + 1), Tree.Tree(4, Tree.Tip(7), Tree.Empty))
    (
     Tree.Tree(7,
               Tree.Tree(5, Tree.Tip(17), Tree.Tip(3)),
               Tree.Tip(9)),
     (fun x -> x * 2),
     Tree.Tree(14,
               Tree.Tree(10, Tree.Tip(34), Tree.Tip(6)),
               Tree.Tip(18))
    )
    ] |> List.map (fun (tree, func, resultTree) -> TestCaseData(tree, func, resultTree))


[<TestCaseSource("caseBinTree")>]
let mapTreeShallWorkCorrectly tree (func: 'b -> 'b)  resultTree =
    mapTree func tree |> should equal resultTree