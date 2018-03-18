import json

# json.dump()
# json.dumps()

hie = json.loads(input())
dictHie = {}
for d in hie:
    dictHie[d['name']] = set(d['parents'])

ancestors = {}


def dfs(graph, start):
    visited, stack = set(), [start]
    while stack:
        vertex = stack.pop()
        if vertex not in visited:
            visited.add(vertex)
            stack.extend(graph[vertex] - visited)
    ancestors[start] = set(visited)


for s in dictHie.keys():
    dfs(dictHie, s)
for k in sorted(dictHie.keys()):
    print("{} : {}".format(k, sum(1 if k in x else 0 for x in ancestors.values())))
