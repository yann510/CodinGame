import sys
import math

# Auto-generated code below aims at helping you parse
# the standard input according to the problem statement.


# game loop
while 1:
    spaceX, spaceY = [int(i) for i in input().split()]
    highestMountain = 0
    highestMountainX = 0
    for i in range(8):
        mountainH = int(
            input())  # represents the height of one mountain, from 9 to 0. Mountain heights are provided from left to right.
        if mountainH > highestMountain:
            highestMountain = mountainH
            highestMountainX = i
            print("mountain height :{} mountain x: {}".format(highestMountain, highestMountainX), file=sys.stderr)

    print("spaceX: {} | spaceY : {} | mountainHeight: {}"
          " | mountainX  {} | isEqual: {}".format(spaceX, spaceY,
                                                  highestMountain,
                                                  highestMountainX,
                                                  spaceX == highestMountainX),
          file=sys.stderr)
    if spaceX == highestMountainX:
        print("FIRE")
    else:
        print("HOLD")
