class MoneyBox:
    def __init__(self, capacity):
        self.cap = capacity
        self.val = 0

    def can_add(self, v):
        return self.val + v <= self.cap

    def add(self, v):
        self.val += v


print(MoneyBox(0).can_add(1))
print(MoneyBox(3).can_add(2))
m = MoneyBox(6)
print(m.can_add(6))
m.add(3)
print(m.can_add(1))
print(m.can_add(2))
print(m.can_add(3))
print(m.can_add(4))
m.add(3)
print(m.can_add(0))
print(m.can_add(1))
