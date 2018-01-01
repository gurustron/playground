num = int(input())


def parse_request(x):
    parents = {"global": None}
    variables = {"global": []}

    def find(ns, var):
        if var in variables[ns]:
            print(ns)
        else:
            parent = parents[ns];
            if parent is not None:
                find(parent, var)
            else:
                print(parent)

    try:
        for s in x:
            cmd, first, second = s.split()
            if cmd == "add":
                variables[first].append(second)
            elif cmd == "create":
                parents[first] = second
                variables[first] = []
            elif cmd == "get":
                find(first, second)
        return parents, variables
    except:
        print(s, parents, variables)
        raise


requests = [input() for i in range(num)]
parse_request(requests)
