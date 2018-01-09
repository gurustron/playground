n, k = map(int, input().split())


def c(n_el, size):
    if n_el < size:
        return 0
    if size <= 0:
        return 1
    return c(n_el - 1, size) + c(n_el - 1, size - 1)


print(c(n, k))
