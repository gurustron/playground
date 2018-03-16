import re
import sys

for line in sys.stdin:
    line = line.rstrip()
    # process line
    print(re.sub(r"\b[aA]+\b", "argh", line, 1))



