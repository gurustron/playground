import time


class Loggable:
    def log(self, msg):
        print(str(time.ctime()) + ": " + str(msg))


class LoggableList(list, Loggable):
    def append(self, o):
        self.log(o)
        super().append(o)


x = LoggableList()
x.append(4)