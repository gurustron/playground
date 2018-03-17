import requests
import re

from urllib.parse import urlparse


# from html.parser import HTMLParser

# class MyHTMLParser(HTMLParser):
#     def __init__(self):
#         HTMLParser.__init__(self)
#         self.urls = []
#
#     def handle_starttag(self, tag, attrs):
#         if tag == 'a':
#             for attr in attrs:
#                 if attr[0] == 'href':
#                     self.urls.append(attr[1])
# parser = MyHTMLParser()
# parser.feed(get.text)

get = requests.get('http://pastebin.com/raw/hfMThaGb')  # 'https://stepik.org/lesson/24471/step/7'


matches = re.findall(r'<a[^>]* href="([^"]*)"', get.text)
matches = matches + re.findall(r"<a[^>]* href='([^']*)'", get.text)

urls = set()
for url in matches:
    url = url if "://" in url else "http://" + url
    hostname = urlparse(url).hostname
    if hostname and hostname not in ['..', '.']:
        urls.add(hostname)

list(map(print, sorted(urls)))

# from urllib.parse import urlparse
# from bs4 import BeautifulSoup
#
# get = requests.get(input())
#
# soup = BeautifulSoup(get.text, "html.parser")
#
# urls = set()
# for a in soup.find_all('a', href=True):
#     url = a['href']
#     url = url if "://" in url else "http://" + url
#     hostname = urlparse(url).hostname
#     if hostname:
#         urls.add(hostname)
#
# list(map(print, sorted(urls)))

# get = requests.get('http://pastebin.com/raw/7543p0ns')  # 'https://stepik.org/lesson/24471/step/7'
# matches = re.findall(r'<a[^>]* href="([^"]*)"', get.text)
# # print(matches)
# matches = matches + re.findall(r"<a[^>]* href='([^']*)'", get.text)
#
# urls = set()
# p = '(?:http.*://)?(?P<host>[^:/ ]+).?(?P<port>[0-9]*).*'
# for m in matches:
#     if not (m.startswith('..') or m.startswith('/')):
#         m = re.search(p, m)
#         urls.add(m.group('host'))
#
# list(map(print, sorted(urls)))
