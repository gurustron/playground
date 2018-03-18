import requests


def inf(num):
    url_format = "http://numbersapi.com/{}/math"
    url = url_format.format(num.strip())
    params = {'json': True}
    get = requests.get(url, params)
    res = 'Interesting' if get.json()['found'] else 'Boring'
    print(res)


with open("C:/Users/Sergey/Downloads/dataset_24476_3.txt") as f:
    list(map(inf, f.readlines()))

