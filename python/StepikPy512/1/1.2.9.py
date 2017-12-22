objects = [1, 2, 1, 2, 3]

ans = 0
d = []
for obj in objects:  # доступная переменная objects
    if len(d) == 0:
        d.append(obj)
    else:
        new = True
        for e in d:
            if obj is e:
                new = False
        if new:
            d.append(obj)
ans = len(d)

print(ans)
