[<AutoOpen>]
module Collisions

/// Collision detection between lander and terrain
let tryFindCollisionLine (lander:Vector2 []) (terrain:Vector2 []) =
    /// Computes lines for a set of vertices 
    let computeLines (vertices:Vector2 array) =
        let length = Array.length vertices 
        Array.zip (Array.sub vertices 0 (length - 1)) (Array.sub vertices 1 (length-1))
        |> Array.map (fun (a,b) -> Line2 (a,b))
    let landerLines = lander |>  computeLines
    terrain |> computeLines |> Array.tryFind (fun a ->
        match 
            landerLines 
            |> Array.tryFind 
            (fun b -> Line2.Intersect (a, b) ) with
        | Some (line) -> true
        | None -> false
    )