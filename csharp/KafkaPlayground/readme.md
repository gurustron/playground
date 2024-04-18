```shell
docker exec -i kafka-topics --create --bootstrap-server localhost:9094 --topic kinaction_helloworld --partitions 3 --replication-factor 1
```
Since we have single broker - can't have replicating factor more than 1