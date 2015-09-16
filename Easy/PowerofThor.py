import sys
import math

# Auto-generated code below aims at helping you parse
# the standard input according to the problem statement.


# game loop
while 1:
    spaceX, spaceY = [int(i) for i in input().split()]
    highestMountain = [0.0]
    for i in range(8):
        mountainH = int(input())  # represents the height of one mountain, from 9 to 0. Mountain heights are provided from left to right.
        if mountainH > highestMountain[0][0]:
            highestMountain = [i, mountainH]
            print(highestMountain, file=sys.stderr)

    # Write an action using print
    # To debug: print("Debug messages...", file=sys.stderr)

    # either:  FIRE (ship is firing its phase cannons) or HOLD (ship is not firing).
    print("HOLD")
