namespace global

/// Seconds helper
module Seconds =
    open System
    // Converts TimeSpan to seconds value
    let OfTimespan (ts:TimeSpan) = float ts.TotalSeconds * 1.0<s>

module Play =
    open System.Windows.Input

    let update (top,left,width,height) 
               (lander:Lander) 
               (isKeyDown:Key -> bool) 
               (thrust:LoopElement) 
               elapsed =

        /// Elapsed seconds since last update
        let elapsed = Seconds.OfTimespan elapsed

        /// Left thruster
        let leftThrust = if isKeyDown Key.Z then 0.25 else 0.0
        /// Right thruster
        let rightThrust = if isKeyDown Key.X then 0.25 else 0.0
        /// Forward thruster
        let forwardThrust =
            if lander.Fuel > 0.0<l> then
                isKeyDown Key.Space
            else
                false
        if forwardThrust then 
            thrust.Play()
            lander.Fuel <- lander.Fuel - 3.0<l>
        else thrust.Pause()
        if lander.Fuel < 0.0<l> then lander.Fuel <- 0.0<l>

        // Handle ship leaving viewport by teleporting to other side
        if (lander.X < (1.0<m> * left)) then 
            lander.Teleport (lander.X + 1.0<m> * width, lander.Y)
        if (lander.X > (1.0<m> * (left + width) )) then 
            lander.Teleport (lander.X - 1.0<m> * width, lander.Y)
        if (lander.Y < (1.0<m> * top)) then 
            lander.Teleport (lander.X, lander.Y  + 1.0<m> * height)
        if (lander.Y > (1.0<m> * (top + height) )) then 
            lander.Teleport (lander.X, lander.Y - 1.0<m> * height )
        
        lander.Update elapsed (leftThrust, rightThrust, forwardThrust)