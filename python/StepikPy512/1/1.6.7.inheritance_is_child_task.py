import sys

sys.stdin = open("data.txt", "r")

hierarchy = {}


def create_hierarchy(x):
    for s in x:
        a = s.split(":")
        if len(a) == 1:
            hierarchy[a[0].strip()] = ["object"]
        elif len(a) == 2:
            hierarchy[a[0].strip()] = a[1].split()


def check_is_parent(s):
    stack = list()
    par, cls = s.split()
    stack.append(cls)
    while len(stack) > 0:
        curr = stack.pop()
        if curr == par:
            print("Yes")
            return
        elif curr in hierarchy:
            stack.extend(hierarchy[curr])
    print("No")


num = int(input())
requests = [input() for i in range(num)]
create_hierarchy(requests)

num = int(input())
requests = [input() for i in range(num)]
list(map(check_is_parent, requests))
