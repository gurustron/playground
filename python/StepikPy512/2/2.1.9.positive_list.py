class NonPositiveError(ArithmeticError):
    pass


class PositiveList(list):
    def append(self, o):
        if o <= 0:
            raise NonPositiveError
        super().append(o)


try:
    x = PositiveList()
    x.append(1)
    x.append(1)
except NonPositiveError:
    print("non")
