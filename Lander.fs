namespace global

open System.Windows.Media
open System.Windows.Shapes

/// Lunar lander type
type Lander () as this =
    [<DefaultValue>]
    val mutable private x : float<m>
    [<DefaultValue>]
    val mutable private y : float<m>
    [<DefaultValue>]
    val mutable private velocity : float<m/s> * float<m/s>
    [<DefaultValue>]      
    val mutable private angle : float<rad>
    [<DefaultValue>]
    val mutable private angularVelocity : float<rad/s>
    [<DefaultValue>]
    val mutable private fuel : float<l>
    
    let SetDefaultValues () =
        this.x <- 100.0<m>
        this.y <- 200.0<m>
        this.velocity <- (0.0<m/s>,0.0<m/s>) 
        this.angle <- degrees.toRadians 0.0<deg>
        this.angularVelocity <- 0.0<rad/s>
        this.fuel <- 999.0<l>
        
    do SetDefaultValues ()

    let vertices =
        [|
            (-4,-4); (-2,-4);
            (-3,-4); (-3,-3);
            (-3,-3); (-2,-2);
            (-2,-2); (-2, 0);
            (-2,-2); ( 2,-2);
            (-2, 0); ( 2, 0);
            ( 2,-2); ( 2, 0);
            (-1, 0); (-2, 1);
            (-2, 1); (-2, 2);
            (-2, 2); (-1, 3); 
            (-1, 3); ( 1, 3);
            ( 1, 3); ( 2, 2);
            ( 2, 2); ( 2, 1);
            ( 2, 1); ( 1, 0);
            ( 1, 0); (-1, 0);
            ( 2,-2); ( 3,-3);
            ( 3,-3); ( 3,-4);
            ( 2,-4); ( 4,-4);
            (-2,-3); ( 2,-3);
            (-2,-3); (-1,-2);
            ( 2,-3); ( 1,-2);
        |]
        |> Array.map (fun (dx,dy) -> dx * 4,  dy * 4)
        |> Array.map (fun (dx,dy) -> float dx, float dy)
            
    member this.X = this.x
    member this.Y = this.y
    member this.Angle = this.angle
    member this.Velocity = this.velocity
    member this.Fuel with get () = this.fuel and set value = this.fuel <- value
    
    /// Resets values
    member this.Reset () =
        SetDefaultValues ()

    /// Updates lander
    member this.Teleport (newX,newY) =
        this.x <- newX
        this.y <- newY
    
    /// Updates lander
    member this.Update elapsed (leftThrust, rightThrust, forwardThrust) = 
        // Capture ship forward acceleration
        let acceleration =
            match forwardThrust with
            | true -> 50.0<m/s^2>
            | false -> 0.0<m/s^2>

        // Update ship angle 
        let rotationalThrust = leftThrust - rightThrust
        let rotation = degrees.toRadians 0.05<deg> * rotationalThrust
        if rotation <> 0.0<rad> then
            let acceleration = (rotation / elapsed)
            this.angularVelocity <- this.angularVelocity + acceleration
        this.angle <- this.angle + (this.angularVelocity * elapsed)
        
        // Update ship velocity
        let heading = (this.angle - degrees.toRadians 90.0<deg>) / 1.0<rad>
        let ddx, ddy = 
            (acceleration * cos (heading)), (acceleration * sin (heading))
        let newVelocity = 
            let dx, dy = this.velocity 
            (dx + (ddx * elapsed), 
                dy + (ddy * elapsed) + (Gravity.Lunar * elapsed))
        this.velocity <- newVelocity
        
        // Update ship position
        this.x <- this.x + (fst this.velocity * elapsed)
        this.y <- this.y + (snd this.velocity * elapsed)

    member this.Vertices () =
        vertices
        |> Array.map (fun (x,y) -> Vector2(x,y))

    /// Map vertices to specified location
    member this.MapVertices (x,y) =
        vertices
        |> Array.map (fun (dx,dy) -> 
            Radians.Rotate (this.Angle + degrees.toRadians 180.0<deg>)  (dx,dy) ) 
        |> Array.map (fun (dx,dy) -> x + dx,y + dy)
        |> Array.map (fun (x,y) -> Vector2(x,y))
