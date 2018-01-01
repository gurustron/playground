# c = 1
# b = 2
# d = 1
# print(id(c))
# print(id(d))
# print(id(1))
# print(id(b))
# print(type(id(c)))
#
# x = [1, 2, 3]
# y = x
# y.append(4)
#
# s = "123"
# t = s
# t = t + "4"
#
# print(str(x) + " " + s)


# print((1 == True))


a = []


def foo(arg1, arg2):
    print("args", arg1, arg2)
    a.append("foo")


foo(a.append("arg1"), a.append("arg2"))

print(a)
print(a.append("arg2"), a.append("arg2"))
