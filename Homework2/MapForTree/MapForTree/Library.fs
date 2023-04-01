module MapForTree
    
type Tree<'a> =
    | Tree of 'a * Tree<'a> * Tree<'a>
    | Tip of 'a
    | Empty
    
let rec mapTree func tree =
    match tree with
    | Tree(node, leftTree, rightTree) -> Tree(func node, mapTree func leftTree, mapTree func rightTree)
    | Tip node -> Tip(func node)
    | Empty -> Empty