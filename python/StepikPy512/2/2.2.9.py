from simplecrypt import decrypt, DecryptionException

pas = None

with open("passwords.txt", "rb") as p:
    pas = p.read().splitlines()

print(pas)

with open("encrypted.bin", "rb") as inp:
    encrypted = inp.read()
    print(decrypt('RVrF2qdMpoq6Lib', encrypted))
    # for p in pas:
    #     try:
    #         print(decrypt(p, encrypted))
    #         print("success", p)
    #     except DecryptionException:
    #         print("failed", p)
    #         pass

#
# 1) идем на сайт https://pypi.python.org/pypi/simple-crypt
# и скачиваем оттуда файл simple-crypt-4.1.7.tar.gz
#
# 2) после распаковки файла копируем оттуда каталог
# simplecrypt и вставляем его в <python>\Lib\site-packages
# где <python> - место установки вашего дистрибутива python
# (анаконды или стандартного интерпретатора)
#
# 3) в командной строке выполняем команду
# pip install pycryptodome для вашего дистрибутива python
# (анаконды или стандартного интерпретатора). pycryptodome
# заменяет пакет pycrypto, который нужен при установке
# simple-crypt
#
# 4) в самом интерпретаторе python теперь можно
# набирать import simplecrypt и я надеюсь, что все
# у вас будет теперь работать.﻿