namespace global

type Line2 =
    struct
        val V1 : Vector2
        val V2 : Vector2
    end
    
    new (v1,v2) = {V1 = v1; V2 = v2}

    member line.IsBounded (v:Vector2) =    
        let isBoundedX =            
            if line.V1.X > line.V2.X then
                v.X <= line.V1.X && v.X >= line.V2.X
            else 
                v.X >= line.V1.X && v.X <= line.V2.X
        let isBoundedY =
            if line.V1.Y > line.V2.Y then
                v.Y <= line.V1.Y && v.Y >= line.V2.Y
            else
                v.Y >= line.V1.Y && v.Y <= line.V2.Y
        isBoundedX && isBoundedY
    
    static member PointOfIntersection (a:Line2, b:Line2) =
        let computeDX (line:Line2) = 
            (line.V2.Y - line.V1.Y) / (line.V2.X - line.V1.X)
        let computeKY (line:Line2) =
            line.V1.Y - (line.V1.X * computeDX line)
        let ky = computeKY a - computeKY b
        let dx = computeDX a - computeDX b
        let x = -ky / dx
        let y = computeKY a + (x * computeDX a)
        Vector2 (x,y)

    static member Angle (a:Line2, b:Line2) = 
        let vA = a.V1 - a.V2 |> Vector2.normalize
        let vB = b.V1 - b.V2 |> Vector2.normalize
        let dotProduct = Vector2.dot (vA , vB)
        let dot = min 1.0 (max -1.0 dotProduct)
        acos (dot)

    static member Intersect (a:Line2, b:Line2) =
        let vi = Line2.PointOfIntersection (a,b)
        if a.IsBounded (vi) then
            let l1 = Line2 (vi, b.V1)
            let l2 = Line2 (vi, b.V2)
            let r = Line2.Angle (l1,l2)
            r > System.Math.PI / 2.0    // 90 degrees
        else
            false