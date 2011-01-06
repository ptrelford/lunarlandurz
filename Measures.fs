namespace global

[<Measure>]
type m
[<Measure>]
type s

module Gravity = 
    let Earth = 9.8<m/s^2> // Standard Gravity
    let Lunar = Earth / 6.0

[<Measure>] 
type radians
[<Measure>] 
type rad = radians
[<Measure>] 
type degrees =
    static member toRadians (d:float<degrees>) =
        (System.Math.PI * d) / (180.0<degrees/rad>)

[<Measure>] 
type deg = degrees
[<Measure>] 
type litres
[<Measure>]
type l = litres

/// Radians helper        
module Radians =           
    let OfDegrees d = degrees.toRadians d
    let Rotate (a:float<rad>) (x:float,y:float) =
        let r = a / 1.0<rad>           
        x * cos(r) - y * sin(r), y * cos(r) + x * sin(r)

