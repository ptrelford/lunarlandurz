namespace LunarLandurz

type App() as this = 
    inherit System.Windows.Application()
    do  this.Startup.AddHandler(fun o e -> this.RootVisual <- new GameControl())
