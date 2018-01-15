import itertools


def primes():
    primes_num = []
    s = 1
    while True:
        s += 1
        if all(s % i for i in primes_num):
            primes_num.append(s)
            yield s


print(list(itertools.takewhile(lambda x: x <= 31, primes())))
# print(list(itertools.takewhile(lambda x: x <= 0, primes())))
print([2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31])

print(primes)
print(primes())
a = list(i + 1 for i in range(4))
print(a)
