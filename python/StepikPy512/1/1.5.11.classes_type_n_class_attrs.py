class A:
    val = 1

    def foo(self):
        A.val += 2

    def bar(self):
        print(self.val)
        self.val += 1
        print(self.val)


a = A()
b = A()


a.foo()
a.bar()
a.bar()
a.bar()
a.foo()

c = A()
print(a.val is b.val)
print(c.val is b.val)
c.bar()
print(c.val is b.val)
print(a.val)
print(b.val)
print(c.val)
