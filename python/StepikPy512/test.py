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


#
# a = []
#
#
# def foo(arg1, arg2):
#     print("args", arg1, arg2)
#     a.append("foo")
#
#
# foo(a.append("arg1"), a.append("arg2"))
#
# print(a)
# print(a.append("arg2"), a.append("arg2"))

#
# class My:
#     a = 10
#
#     def f(self):
#         print("hi")
#
#
# x = My()
# print(type(x))
# print(My)
# print(My.f)
# print(x.f)
# x.f = lambda: print("5")
# print(x.f)
# x.f()
#

# print(str.find.__doc__)

# from random import random
#
# import requests
#
# print("{cap} haha {bar}".format(cap=1, bar="asdasd"))
# f = "r fr {0.url} is {0.status_code}"
# res = requests.get("http://google.com/myurl")
# print(f.format(res))
# x = random()
# print(x)
# print("{:.4}".format(x))


# # сырая строка
# print("\"a")
# print(r"\"a")

# import re
#
# print(re.match)
# print(re.search)
# print(re.findall)
# print(re.sub)
# pattern = r"a[bd]{,4}c"
# s = "absacc abbbc abdbdc abccc abbbbbbbc"
# print(re.findall(pattern, s))
# s = "abc"
# if re.match(pattern, s):
#     print("hehe")
# print(re.search(pattern, s))
#
# line = "!cat?"
# if re.search(r"\bcat\b", line):
#     print(line)


import requests

get = requests.get("https://google.com")
# get = requests.get("https://docs.python.org/3/_static/py.png")
print(get.status_code)
print(get.headers['Content-Type'])
print(get.content)
print(get.text)
#
# with open("py.png", "wb") as f:
#     f.write(get.content)
get = requests.get("https://yandex.ru/search", params={"text": "strong"})
print(get.url)
