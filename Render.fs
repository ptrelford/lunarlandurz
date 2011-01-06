[<AutoOpen>]
module Render

open System.Windows.Media
open System.Windows.Shapes

let toPolyline (vertices:Vector2 seq) =
    let line = Polyline(Stroke=SolidColorBrush Colors.White, StrokeThickness=1.5)
    vertices
    |> Seq.map Vector2.toPoint
    |> Seq.iter line.Points.Add
    line

let toLines vectors =
    vectors
    |> Seq.fold (fun (lines,v) (v2:Vector2) -> 
        match v with
        | Some (v1:Vector2) ->
            let line = Line(X1=v1.X,Y1=v1.Y,X2=v2.X,Y2=v2.Y) 
            line.StrokeThickness <- 1.5
            line.Stroke <- SolidColorBrush Colors.White
            line :: lines,None
        | None -> lines, Some(v2)
    )  ([],None)
    |> fst

