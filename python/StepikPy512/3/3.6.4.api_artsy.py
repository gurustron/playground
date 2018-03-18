import requests
import json

client_id = '...'
client_secret = '...'

# инициируем запрос на получение токена
r = requests.post("https://api.artsy.net/api/tokens/xapp_token",
                  data={
                      "client_id": client_id,
                      "client_secret": client_secret
                  })

# разбираем ответ сервера
j = r.json()

# достаем токен
token = j["token"]

# создаем заголовок, содержащий наш токен
headers = {"X-Xapp-Token": token}

artists = []
with open("C:/Users/Sergey/Downloads/dataset_24476_4 (1).txt") as f:
    for artist_id in map(str.strip, f.readlines()):
        # инициируем запрос с заголовком
        r = requests.get("https://api.artsy.net/api/artists/%s" % artist_id, headers=headers)

        # разбираем ответ сервера
        j = r.json()
        print(j)
        artists.append(j)

list(map(lambda k: print(k['sortable_name']), sorted(artists, key=lambda item: (item['birthday'], item['sortable_name']))))
