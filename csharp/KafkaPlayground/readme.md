```shell
docker exec -i kafkakraft kafka-topics --create --bootstrap-server kafkakraft:29092 --topic kinaction_helloworld --partitions 3 --replication-factor 1
```
Since we have single broker - can't have replicating factor more than 1
```shell
docker exec -i kafkakraft kafka-topics --list --bootstrap-server kafkakraft:29092 
```