hierarchy = {}
caught = []


def check_caught(e):
    return e in caught or any(map(lambda ee: check_caught(ee), hierarchy[e]))
    pass


for i in range(2):
    for hie in [input().split() for j in range(int(input()))]:
        ex = hie[0]
        if i:
            if check_caught(ex):
                print(ex)
            else:
                caught.append(ex)
        else:
            hierarchy[ex] = hie[2:]
