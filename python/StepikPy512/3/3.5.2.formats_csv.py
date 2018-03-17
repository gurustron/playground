import csv
import itertools
import datetime

res = {}
with open("Crimes.csv", "r") as file:
    reader = csv.DictReader(file)
    for row in reader:
        year = datetime.datetime.strptime(row['Date'], '%m/%d/%Y %I:%M:%S %p').year
        if year == 2015:
            t = row['Primary Type']
            res[t] = res.get(t, 0) + 1
        # print(row)

print(res)
print(max(res, key=lambda k: res[k]))
print(res[max(res, key=lambda k: res[k])])
