import sys


# Auto-generated code below aims at helping you parse
# the standard input according to the problem statement.

n = int(input())
horseArray = []
for i in range(n):
    pi = int(input())
    horseArray.append(pi)

sortedHorse = sorted(horseArray)
print(sortedHorse, file=sys.stderr)

try:
    lowestDiff = 100000000
    for idx, val in enumerate(sortedHorse):
        diff = abs(val - sortedHorse[idx+1])
        print(diff, file=sys.stderr)
        if diff < lowestDiff:
            lowestDiff = diff

except Exception:
    pass
# Write an action using print
# To debug: print("Debug messages...", file=sys.stderr)

print(lowestDiff)
