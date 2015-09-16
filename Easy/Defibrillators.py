import sys
import math


class Defibrillator(object):
    def __init__(self, number, name, adress, contactphone, longitude, latitude):
        self.number = number
        self.name = name
        self.adress = adress
        self.contactPhone = contactphone
        self.longitude = float(longitude.replace(",", "."))
        self.latitude = float(latitude.replace(",", "."))


lon = float(input().replace(",", "."))
lat = float(input().replace(",", "."))
n = int(input())
closestDistance = 100000000.00000000
cloestDefibrilator = Defibrillator
for i in range(n):
    defib = input()
    attributes = defib.split(";")

    defibrillator = Defibrillator(attributes[0], attributes[1], attributes[2], attributes[3], attributes[4],
                                  attributes[5])
    print(defibrillator.latitude, file=sys.stderr)
    x = (defibrillator.longitude - lon) * math.cos((lat + defibrillator.latitude) / 2)
    y = defibrillator.latitude - lat
    distance = math.sqrt(pow(x, 2) + pow(y, 2)) * 6371

    print(defibrillator.name + " distance:" + str(distance), file=sys.stderr)

    if distance < closestDistance:
        closestDistance = distance
        cloestDefibrilator = defibrillator

print(cloestDefibrilator.name)
# Write an action using print
# To debug: print("Debug messages...", file=sys.stderr)

