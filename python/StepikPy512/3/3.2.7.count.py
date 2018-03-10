(a, b) = [input() for i in range(2)]
_from = 0
i = 0
while True:
    found = a[_from:].find(b)
    if found >= 0:
        i += 1
        _from += found + 1
    else:
        break

print(i)
