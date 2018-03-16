import sys
import re

for line in sys.stdin:
    line = line.rstrip()
    # process line
    if re.search(r"z.{3}z", line):
        print(line)

