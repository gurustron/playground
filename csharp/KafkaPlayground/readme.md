```shell
docker exec -i kafka-topics --create --bootstrap-server localhost:9094 --topic kinaction_helloworld --partitions 3 --replication-factor 3
```