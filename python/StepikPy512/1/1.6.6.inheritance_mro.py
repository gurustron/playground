class A:
    pass


class B(A):
    pass


class C:
    pass


class D(C):
    pass


class E(B, D, C):
    pass


print(E.mro())
