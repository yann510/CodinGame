import sys
import math

# Auto-generated code below aims at helping you parse
# the standard input according to the problem statement.

road = int(input())  # the length of the road before the gap.
gap = int(input())  # the length of the gap.
platform = int(input())  # the length of the landing platform.

# game loop
while 1:
    speed = int(input())  # the motorbike's speed.
    coordX = int(input())  # the position on the road of the motorbike.
    action = ""

    # Write an action using print
    print("road: {} | gap : {} | platform : {} | speed: {} | coordX : {}".format(road, gap, platform, speed, coordX),
          file=sys.stderr)
    if coordX >= road + gap:
        action = "SLOW"
    elif coordX == road-1:
        action = "JUMP"
    else:
        if speed == 0:
            action = "SPEED"
        elif speed < gap + 1:
            action = "SPEED"
        elif speed > gap + 1:
            action = "SLOW"
        elif road % speed == 0:
            action = "WAIT"
        else:
            action = "WAIT"


    # A single line containing one of 4 keywords: SPEED, SLOW, JUMP, WAIT.
    print(action)
