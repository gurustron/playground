import sys
import re

for line in sys.stdin:
    line = line.rstrip()
    # process line
    print(re.sub("human", "computer", line))
