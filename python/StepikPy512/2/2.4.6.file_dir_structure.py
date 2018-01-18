import os
import os.path
import re

r = "D:/Stron/Projects/"
fld = "main"
lines = []

with open("2.4.6.A", "w+") as f:
    for root, d, files in os.walk(r + fld):
        if any(f.endswith(".py") for f in files):
            lines.append(re.sub(r, '', root, flags=re.IGNORECASE))
            print(root)
    print(lines)
    f.write("\n".join(lines))
