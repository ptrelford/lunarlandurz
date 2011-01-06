namespace global

open System
open System.Windows.Controls
open System.Windows.Input

type KeyState (control:Control) =
    let mutable disposables : IDisposable list = []
    let remember item = disposables <- item :: disposables
    let forget () = for item in disposables do item.Dispose()

    let mutable keysDown = Set.empty
    do  control.KeyUp
        |> Observable.subscribe (fun e -> keysDown <- keysDown.Remove e.Key)
        |> remember
    do  control.KeyDown
        |> Observable.subscribe (fun e -> keysDown <- keysDown.Add e.Key)
        |> remember

    member this.IsKeyDown (key:Key) = keysDown.Contains key

    interface IDisposable with
        member this.Dispose() = forget()

