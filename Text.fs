namespace global

open Font
open System
open System.Collections.Generic
open System.Windows
open System.Windows.Controls
open System.Windows.Media
open System.Windows.Shapes

[<AutoOpen>]
module Text =
    let CreateShapes scale (lines:(int * int) list list) =
        let lineWidth, lineHeight = 4.0*scale, 3.0*scale
        lines |> List.map (fun points ->
            let line = Polyline()
            line.Stroke <- SolidColorBrush(Colors.Green)
            line.StrokeThickness <- 2.0
            points |> List.iter (fun (dx,dy) ->
                let dx, dy = float dx, float dy
                Point (dx*lineWidth, dy*lineHeight) 
                |> line.Points.Add
            )
            line
        )

    let PlaceShapes (x,y) (shapes:#Shape seq) =
        shapes |> Seq.iter (fun shape ->
            Canvas.SetLeft(shape, x)
            Canvas.SetTop(shape, y)
        )

type HorizontalPosition = Left | Center | Right
type VerticalPosition = Top | Middle | Bottom

type TextControl 
        (parent:Control,
         vertical:VerticalPosition,
         horizontal:HorizontalPosition,
         scale:float) =
    let canvas = Canvas()
    let margin = 10.0
    let mutable text = ""
    let mutable inUse = List<_>()
    let lookupCharacter c =
        match mappings |> List.tryFind (fun (a,_) -> a = Char.ToUpper (c) ) with
        | Some (_, lines) -> lines
        | None -> []
    let obtainShapes (available:List<_>) c =
        match available |> Seq.tryFind (fun (c',_) -> c = c') with
        | Some((_,shapes) as item) -> 
            available.Remove item |> ignore
            shapes
        | None -> 
            let shapes = lookupCharacter c |> CreateShapes scale
            shapes |> List.iter canvas.Children.Add
            shapes
    let setText value =
        text <- value
        let width, height = parent.Width, parent.Height
        let girth = float text.Length * 16.0 * scale
        let x = 
            match horizontal with 
            | Left -> margin 
            | Center -> (width - girth)/2.0 
            | Right -> width - girth - margin
        let y = 
            match vertical with
            | Top -> margin
            | Middle -> height/2.0
            | Bottom -> height - 16.0 - margin
        let available = inUse
        inUse <- new List<_>()
        let charWidth = 16.0 * scale
        text.ToCharArray () |> Array.iteri (fun i c ->
            let shapes = obtainShapes available c
            inUse.Add(c,shapes)
            PlaceShapes (x + float i * charWidth, y) shapes
        )
        for (_,lines) in available do
            for line in lines do canvas.Children.Remove line |> ignore
    member this.MeasureText (text:string) =
        float text.Length * 16.0 * scale
    member this.Text
        with get () = text
        and set value = if value <> text then setText value
    member this.Control = canvas :> UIElement
