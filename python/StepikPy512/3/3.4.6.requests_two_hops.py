import requests
import re

where, what = [input() for x in range(2)]
urls = [(0, where)]
link_pattern = "<a href=\"(.*)\">"
found = 'No'
while len(urls) > 0:
    curr = urls[0]
    urls.remove(curr)
    if curr[1].find(what) >= 0 and curr[0] == 2:
        found = 'Yes'
        break
    if curr[0] > 2:
        continue
    get = requests.get(curr[1])
    if get.status_code != 404:
        list(map(urls.append, [(curr[0] + 1, x) for x in re.findall(link_pattern, get.text)]))

print(found)






