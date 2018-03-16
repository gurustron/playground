import sys
import re

pattern = r"cat"
for line in sys.stdin:
    line = line.rstrip()
    # process line
    var = re.findall(pattern, line)
    if var and len(var) >= 2:
        print(line)
