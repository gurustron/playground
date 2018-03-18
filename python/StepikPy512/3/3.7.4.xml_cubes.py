from xml.etree import ElementTree

root = ElementTree.fromstring('<cube color="blue"><cube color="red"><cube color="green"></cube></cube><cube color="red"></cube></cube>')

# print(root.attrib)
res = {}
queue = [(root, 1)]

while len(queue) > 0:
    curr = queue.pop()

    elem = curr[0]
    lvl = curr[1]
    new_lvl = lvl + 1
    for child in elem:
        queue.append((child, new_lvl))
    key = elem.attrib['color']
    res[key] = res.get(key, 0) + lvl

print(" ".join(str(x) for x in [res['red'], res['green'], res['blue']]))
