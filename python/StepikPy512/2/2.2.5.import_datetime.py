import datetime

dt = datetime.date(*map(int, input().split())) + datetime.timedelta(days=int(input()))
print(dt.year, dt.month, dt.day)
