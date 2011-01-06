namespace global

open System
open System.Windows.Controls
open System.Windows.Media

type LoopElement (name) as loop =
    let element = MediaElement(AutoPlay=false, Source=Uri(name,UriKind.Relative))
    let disposable =
        element.MediaEnded
        |> Observable.subscribe (fun _ -> loop.Play())
    member loop.Element = element
    member loop.Pause () = 
        if element.CurrentState = MediaElementState.Playing then 
            element.Pause()
    member loop.Play () =
        if element.CurrentState <> MediaElementState.Playing then
            element.Position <- TimeSpan()
            element.Play()
    interface IDisposable with
        member this.Dispose() = disposable.Dispose()