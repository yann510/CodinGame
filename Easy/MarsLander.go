package main

import "fmt"
import "os"

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

func main() {
    // surfaceN: the number of points used to draw the surface of Mars.
    var surfaceN int
    fmt.Scan(&surfaceN)
    var middleFlatAreaX,middleFlatAreaY
    var arrayLand [,]

    for i := 0; i < surfaceN; i++ {
        // landX: X coordinate of a surface point. (0 to 6999)
        // landY: Y coordinate of a surface point. By linking all the points together in a sequential fashion, you form the surface of Mars.
        var landX, landY int
        arr
        fmt.Scan(&landX, &landY)
    }
    for {
        // hSpeed: the horizontal speed (in m/s), can be negative.
        // vSpeed: the vertical speed (in m/s), can be negative.
        // fuel: the quantity of remaining fuel in liters.
        // rotate: the rotation angle in degrees (-90 to 90).
        // power: the thrust power (0 to 4).
        var X, Y, hSpeed, vSpeed, fuel, rotate, power int
        fmt.Scan(&X, &Y, &hSpeed, &vSpeed, &fuel, &rotate, &power)
        
        //We can start going down in straight line
        if (Abs(landX - X) <= 500)
        {
            var straight = true
            fmt.Fprintln(os.Stderr, "We can now go down straight")
        }
        
        //Sets the right rotation angle
        if (straight && rotate != 0)
        {
            if (rotate < 0)
            {
                rotate = Abs(rotate)
            }
            else
            {
                rotate = rotate * -1
            }
        }
        else
        {
            rotate = 0
        }
        


        // fmt.Fprintln(os.Stderr, "Debug messages...")

        fmt.Println(rotate + " " + ) // rotate power. rotate is the desired rotation angle. power is the desired thrust power.
    }
}
