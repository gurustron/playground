(s, a, b) = [input() for i in range(3)]

if s.find(a) == -1:
    print(0)
elif b.find(a) > -1:
    print("Impossible")
else:
    i = 0
    while s.count(a) > 0:
        s = s.replace(a, b)
        i += 1
    print(i)
