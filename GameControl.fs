namespace global

open System
open System.Collections.Generic
open System.Windows
open System.Windows.Controls
open System.Windows.Input
open System.Windows.Media
open System.Windows.Shapes
open System.Windows.Threading

type GameState = Play | GameOver

type GameControl() as control=
    inherit UserControl ()

    let uri = Uri("/LunarLandurz;component/GameControl.xaml", UriKind.Relative)
    do  Application.LoadComponent(control, uri)

    let disposables = List<IDisposable>()
    let remember = disposables.Add
    let forget () = for item in disposables do item.Dispose()

    let top,left,width,height = 0.0,0.0,560.0,480.0
    do  control.Width <- width
    do  control.Height <- height

    let canvas = Canvas(Background=SolidColorBrush Colors.Black)
    do  control.Content <- canvas
    let add (element:#UIElement) = canvas.Children.Add element
    let remove (element:#UIElement) = canvas.Children.Remove element |> ignore

    let keyState = new KeyState(control)
    do  keyState |> remember
    let isKeyDown = keyState.IsKeyDown

    let playMedia name =
        let me = MediaElement(AutoPlay=true)
        me.Source <- Uri(name, UriKind.Relative)
        add me
        me.CurrentStateChanged
        |> Observable.filter (fun _  -> me.CurrentState = Media.MediaElementState.Paused)
        |> Observable.run (fun _ -> remove me)

    let playExplosion () = playMedia "/explosion.mp3"
    let thrust = new LoopElement "/thrust.mp3"
    do  thrust |> remember
    do  add thrust.Element

    let mutable gameState = Play
    let mutable landed = false
    let lander = Lander()

    let fuelGauge = TextControl(control,Top,Right,1.0)
    do  add fuelGauge.Control
    let setFuelGauge () =
        let fuel = int (lander.Fuel / 1.0<l>)
        fuelGauge.Text <- sprintf "FUEL %05dL" fuel
    do  setFuelGauge()

    let speedGauge = TextControl(control,Top,Left,1.0)
    do  add speedGauge.Control
    let setSpeedGauge () =
        let dx, dy = lander.Velocity
        let d = sqrt ((dx * dx) + (dy * dy)) / 1.0<m/s>
        speedGauge.Text <- sprintf "SPEED %0.2fm/s" d
    do  setSpeedGauge ()

    let terrainLines = Terrain.ScaleVertices (width,height) 
    do  terrainLines |> toPolyline |> add
            
    let createLander () =
        let canvas = Canvas ()
        Canvas.SetTop(canvas,-999.0)
        Canvas.SetLeft(canvas,-999.0)
        lander.Vertices()
        |> toLines
        |> List.iter canvas.Children.Add
        canvas

    let craft = createLander()
    let rotation = RotateTransform()
    do  craft.RenderTransform <- rotation
    do  add craft

    let update elapsed =
        if gameState = Play then
            Play.update (top,left,width,height) lander isKeyDown thrust elapsed
            let landerLines = lander.MapVertices (lander.X/1.0<m>, lander.Y/1.0<m>)
            match tryFindCollisionLine landerLines terrainLines with
            | Some line ->
                thrust.Pause()
                if line.V1.Y = line.V2.Y then 
                    gameState <- GameOver
                    landed <- true
                else
                    playExplosion ()
                    gameState <- GameOver
                let message = TextControl(control,Middle,Center,2.0)
                message.Text <- if landed then "COMPLETED" else "GAME OVER"
                add message.Control
            | None -> ()
        else
            if isKeyDown Key.Enter then 
                lander.Reset ()
                landed <- false
                gameState <- Play
        Canvas.SetTop(craft,lander.Y/1.0<m>)
        Canvas.SetLeft(craft,lander.X/1.0<m>)
        rotation.Angle <- (lander.Angle * 180.0 / (Math.PI * 1.0<rad>)) - 180.0
        setFuelGauge ()
        setSpeedGauge ()

    let startGame () =
        let lastUpdate = ref DateTime.Now
        CompositionTarget.Rendering
        |> Observable.subscribe (fun x -> 
            let now = DateTime.Now
            now - !lastUpdate |> update
            lastUpdate := now
        )
        |> remember

    do  let prompt = TextControl(control,Middle,Center,2.0,Text="Click to Start")
        add prompt.Control
        control.MouseLeftButtonUp
        |> Observable.run (fun _ ->
            remove prompt.Control |> ignore
            startGame()
        )

    interface IDisposable with
        member this.Dispose() = forget()