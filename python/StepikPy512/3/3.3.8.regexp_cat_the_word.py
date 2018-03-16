import sys
import re

for line in sys.stdin:
    line = line.rstrip()
    # process line
    if re.search(r"\bcat\b", line):
        print(line)

