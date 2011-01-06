namespace global

type Vector2 =
    struct
        val X : float
        val Y : float
    end
    new (x,y) = { X=x; Y=y }
    static member (+) (lhs:Vector2,rhs:Vector2) =
        Vector2(lhs.X-rhs.X,lhs.Y-rhs.Y)
    static member (-) (lhs:Vector2,rhs:Vector2) =
        Vector2(lhs.X-rhs.X,lhs.Y-rhs.Y)
    static member dot (vector1:Vector2,vector2:Vector2) =
        (vector1.X * vector2.X) + (vector1.Y * vector2.Y)
    static member length (vector:Vector2) =
        sqrt(vector.X ** 2.0 + vector.Y ** 2.0)
    static member normalize (vector:Vector2) =
        let length = Vector2.length vector
        Vector2(vector.X/length, vector.Y/length)
    static member toPoint (vector:Vector2) =
        System.Windows.Point(vector.X,vector.Y)